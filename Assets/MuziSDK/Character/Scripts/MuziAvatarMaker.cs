using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;
using MuziNakamaBuffer;
using Networking;
using Opsive.UltimateCharacterController.Character;
using TriLibCore.Extensions;
using TriLibCore.General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MuziCharacter
{
    public class MuziAvatarMaker : MonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea] public string _Description =
            "This option is ticked avatar will be loaded automatically when script is awoke. Best use for loading none network avatar";
#endif
        [SerializeField] private bool BuildAvatarFromStart = true;

        [Space(20)] [Header("Parent GameObject - holding the Avatar")] [SerializeField]
        private GameObject parentGameObject;

        [Space(20)] [Header("Type of character")] [SerializeField]
        private MuziCharacterType muziCharacterType = MuziCharacterType.Local;

        [Tooltip("Local Character Type in case Character type is NoneNetwork")]
        public SupportedAvatarType noneNetworkBaseType = SupportedAvatarType.Female;

        [Space(20)]
        [Header("NPC Settings")]
#if UNITY_EDITOR
        [TextArea]
        public string NPCDesc = "The NPC settings only apply when character type is none network character";
#endif
        [SerializeField] private bool isNPC;
        [SerializeField] private bool forceDance = false;

        [Space(20)]
        [Header("Supportated Avatar Base Configuration in client")]
#if UNITY_EDITOR
        [TextArea]
        public string __Description =
            "Client needs to prepare appropriate avatar config so that each avatar items (shoe, pant, hair, hat...) would be loaded onto correct" +
            " base object which includes correct animation avatar and root bone!";
#endif
        [SerializeField] public List<ClientAvatarConfig> supportedAvatarConfigs;
        [Space(20)]
        [Header("Data about equipment rule of items")]
#if UNITY_EDITOR
        [TextArea]
        public string equipmentRuleDesc = "Foreach category, avatar has a combination of 3 equipment possibilities: " +
                                          "equip 0, 1, > 1 items. These rules are defined in here";
#endif
        [SerializeField] private CategoryEquipRuleData equipRuleData;

        [SerializeField] private HairHasHeadSkin specialHairs;

        public Vector3 prevCharacterPos;
        public Quaternion prevCharacterRot;

        private readonly Dictionary<string, LocalItem> _allLocalItems = new Dictionary<string, LocalItem>();

        private Animator _parentGoAnimator;
        private UltimateCharacterLocomotion _parentGoCharacterLocomotion;

        private Dictionary<string, bool> _trackingLoadingStates = new Dictionary<string, bool>();


        [Space(20)] [Header("Avatar Data ")] [Tooltip("CurrentAvatar items that shows as a full character")]
        private DataModel.UserAvatar currentAvatar;

        private UNBufUserInfo currentUserInfo;

        public Action OnApplyAvatarDone;

        public Action<Dictionary<string, EquipStateChanges>> OnEquipStateChange;

        public ClientAvatarConfig CurrentConfig
        {
            get
            {
                ClientAvatarConfig config = null;
                switch (muziCharacterType)
                {
                    case MuziCharacterType.Local:
                    {
                        config = supportedAvatarConfigs.FirstOrDefault(cf => cf.BaseAvatar == noneNetworkBaseType);
                        break;
                    }
                    case MuziCharacterType.SubNetwork:
                    case MuziCharacterType.Network:
                    {
                        config = supportedAvatarConfigs.FirstOrDefault(cf =>
                            cf.BaseAvatar.ToString() == currentAvatar.info.baseModel);
                        break;
                    }

                    default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                if (config == null)
                {
                    Debug.LogError("[MuziAvatarMake] There is no current config matching avatar data!!!");
                    throw new Exception("[MuziAvatarMake] There is no current config matching avatar data!!!");
                }

                return config;
            }
        }

        public List<string> CurrentEquippedItems
        {
            get
            {
                if (CurrentConfig == null || CurrentConfig.EquipTracker == null ||
                    CurrentConfig.EquipTracker.ListNewlyEquippeds == null)
                {
                    return null;
                }
                return CurrentConfig.EquipTracker.ListNewlyEquippeds;
            }
        }

        public List<string> CurrentUnequippedItems
        {
            get
            {
                if (CurrentConfig == null || CurrentConfig.EquipTracker == null ||
                    CurrentConfig.EquipTracker.ListNewlyUnequippeds == null)
                {
                    return null;
                }
                return CurrentConfig.EquipTracker.ListNewlyUnequippeds;
            }
        }

        public string CurrentChoiceAvatarId => CurrentConfig.CurrentAvatarId;

        private void Awake()
        {
            CachingLocalItemsReference();
            AutoParentingAtRuntime();
        }

        private IEnumerator Start()
        {
            if (!BuildAvatarFromStart) yield break;
            BuildAvatar();
            yield return null;
        }

        public void SetCharacterType(MuziCharacterType type)
        {
            muziCharacterType = type;
        }

        public void BuildAvatar()
        {
            AutoParentingAtRuntime();
            switch (muziCharacterType)
            {
                case MuziCharacterType.Local:
                    MakeLocalAvatar();
                    return;
                case MuziCharacterType.Network:
                    MakeNetworkAvatar(currentAvatar);
                    return;
                case MuziCharacterType.SubNetwork:
                    var subNetworkPlayer = parentGameObject.GetComponent<NakamaNetworkPlayer>();
                    if (subNetworkPlayer == null)
                    {
                        Debug.LogWarning(
                            "There is no NakamaNetworkPlayer attached => Ignore creating avatar for this character");
                        return;
                    }

                    subNetworkPlayer.OnReceiveUserBasisData = info => HandleSubCharInfo(info, subNetworkPlayer);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleSubCharInfo(UNBufUserInfo info, NakamaNetworkPlayer subNetworkPlayer)
        {
            currentUserInfo = info;
            subNetworkPlayer.OnAvatarChanged = _ => LoadAvatarForSubChar();
            LoadAvatarForSubChar();
        }

        private bool ValidateCurrentAvatarAndClientConfig()
        {
            if (currentAvatar == null) return false;
            var hasNoAvatarItems = currentAvatar.item == null || currentAvatar.item.Count == 0;
            if (hasNoAvatarItems && (currentAvatar.info.baseModel == SupportedAvatarType.Female.ToString() ||
                                     currentAvatar.info.baseModel == SupportedAvatarType.Male.ToString()))
            {
                Debug.LogWarning("<color=red>[MuziAvatarMaker] There is no avatar info</color>");
                return false;
            }

            if (CurrentConfig == null)
            {
                Debug.LogWarning("There is no appropriate local configuration to instantiate an avatar");
                return false;
            }

            CurrentConfig.CurrentAvatarId = currentAvatar.info.avatarId;

            return true;
        }

        private bool HideNetworkOldAvatar()
        {
            var networkPlayer = parentGameObject.GetComponent<NakamaMyNetworkPlayer>();
            if (networkPlayer != null)
            {
                networkPlayer.HideOldAvatar();
                return true;
            }

            Debug.Log("<color=red>There is no NakamaMyNetworkPlayer script attached</color>");
            return false;
        }

        private bool HideSubNetworkOldAvatar()
        {
            var networkPlayer = parentGameObject.GetComponent<NakamaNetworkPlayer>();
            if (networkPlayer != null)
            {
                networkPlayer.HideOldAvatar();
                return true;
            }

            Debug.Log("<color=red>There is no NakamaNetworkPlayer script attached</color>");
            return false;
        }

        private void MakeLocalAvatar()
        {
            if (isNPC)
            {
                var randomConfig = ChooseConfigRandomly(forceDance);
                if (randomConfig == null)
                {
                    Debug.LogError("[MuziAvatarMaker] Creating an NPC but there is no avatar configuration");
                    return;
                }

                if (!randomConfig.FullCharacterPrefab && (randomConfig.AllLocalItemsData == null ||
                                                          randomConfig.AllLocalItemsData.Data == null ||
                                                          randomConfig.AllLocalItemsData.Data.Count < 1))
                {
                    Debug.LogError("[MuziAvatarMaker] There is no local items data to create NPC!");
                    return;
                }

                noneNetworkBaseType = randomConfig.BaseAvatar;
                if (!randomConfig.FullCharacterPrefab)
                {
                    ApplyALocalAvatar(noneNetworkBaseType, RandomItemsSubset(randomConfig.AllLocalItemsData));
                }
                else
                {
                    ApplyALocalAvatar(noneNetworkBaseType);
                }
            }
            else
            {
                ApplyALocalAvatar(noneNetworkBaseType);
            }
        }

        private ClientAvatarConfig ChooseConfigRandomly(bool forceDancing)
        {
            ClientAvatarConfig randomConfig = null;
            if (forceDancing)
            {
                randomConfig = supportedAvatarConfigs.FirstOrDefault(e => e.Danceable);
            }

            if (randomConfig == null)
            {
                randomConfig = supportedAvatarConfigs[UnityEngine.Random.Range(0, supportedAvatarConfigs.Count)];
            }

            return randomConfig;
        }

        private GroupedLocalItemsData RandomItemsSubset(GroupedLocalItemsData itemsData)
        {
            var randItemsData = ScriptableObject.CreateInstance<GroupedLocalItemsData>();
            randItemsData.Data = new List<LocalItemsData>();
            for (int i = 0; i < itemsData.Data.Count; i++)
            {
                // randItemsData.Data.Add();
                var newItems = new LocalItemsData()
                {
                    CategoryCode = itemsData.Data[i].CategoryCode,
                    LocalItems = new List<LocalItem>()
                };
                if (itemsData.Data[i].LocalItems.Count > 1)
                {
                    var count = itemsData.Data[i].LocalItems.Count;
                    newItems.LocalItems.Add(itemsData.Data[i].LocalItems[UnityEngine.Random.Range(0, count)]);
                }
                else
                {
                    newItems.LocalItems.Add(itemsData.Data[i].LocalItems[0]);
                }

                randItemsData.Data.Add(newItems);
            }

            randItemsData.Data = randItemsData.Data.OrderBy(e => e.CategoryCode).ToList();
            return randItemsData;
        }


        private void AutoParentingAtRuntime()
        {
            if (parentGameObject == null)
            {
                if (gameObject.transform.parent != null)
                {
                    Debug.LogWarning("[MuziAvatarMaker] Auto assign parent game object at runtime!");
                    parentGameObject = gameObject.transform.parent.gameObject;
                }
                else
                {
                    var parent = new GameObject("MuziCharacterHolder");
                    parent.transform.position = gameObject.transform.position;
                    gameObject.transform.SetParent(parent.transform);
                    parentGameObject = parent;
                    Debug.LogWarning("[MuziAvatarMaker] Create and assign parent game object at runtime!");
                }
            }

            _parentGoCharacterLocomotion = parentGameObject.GetComponent<UltimateCharacterLocomotion>();
            _parentGoAnimator = parentGameObject.GetComponent<Animator>();
            if (_parentGoAnimator == null)
            {
                _parentGoAnimator = parentGameObject.AddComponent<Animator>();
            }
        }

        private void CachingLocalItemsReference()
        {
            foreach (var cfg in supportedAvatarConfigs)
            {
                if (cfg.AllLocalItemsData != null && cfg.AllLocalItemsData.Data != null)
                {
                    foreach (var localItemsData in cfg.AllLocalItemsData.Data)
                    {
                        foreach (var localItem in localItemsData.LocalItems)
                        {
                            if (_allLocalItems.ContainsKey(localItem.ExternalId)) continue;
                            _allLocalItems.Add(localItem.ExternalId, localItem);
                        }
                    }
                }
            }
        }

        private async void LoadAvatarForSubChar()
        {
            if (currentUserInfo == null)
            {
                Debug.LogWarning(
                    "Expected current sub character info is not null but got null, cannot load sub character avatar");
                return;
            }

            var avt = await AvatarServices.Inst.GetCurrentAvatarByUserID(currentUserInfo.MuziUserId);
            MakeSubAvatar(avt);
        }

        private List<LocalItemsData> GetLocalItemsData(SupportedAvatarType avatarType)
        {
            var cfg = supportedAvatarConfigs.FirstOrDefault(e => e.BaseAvatar == avatarType);
            if (cfg == null)
            {
                Debug.LogError($"[MuziAvatarMaker] There is no client config for {avatarType}");
                return null;
            }

            if (cfg.AllLocalItemsData == null)
            {
                Debug.LogWarning($"Avatar Config of {avatarType} has no local ItemData");
                return null;
            }

            if (cfg.AllLocalItemsData.Data == null || cfg.AllLocalItemsData.Data.Count < 1)
            {
                Debug.LogWarning($"Local Item Data of Avatar Config of {avatarType} has no data");
                return null;
            }

            return cfg.AllLocalItemsData.Data;
        }

        public List<SItemRequest> GetInitializedEquippedItems(SupportedAvatarType avatarType,
            out List<SItemRequest> initializedUnequippedItems)
        {
            var listLocalItemsData = GetLocalItemsData(avatarType);
            initializedUnequippedItems = new List<SItemRequest>();
            if (listLocalItemsData == null || listLocalItemsData.Count < 1) return null;

            var listItems = new List<SItemRequest>();
            foreach (var localItemsData in listLocalItemsData)
            {
                foreach (var localItem in localItemsData.LocalItems)
                {
                    if (localItem.Owned && localItem.Equipped)
                    {
                        listItems.Add(new SItemRequest()
                        {
                            ItemExternalId = localItem.ExternalId
                        });
                    }
                    else if (localItem.Owned && !localItem.Equipped)
                    {
                        initializedUnequippedItems.Add(new SItemRequest()
                        {
                            ItemExternalId = localItem.ExternalId
                        });
                    }
                }
            }

            return listItems;
        }
        

        public void MakeNetworkAvatar(UserAvatar avt)
        {
            if (!HideNetworkOldAvatar()) return;
            _makeNetworkAvt(avt);
        }

        public void MakeAvatarForServer(UserAvatar avt)
        {
            _makeNetworkAvt(avt);
        }

        private void _makeNetworkAvt(UserAvatar avt, bool isSub = false)
        {
            currentAvatar = avt;
            if (!ValidateCurrentAvatarAndClientConfig()) return;
            AssignRuntimeReferenceToCurrentConfig();
            HideOtherConfigGameObject(ToSupportType(avt.info.baseModel));
            Debug.Log("Loading server avatar for my network player");
            CoroutineHandler.StartStaticCoroutine(RefreshAnimController());
            
            
            //HACK ANHNGUYEN: special treat for developing character specificly 
            // ANh nguyen please remove it when art model is fully usable
            if (CurrentConfig.BaseAvatar == SupportedAvatarType.Male04 ||
                CurrentConfig.BaseAvatar == SupportedAvatarType.Female02)
            {
                CurrentConfig.AvatarGameObjectRef.SetActive(true);
                if (muziCharacterType == MuziCharacterType.Network && CurrentConfig.AvatarGameObjectRef.transform.parent != null && prevCharacterPos != Vector3.zero &&
                    SceneManager.GetActiveScene().name == "Main")
                {
                    prevCharacterPos =
                        prevCharacterPos +
                        new Vector3(0f, 0.4f, 0f); // a bit higher than ground to avoid going through ground
                    var parent = CurrentConfig.AvatarGameObjectRef.transform.parent;
                    parent.position = prevCharacterPos;
                    parent.rotation = prevCharacterRot;
                }
                FullscreenLoading.HideLoading();
                return;
            }

            
            
            // CoroutineHandler.StartStaticCoroutine(ApplyAvatar(currentAvatar, isSub: isSub));
            ApplyAvatar(currentAvatar, isSub: isSub);
        }

        private void MakeSubAvatar(UserAvatar avt)
        {
            if (!HideSubNetworkOldAvatar()) return;
            _makeNetworkAvt(avt, isSub: true);
        }

        SupportedAvatarType ToSupportType(string baseModel)
        {
            return (SupportedAvatarType) Enum.Parse(typeof(SupportedAvatarType), baseModel, true);
        }


        public void ApplyALocalAvatar(SupportedAvatarType baseAvatar, GroupedLocalItemsData itemsData = null)
        {
            CoroutineHandler.StartStaticCoroutine(_ApplyALocalAvatar(baseAvatar, itemsData));
        }

        private IEnumerator _ApplyALocalAvatar(SupportedAvatarType baseAvatar, GroupedLocalItemsData optionalItemData = null)
        {
            // if (itemsData == null)
            // {
            //     FullscreenLoading.ShowLoading($"Loading {baseAvatar} Avatar", true);
            // }
            HideOtherConfigGameObject(baseAvatar);
            yield return new WaitForEndOfFrame();
            AssignRuntimeReferenceToCurrentConfig();
            yield return new WaitForEndOfFrame();
            CoroutineHandler.StartStaticCoroutine(RefreshAnimController());
            var cf = supportedAvatarConfigs.FirstOrDefault(cf => cf.BaseAvatar == baseAvatar);

            if (!CurrentConfig.FullCharacterPrefab)
            {
                var itemData = optionalItemData != null ? optionalItemData : CurrentConfig.AllLocalItemsData;

                if (itemData != null)
                {
                    bool isSetEquipped = false;
                    foreach (var cat in itemData.Data)
                    {
                        var item = new Item() // first item of each category
                        {
                            categoryCode = cat.LocalItems.First().CategoryCode,
                            externalId = cat.LocalItems.First().ExternalId
                        };

                        // don't equip accessory
                        if (cat.CategoryCode == CategoryCode.FASHION_ACCESSORIES) continue;
                        // ignore hat for main character
                        if (cat.CategoryCode == CategoryCode.FASHION_HAT && !isNPC) continue;
                        // no fashion_instrument
                        if (cat.CategoryCode == CategoryCode.FASHION_INSTRUMENT) continue;


                        // random fullset with 30% rate for NPC
                        if (isNPC && cat.CategoryCode == CategoryCode.FASHION_FULLSET && !isSetEquipped)
                        {
                            if (UnityEngine.Random.Range(0, 10) < 3) // 30 % of FULLSET
                            {
                                InitNewItem(item, cf);
                                isSetEquipped = true;
                                continue;
                            }
                        }

                        // try to equip the item set by default in avatar
                        bool equippingDone = false;
                        foreach (var localItem in cat.LocalItems)
                        {
                            if (localItem.Equipped)
                            {
                                InitNewItem(localItem.ToItem(), cf);
                                equippingDone = true;
                                break;
                            }
                        }

                        if (!equippingDone)
                        {
                            InitNewItem(item, cf);
                        }
                    }
                }
            }

            if (parentGameObject != null && parentGameObject.GetComponent<Animator>() != null)
            {
                parentGameObject.GetComponent<Animator>().runtimeAnimatorController = CurrentConfig.AnimController;
            }

            yield return new WaitForEndOfFrame();
            CurrentConfig.AvatarGameObjectRef.SetActive(true);
            if (parentGameObject != null)
            {
                var characterIK = parentGameObject.GetComponent<CharacterIK>();
                CoroutineHandler.StartStaticCoroutine(RefreshAvatarBaseGameObject(characterIK));
            }

            CoroutineHandler.StartStaticCoroutine(delayHide());
        }

        IEnumerator delayHide()
        {
            yield return new WaitForSeconds(1.5f);
            FullscreenLoading.HideLoading();
        }

        void AssignRuntimeReferenceToCurrentConfig()
        {
            // instantiate object
            if (CurrentConfig.AvatarGameObjectRef == null)
            {
                // networkPlayerGO.GetComponent<Animator>().enabled = false;
                var go = Instantiate(CurrentConfig.BaseAvatarPrefab, Vector3.zero, Quaternion.identity,
                    parentGameObject.transform);
                // go.SetActive(false);
                // networkPlayerGO.GetComponent<Animator>().enabled = false;
                // go.GetComponent<Animator>().enabled = false;
                CurrentConfig.AvatarGameObjectRef = go;
                CurrentConfig.AvatarGameObjectRef.SetActive(false);
            }

            var configAnimator = CurrentConfig.AvatarGameObjectRef.GetComponent<Animator>();
            if (configAnimator != null && _parentGoAnimator != null)
            {
                _parentGoAnimator.avatar = configAnimator.avatar;
                configAnimator.enabled = false;
            }

            if (_parentGoCharacterLocomotion != null)
            {
                _parentGoCharacterLocomotion.enabled = false;
            }

            CurrentConfig.AvatarGameObjectRef.transform.localPosition = Vector3.zero;
            CurrentConfig.AvatarGameObjectRef.transform.localRotation = Quaternion.identity;
            // Assign rootbone
            if (CurrentConfig.RootBone == null)
            {
                CurrentConfig.RootBone =
                    CurrentConfig.AvatarGameObjectRef.transform.FindDeepChild("Root_M",
                        StringComparisonMode.RightEqualsLeft,
                        true);
            }

            if (parentGameObject != null)
            {
                var footEffect = parentGameObject.GetComponent<CharacterFootEffects>();
                if (footEffect != null && CurrentConfig.RootBone != null)
                {
                    footEffect.enabled = false;
                    footEffect.Feet[0].Object =
                        CurrentConfig.RootBone.FindDeepChild("Toes_L", StringComparisonMode.LeftContainsRight, true);
                    footEffect.Feet[1].Object =
                        CurrentConfig.RootBone.FindDeepChild("Toes_R", StringComparisonMode.LeftContainsRight, true);
                    footEffect.enabled = true;
                }
            }

            if (_parentGoCharacterLocomotion != null)
            {
                _parentGoCharacterLocomotion.enabled = true;
            }

            // neu ko co back head thi add back head
            var backHeadRenderer = CurrentConfig.AvatarGameObjectRef.transform.FindDeepChild("_backhead",
                StringComparisonMode.LeftContainsRight,
                true);
            if (backHeadRenderer != null)
            {
                CurrentConfig.backHeadMesh = backHeadRenderer.GetComponent<SkinnedMeshRenderer>();
            }
        }

        void HideOtherConfigGameObject(SupportedAvatarType baseAvatar)
        {
            foreach (var otherCf in supportedAvatarConfigs)
            {
                if (otherCf.BaseAvatar != baseAvatar && otherCf.AvatarGameObjectRef != null)
                {
                    otherCf.AvatarGameObjectRef.SetActive(false);
                }
            }
        }

        public void ShowHideConfigAvatarReference()
        {
            foreach (var config in supportedAvatarConfigs)
            {
                if (config.AvatarGameObjectRef != null)
                {
                    config.AvatarGameObjectRef.SetActive(config.BaseAvatar.ToString() == currentAvatar.info.baseModel);
                }
            }
        }

        IEnumerator RefreshAvatarBaseGameObject(CharacterIK characterIK)
        {
            // if (characterIK != null) characterIK.Refresh();
            parentGameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            parentGameObject.SetActive(true);
// #if MUZIVERSE_MAIN
            if (characterIK != null) characterIK.InitializeBones(fromAwake: false, force: true);
// #endif
        }

        void ApplyAvatar(UserAvatar avatar, bool isSub = false)
        {
            var avatarGameObject = CurrentConfig.AvatarGameObjectRef;
            avatarGameObject.SetActive(false);
            avatar.item = avatar.item.OrderBy(e => (int)e.itemData.categoryCode.ToCategoryCode()).ToList();
            
            // CurrentConfig.Initialize(equipRuleData, HairHasHeadSkin, );

            MonitorLoadingAvatar(avatar, avatarGameObject);

            if (avatar.item == null)
            {
                Debug.LogError($"[MuziAvatarMaker] Avatar {avatar} doesn't has item");
            }
            else
            {
                UserItem fullSet = null;
                foreach (var i in avatar.item)
                {
                    if (i.itemData.categoryCode == CategoryCode.FASHION_FULLSET.ToString())
                    {
                        fullSet = i;
                        continue;
                    }

                    InitNewItem(i.itemData, CurrentConfig);
                    // yield return new WaitForEndOfFrame();
                }

                if (fullSet != null) // apply full set later
                {
                    InitNewItem(fullSet.itemData, CurrentConfig);
                    // yield return new WaitForEndOfFrame();
                }
            }
            
            if (CurrentConfig != null)
            {
                List<string> listServerItems = new List<string>();
                foreach (var item in currentAvatar.item)
                {
                    listServerItems.Add(item.itemData.externalId);
                }
                CurrentConfig.SetListServerItem(listServerItems);
            }
        }

        private void MonitorLoadingAvatar(UserAvatar avatar, GameObject avatarGameObject)
        {
            _trackingLoadingStates.Clear();
            foreach (var item in avatar.item)
            {
                if (!_trackingLoadingStates.ContainsKey(item.itemData.externalId))
                {
                    _trackingLoadingStates.Add(item.itemData.externalId, false);
                }
            }

            CoroutineHandler.StartStaticCoroutine(WaitingApplyAvatarDone(avatarGameObject));
        }

        private IEnumerator RefreshAnimController()
        {
            parentGameObject.GetComponent<Animator>().runtimeAnimatorController = CurrentConfig.AnimController;
            var characterIK = parentGameObject.GetComponent<CharacterIK>();
            yield return RefreshAvatarBaseGameObject(characterIK);
        }

        private IEnumerator WaitingApplyAvatarDone(GameObject avatarBaseGO)
        {
            yield return new WaitUntil(() =>
                _trackingLoadingStates.All(e => e.Value) || _trackingLoadingStates.Count == 0);

            if (muziCharacterType == MuziCharacterType.Network && avatarBaseGO.transform.parent != null && prevCharacterPos != Vector3.zero &&
                SceneManager.GetActiveScene().name == "Main")
            {
                prevCharacterPos =
                    prevCharacterPos +
                    new Vector3(0f, 0.4f, 0f); // a bit higher than ground to avoid going through ground
                var parent = avatarBaseGO.transform.parent;
                parent.position = prevCharacterPos;
                parent.rotation = prevCharacterRot;
            }

            yield return RefreshAnimController();

            // spawn effect here
            // yield return new WaitForEndOfFrame();
            avatarBaseGO.SetActive(true);
            // yield return new WaitForEndOfFrame();
            // Debug.Log("<color=red>APPLY AVATAR DONE</color>");
            OnApplyAvatarDone?.Invoke();
        }

        /// <summary>
        /// Change item apply for a local item, not item from server
        /// </summary>
        /// <param name="localItem"></param>
        /// <param name="avatarType"></param>
        public void ChangeLocalItem(LocalItem localItem, SupportedAvatarType avatarType)
        {
            noneNetworkBaseType = avatarType;
            var cf = supportedAvatarConfigs.FirstOrDefault(cf => cf.BaseAvatar == noneNetworkBaseType);
            var item = new Item()
            {
                categoryCode = localItem.CategoryCode,
                externalId = localItem.ExternalId
            };
            EquipNewItem(item, cf);
        }

        public void UnequipItem(UserItem unequippedItem)
        {
            CurrentConfig.Initialize(equipRuleData, specialHairs, _allLocalItems);
            CurrentConfig.ItemHolder.SetItemInstancer(ItemGameObjectInstancer);
            CurrentConfig.ItemHolder.AddEquipChangeListener(FireEquipStateChanges);
            CurrentConfig.ItemHolder.UnEquipItem(unequippedItem.itemData);
            CurrentConfig.ItemHolder.RemoveEquipChangeLister(FireEquipStateChanges);
        }

        private void FireEquipStateChanges(Dictionary<string, EquipStateChanges> obj)
        {
            OnEquipStateChange?.Invoke(obj);
        }

        public void ChangeUserItem(UserItem newUserItem)
        {
            _trackingLoadingStates.Clear();
            var newItem = newUserItem.itemData;

            EquipNewItem(newItem, CurrentConfig);

            var changeWearableEffect = CurrentConfig.AvatarGameObjectRef.GetComponents<IChangeWearableEffect>();
            foreach (var ef in changeWearableEffect)
            {
                ef?.DoEffect();
            }
        }

        private void EquipNewItem(Item newItem, ClientAvatarConfig cf)
        {
            cf.Initialize(equipRuleData, specialHairs, _allLocalItems);
            cf.ItemHolder.AddEquipChangeListener(FireEquipStateChanges);
            cf.ItemHolder.SetItemInstancer(ItemGameObjectInstancer);
            cf.ItemHolder.EquipItem(newItem);
            if (_trackingLoadingStates.ContainsKey(newItem.externalId))
            {
                _trackingLoadingStates[newItem.externalId] = true;
            }
            
            cf.ItemHolder.RemoveEquipChangeLister(FireEquipStateChanges);
        }

        private void InitNewItem(Item newItem, ClientAvatarConfig cf)
        {
            // var inScopeItem = newItem;
            cf.Initialize(equipRuleData, specialHairs, _allLocalItems);
            cf.ItemHolder.SetItemInstancer(ItemGameObjectInstancer);
            cf.ItemHolder.InitItem(newItem);
            cf.ItemHolder.OnDone = _ =>
            {
                if (_trackingLoadingStates.ContainsKey(newItem.externalId))
                {
                    _trackingLoadingStates[newItem.externalId] = true;
                }
            };
        }


        private void ItemGameObjectInstancer(Item item, Action<ItemGameObject> cb)
        {
            CharacterAssetLoader.Instance.SetCurrentMaterials(CurrentConfig.BaseMaterial, CurrentConfig.BodyMaterial,
                CurrentConfig.FaceMaterial, CurrentConfig.BackHeadMaterial, CurrentConfig.EyebrowMaterial);
            CharacterAssetLoader.Instance.SetBackHeadMesh(CurrentConfig.backHeadMesh);
            var localItem = GetLocalItem(item);
            if (localItem == null || localItem.AlwaysFetchFromServer)
            {
                CharacterAssetLoader.Instance.InstantiateFbxFromDeviceStorageV2(item, CurrentConfig.RootBone, cb);
            }
            else
            {
                var firstOrDefault = localItem.Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.Fbx);
                // Always from prefer the local prefab if any
                if (firstOrDefault != null && firstOrDefault.FbxPrefab != null)
                {
                    var go = CharacterAssetLoader.GetGameObjectFromPool(localItem, string.Empty,
                        firstOrDefault.FbxPrefab);

                    cb?.Invoke(go);
                    return;
                }

                CharacterAssetLoader.Instance.InstantiateFbxFromResourceV2(localItem, CurrentConfig.RootBone, cb);
            }
        }


        public LocalItem GetLocalItem(Item item) =>
            !_allLocalItems.ContainsKey(item.externalId) ? null : _allLocalItems[item.externalId];

        public void ClearSameCategory(string categoryCode, SupportedAvatarType avatarType)
        {
            var cf = supportedAvatarConfigs.FirstOrDefault(cf => cf.BaseAvatar == avatarType);
            if (cf == null) return;
        }

        public void SetPrevCharacterPos(Vector3 pos)
        {
            prevCharacterPos = pos;
        }

        public void SetPrevCharacterRot(Quaternion rot)
        {
            prevCharacterRot = rot;
        }
    }

    public enum MuziCharacterType
    {
        Local, // NPC, or just local player without networking feature
        Network, // myself in the world with networking
        SubNetwork // other sub character in my world
    }
}