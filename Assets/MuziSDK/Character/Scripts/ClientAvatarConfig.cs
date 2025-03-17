using System;
using System.Collections.Generic;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    /// <summary>
    /// Việc tạo được avatar dạng nào là do client Unity hỗ trợ
    /// Khi người chơi start 1 version client thì sẽ đọc thông tin từ base avatar option này lên để hiện UI xem thử
    /// có thể tạo avatar gender là gì. Server chỉ lưu thông tin avatar
    /// </summary>
    [Serializable]
    public class ClientAvatarConfig
    {
        [Header("Client Definition")] public SupportedAvatarType BaseAvatar;

        public GameObject BaseAvatarPrefab;
        public bool FullCharacterPrefab = false;

        [Header("Runtime set (or from scene) reference")]
        public Transform RootBone;

        public GameObject AvatarGameObjectRef;
        public SkinnedMeshRenderer backHeadMesh;

        [Header("Some other config data")] public RuntimeAnimatorController AnimController;

        public GroupedLocalItemsData AllLocalItemsData;

        [Header("Dancing supported")] public bool Danceable = false;

        [Header("Mannequin")] public bool IsMannequine = false;
        public RuntimeAnimatorController MannequinPoseAnimController;


        [Tooltip("The material base used to instantiate runtime material")]
        public Material BaseMaterial;

        [Tooltip("The common material used for face mesh like nose, cheek ...")]
        public Material FaceMaterial;

        [Tooltip("The material used for eyesbrow")]
        public Material EyebrowMaterial;

        [Tooltip("The common material used for other skin part like 2nd part of shirts, pants and shoes")]
        public Material BodyMaterial;

        [Tooltip("The back head material used when changing hair")]
        public Material BackHeadMaterial;

        public EquipTracker EquipTracker;


        private bool initialized = false;

        [Header("New POOLING and Equipped state tracker")]
        public ItemHolder ItemHolder;

        public string CurrentAvatarId { get; set; }

        public void SetListServerItem(List<string> serverItemExternals)
        {
            if (EquipTracker != null)
            {
                EquipTracker.SetListEquippedItemFromServer(serverItemExternals);
            }
        }

        public List<LocalItem> DefaultItems
        {
            get
            {
                if (_defaultItems.Count < 1)
                {
                    PrepareItemsDataForUsage();
                }
                return _defaultItems;
            }
        }

        public Dictionary<CategoryCode, List<LocalItem>> CategorizedDefaultItems
        {
            get
            {
                if (_categorizedDefaultItems.Count < 1)
                {
                    PrepareItemsDataForUsage();
                }

                return _categorizedDefaultItems;
            }
        }
        
        public Dictionary<CategoryCode, List<LocalItem>> CategorizedEquipItems
        {
            get
            {
                if (_categorizedEquippedItems.Count < 1)
                {
                    PrepareItemsDataForUsage();
                }

                return _categorizedEquippedItems;
            }
        }
        
        public Dictionary<CategoryCode, List<LocalItem>> CategorizedOwnedItems
        {
            get
            {
                if (_categorizedOwnedItems.Count < 1)
                {
                    PrepareItemsDataForUsage();
                }

                return _categorizedOwnedItems;
            }
        }

        public List<LocalItem> OwnedItems
        {
            get
            {
                if (_ownedItems.Count < 1)
                {
                    PrepareItemsDataForUsage();
                }
                return _ownedItems;
            }
        }

        public List<LocalItem> EquippedItems
        {
            get
            {
                if (_equippedItems.Count < 1)
                {
                    PrepareItemsDataForUsage();
                }
                return _equippedItems;
            }
        }

        public void Initialize(CategoryEquipRuleData equipRule, HairHasHeadSkin hairSpecial, Dictionary<string, LocalItem> localItems)
        {
            if (initialized) return;
            var listDefaultItems = new List<string>();
            
            foreach (var localItem in localItems)
            {
                if (localItem.Value.IsDefault && localItem.Value.BaseType == BaseAvatar.ToString() )
                {
                    listDefaultItems.Add(localItem.Value.ToItem().UniqueItemKey);
                }
            }
            
            EquipTracker = new EquipTracker(equipRule, listDefaultItems);
            
            ItemHolder = new ItemHolder(EquipTracker, AvatarGameObjectRef.transform, RootBone, hairSpecial, localItems);
            initialized = true;
        }

        public bool HasNoLocalItems => AllLocalItemsData == null || AllLocalItemsData.Data == null || AllLocalItemsData.Data.Count < 1;

        private List<LocalItem> _defaultItems = new List<LocalItem>();
        private List<LocalItem> _ownedItems = new List<LocalItem>();
        private List<LocalItem> _equippedItems = new List<LocalItem>();
        
        private Dictionary<CategoryCode, List<LocalItem>> _categorizedDefaultItems = new Dictionary<CategoryCode, List<LocalItem>>();
        private Dictionary<CategoryCode, List<LocalItem>> _categorizedOwnedItems = new Dictionary<CategoryCode, List<LocalItem>>();
        private Dictionary<CategoryCode, List<LocalItem>> _categorizedEquippedItems = new Dictionary<CategoryCode, List<LocalItem>>();

        void PrepareItemsDataForUsage()
        {
            if (AllLocalItemsData == null || AllLocalItemsData.Data == null ||
                AllLocalItemsData.Data.Count < 1)
            {
                return;
            }
            foreach (var localItemsData in AllLocalItemsData.Data)
            {
                foreach (var lcItem in localItemsData.LocalItems)
                {
                    if (lcItem.IsDefault)
                    {
                        // list<localitem>
                        if (!_defaultItems.Contains(lcItem))
                        {
                            _defaultItems.Add(lcItem);
                        }

                        if (!_ownedItems.Contains(lcItem))
                        {
                            _ownedItems.Add(lcItem);
                        }
                        
                        // DIctionary<CatgegoryCode,List<LocalItem>>
                        if (!_categorizedDefaultItems.ContainsKey(localItemsData.CategoryCode))
                        {
                            _categorizedDefaultItems.Add(localItemsData.CategoryCode, new List<LocalItem>(){ lcItem});
                        }
                        else if (!_categorizedDefaultItems[localItemsData.CategoryCode].Contains(lcItem))
                        {
                            _categorizedDefaultItems[localItemsData.CategoryCode].Add(lcItem);
                        }
                        
                        if (!_categorizedOwnedItems.ContainsKey(localItemsData.CategoryCode))
                        {
                            _categorizedOwnedItems.Add(localItemsData.CategoryCode, new List<LocalItem>(){ lcItem});
                        }
                        else if (!_categorizedOwnedItems[localItemsData.CategoryCode].Contains(lcItem))
                        {
                            _categorizedOwnedItems[localItemsData.CategoryCode].Add(lcItem);
                        }
                    }
                    
                    if (lcItem.Equipped)
                    {
                        if (!_equippedItems.Contains(lcItem))
                        {
                            _equippedItems.Add(lcItem);
                        }

                        if (!_ownedItems.Contains(lcItem))
                        {
                            _ownedItems.Add(lcItem);
                        }
                        
                        if (!_categorizedEquippedItems.ContainsKey(localItemsData.CategoryCode))
                        {
                            _categorizedEquippedItems.Add(localItemsData.CategoryCode, new List<LocalItem>(){ lcItem});
                        }
                        else if (!_categorizedEquippedItems[localItemsData.CategoryCode].Contains(lcItem))
                        {
                            _categorizedEquippedItems[localItemsData.CategoryCode].Add(lcItem);
                        }
                        
                        if (!_categorizedOwnedItems.ContainsKey(localItemsData.CategoryCode))
                        {
                            _categorizedOwnedItems.Add(localItemsData.CategoryCode, new List<LocalItem>(){ lcItem});
                        }
                        else if (!_categorizedOwnedItems[localItemsData.CategoryCode].Contains(lcItem))
                        {
                            _categorizedOwnedItems[localItemsData.CategoryCode].Add(lcItem);
                        }
                    }

                    if (lcItem.Owned)
                    {
                        if (!_ownedItems.Contains(lcItem))
                        {
                            _ownedItems.Add(lcItem);
                        }
                        
                        if (!_categorizedOwnedItems.ContainsKey(localItemsData.CategoryCode))
                        {
                            _categorizedOwnedItems.Add(localItemsData.CategoryCode, new List<LocalItem>(){ lcItem});
                        }
                        else if (!_categorizedOwnedItems[localItemsData.CategoryCode].Contains(lcItem))
                        {
                            _categorizedOwnedItems[localItemsData.CategoryCode].Add(lcItem);
                        }
                    }
                }
            }
        } 
    }
}