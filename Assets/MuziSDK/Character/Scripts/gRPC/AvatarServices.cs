using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MuziCharacter.DataModel;
using Muziverse.Proto.Avatar.Api.Info;
using Muziverse.Proto.User.Api.User;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enum = System.Enum;

namespace MuziCharacter
{
    public class AvatarServices : BaseService
    {
        
        private static AvatarServices _instance;

        public static AvatarServices Inst
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (new GameObject("grpcAvatarServices")).AddComponent<AvatarServices>();
                }

                return _instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }

        public SupportedAvatarType CurrentAvatarType => (SupportedAvatarType) Enum.Parse(typeof(SupportedAvatarType), CurrentAvatarInfo.baseModel);
        

#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task TestCreateAvatarWithoutCustomization(string baseModel, SGender gender, string avatarName)
        {
            var itemRequests = new List<SItemRequest>();
            itemRequests.Add(new SItemRequest()
            {
                ItemExternalId = "snorkel-mask-red-7668741"
            });

            await CreateAvatar(baseModel, gender, avatarName, itemRequests);
        }

#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task TestCreateAvatarWithCustomization(string baseModel, SGender gender, string avatarName)
        {
            var itemRequests = new List<SItemRequest>();
            itemRequests.Add(new SItemRequest()
            {
                ItemExternalId = "snorkel-mask-red-7668741",
                Customization = new SStruct()
                {
                    Fields = new List<KeyValue>()
                    {  
                        new KeyValue()
                        {
                            Key = "Color",
                            Value = "Red"
                        }
                        // new {"Color", "Red"},
                        // {"TextureId", 3},
                        // {"HasDecal", true}
                    }
                }
            });

            await CreateAvatar(baseModel, gender, avatarName, itemRequests);
        }
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> CreateAvatar(string baseModel, SGender gender, string avatarName, List<SItemRequest> initializedEquippedItems, bool isCurrent = false)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            try
            {
                var client = new AvatarService.AvatarServiceClient(Channel);
                var avatarRequest = new CreateAvatarRequest();
                avatarRequest.Info = new AvatarInfoCreate();
                avatarRequest.Info.BaseModel = baseModel;
                avatarRequest.Info.Gender = gender.ToGender();
                avatarRequest.Info.NickName = avatarName;
                if (initializedEquippedItems != null && initializedEquippedItems.Count > 0)
                {
                    foreach (var rq in initializedEquippedItems)
                    {
                        avatarRequest.Item.Add(rq.ToItemRequest());
                    }
                }

                LogRequest(GetType()+".CreateAvatarAsync", avatarRequest.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.CreateAvatarAsync(avatarRequest, Metadata);
                var response = await request;
                await UpdateAvatarAsync(response.AvatarId, new List<long>(), new List<long>(), newIsCurrent: true);
                LogResponse(GetType()+".CreateAvatarAsync", response.ToString());
                return true;
            }
            catch (RpcException e)
            {
                Debug.LogException(e);
                return false;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        [SerializeField] private UserAvatar _avatarResponse;
        public UserAvatar CurrentAvatar => _avatarResponse;
        public List<UserAvatar> AllAvatars => allAvatar;
        private  List<UserAvatar> allAvatar;
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<UserAvatar> GetAvatarByIdAsync(string avatarId)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return null;
            }
            try
            {
                var client = new AvatarService.AvatarServiceClient(Channel);
                var requestParam = new GetAvatarByIdRequest();
                requestParam.AvatarId = avatarId;
               
                LogRequest(GetType()+".GetAvatarByIdAsync", requestParam.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.GetAvatarByIdAsync(requestParam, Metadata);
                var response = await request;

                LogResponse(GetType()+".GetAvatarByIdAsync", response.ToString());
                
                var uv = await Task.Run(() => JsonConvert.DeserializeObject<UserAvatar>(response.ToString()));
                if (uv.item == null) uv.item = new List<UserItem>();
                
                // add owned to easy UI display
                if (uv != null && uv.item != null)
                {
                    for (int i = 0; i < uv.item.Count; i++)
                    {
                        uv.item[i].isOwned = true;
                    }
                }

                return uv;
            }
            catch (RpcException e)
            {
                Debug.LogException(e);
                TW.I.AddWarning("Error", e.Status.StatusCode+ "\n" + e.Status.Detail);
                return null;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                TW.I.AddWarning("Error", e.Message);
                return null;
            }
        }
        
        public async Task<UserAvatar> GetCurrentAvatarByUserID(string userId)
        {
            var client = new AvatarService.AvatarServiceClient(FoundationManager.channel);
            try
            {
                Debug.Log("<color=red>Get info of sub character with userId {userId}!</color>");
                var rq = new QueryListAvatarCurrentByUserIdRequest()
                {
                    UserIds = {userId}
                };
                // LogRequest(GetType() + ".GetAvatarsAsync", rq.ToString());
                // LogMetadataAndChannel(Metadata, Channel);
                var response = await client.QueryListAvatarCurrentByUserIdAsync(rq, FoundationManager.metadata);

                // LogResponse(GetType()+".GetAvatarsAsync", response.ToString());
                return JsonConvert.DeserializeObject<UserAvatar>(response.Data[0].ToString());
            }
            catch (RpcException e)
            {
                Debug.Log("<color=red>Get avatar of sub character with userId {userId} got exception</color>");
                Debug.LogException(e);
                return null;
            }
        }

        [SerializeField]
        private GetAvatarsResult avatarInfos = null;
        
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> GetAvatarsAsync()
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                // await ReAuthenticate();
                await Task.Delay(TimeSpan.FromSeconds(1f));
                return false;
            }
            try
            {
                var client = new AvatarService.AvatarServiceClient(Channel);
                var rq = new Empty();
                
                LogRequest(GetType() + ".GetAvatarsAsync", rq.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.GetAvatarsAsync(rq, Metadata);
                
                var response = await request;
                

                LogResponse(GetType()+".GetAvatarsAsync", response.ToString());

                avatarInfos = await Task.Run(() => JsonConvert.DeserializeObject<GetAvatarsResult>(response.ToString()));

                // there is no current avatar => set current avatar is the first one
                if (HasAvatarInfo)
                {
                    if (CurrentAvatarInfo == null)
                    {
                        await UpdateAvatarAsync(avatarInfos.avatars.First().avatarId, new List<long>(),new List<long>(), true);
                    }
                    allAvatar = new List<UserAvatar>();
                    foreach (var avatarInfo in avatarInfos.avatars)
                    {
                       var userAvt = await GetAvatarByIdAsync(avatarInfo.avatarId);
                       if (userAvt == null || userAvt.info == null)
                       {
                           continue;
                       }
                       if (userAvt.info.isCurrent)
                       {
                           _avatarResponse = userAvt;
                       }
                       
                       allAvatar.Add(userAvt);
                    }
                    
                }

                return true;
            }
            catch (RpcException e)
            {
                Debug.LogWarning($"Get avatarasync got error {e}");
          
                if (e.StatusCode == StatusCode.Unauthenticated)
                {
                    await ReAuthenticate();
                    await Task.Delay(TimeSpan.FromSeconds(1f));
                    return await GetAvatarsAsync();
                } 
                else if (e.StatusCode == StatusCode.InvalidArgument)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1f));
                    return await GetAvatarsAsync();
                }
                else
                {
                    Debug.LogException(e);
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }
        
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task QueryListAvatarCurrentByUserIdAsync(List<string> userIds)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return;
            }
            try
            {
                var client = new AvatarService.AvatarServiceClient(Channel);
                var requestParam = new QueryListAvatarCurrentByUserIdRequest();
                requestParam.UserIds.AddRange(userIds.ToRepeatedField());
                
                LogRequest(GetType()+".QueryListAvatarCurrentByUserIdAsync", requestParam.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.QueryListAvatarCurrentByUserIdAsync(requestParam, Metadata);
                var response = await request;
                
                LogResponse(GetType()+".QueryListAvatarCurrentByUserIdAsync", response.ToString());
            }
            catch (RpcException e)
            {
                Debug.LogException(e);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> UpdateAvatarAsync(string avatarId, List<long> equippedItemIds, 
            List<long> unequippedItemIds, bool newIsCurrent, string newName = "", string newBaseModel = "")
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            try
            {
                var client = new AvatarService.AvatarServiceClient(Channel);
                var requestParam = new UpdateAvatarRequest();

                requestParam.AvatarId = avatarId;
                
                requestParam.Info = new UpdateAvatarRequest.Types.AvatarInfoUpdate();
                if (!string.IsNullOrEmpty(newName))
                {
                    requestParam.Info.NickName = newName;
                }
                if (!string.IsNullOrEmpty(newBaseModel))
                {
                    requestParam.Info.BaseModel = newBaseModel;
                }
                requestParam.Info.IsCurrent = newIsCurrent;

                requestParam.EquipmentItemIds.AddRange(equippedItemIds.ToRepeatedField());
                requestParam.UnEquipmentItemIds.AddRange(unequippedItemIds.ToRepeatedField());
               
                LogRequest(GetType() + ".UpdateAvatarAsync", requestParam.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.UpdateAvatarAsync(requestParam, Metadata);
                var response = await request;
                
                requestParam.UnEquipmentItemIds.AddRange(unequippedItemIds.ToRepeatedField());
                
                // //HACK BY UPDATE ONE BY ONE DUE TO BACKEND FAILED TO UPDATE LIST OF UNEQUIPPED
                // requestParam = new UpdateAvatarRequest();
                // requestParam.AvatarId = avatarId;
                // var unequipCount = 0;
                // foreach (var unequipped in unequippedItemIds.ToRepeatedField())
                // {
                //     unequipCount++;
                //     requestParam = new UpdateAvatarRequest();
                //     requestParam.AvatarId = avatarId;
                //     requestParam.UnEquipmentItemIds.Add(unequipped);
                //     LogRequest(GetType() + $".UpdateAvatarAsync Extra {unequipCount}", requestParam.ToString());
                //     // LogMetadataAndChannel(Metadata, Channel);
                //     await client.UpdateAvatarAsync(requestParam, Metadata);
                // }
                
                LogResponse(GetType() + ".UpdateAvatarAsync", response.ToString());
                
                return true;
            }
            catch (RpcException e)
            {
                Debug.LogException(e);
                return false;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        [SerializeField] bool autoStartFetchAvatar = false;
        [SerializeField] private MuziAvatarMaker netWorkAvatarMaker;
        

        private void Start()
        {
            if (autoStartFetchAvatar)
            {
                Debug.Log("<color=red>AvatarServices is auto running from Start()</color>");
                FetchUserAvatarsData();
            }
        }

        public async void FetchUserAvatarsData()
        {
            // await Task.Delay(1000);
            var done = await GetAvatarsAsync();
            if (done)
            {
                if (HasAvatarInfo && CurrentAvatar != null && CurrentAvatar.item != null)
                {
                    // var userInventoryService = GetComponent<UserInventoryService>();
                    // if (userInventoryService) // due to backend lack of client support, client need to call another api to filter equipped item only
                    // {
                    //     var successful = await userInventoryService.GetAllUserAvatarsItems();
                    //     if (successful)
                    //     {
                    //         foreach (var item in CurrentAvatar.item)
                    //         {
                    //             item.isEquipped =
                    //                 userInventoryService.DictIDtoUserItems.ContainsKey(item.inventoryItemId) &&
                    //                 userInventoryService.DictIDtoUserItems[item.inventoryItemId].isEquipped;
                    //         }
                    //     }
                    // }

                    Debug.Log(
                        $"<color=yellow>AvatarServices call AvatarMaker.ApplyNetworkAvatar for the avatar \n{CurrentAvatar} </color>");
                    netWorkAvatarMaker.MakeNetworkAvatar(CurrentAvatar);
                }
                else
                {
                    Debug.Log(
                        "<color=yellow>AvatarServices return no avatar => Auto switch to CharacterCustomization scene </color>");
                    if (SceneManager.GetActiveScene().name != "Character")
                    {
                        TW.AddLoading().LoadScene("Character");
                    }
                }
            }
        }

        public bool HasAvatarInfo => avatarInfos != null && avatarInfos.avatars != null && avatarInfos.avatars.Count > 0;

        public bool HasValidAvatar
        {
            get
            {
                if (CurrentAvatar != null && (CurrentAvatar.info.baseModel == SupportedAvatarType.Female02.ToString() ||
                    CurrentAvatar.info.baseModel == SupportedAvatarType.Male04.ToString()))
                {
                    return true;
                }
                return HasAvatarInfo &&
                       CurrentAvatar != null && CurrentAvatar.item != null && CurrentAvatar.item.Count > 0;
            }
        }

        public AvatarInfo CurrentAvatarInfo
        {
            get
            {
                if (avatarInfos == null || avatarInfos.avatars == null || avatarInfos.avatars.Count < 1)
                {
                    return null;
                }
                return avatarInfos.avatars.FirstOrDefault(e => e.isCurrent);
            }
        }

        public async Task<bool> GetUserNoneCompleteAction()
        {
            try
            {
                var client2 = new UserService.UserServiceClient(FoundationManager.channel);
                var myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.Unspecified;
                var reply2 = await client2.GetUserInfoAsync(new Google.Protobuf.WellKnownTypes.Empty(),
                    FoundationManager.metadata);
                // _userId_Text.text = reply2.UserId;
                Debug.Log(reply2.ToString());

                if (FoundationManager.userInfoLiteResponse == null)
                {
                    FoundationManager.userInfoLiteResponse = reply2;
                }

                // foreach (Muziverse.Proto.User.Domain.NonCompletedAction it in reply2.NonCompletedActions)
                // {
                //     if (it == Muziverse.Proto.User.Domain.NonCompletedAction.NotPassword)
                //     {
                //         myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.NotPassword;
                //         text_ChangePasswordButton.text = "Set password";
                //     }
                //     else if (it == Muziverse.Proto.User.Domain.NonCompletedAction.NotEmail)
                //     {
                //         myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.NotEmail;
                //         text_ChangePasswordButton.text = "Claim account";
                //         buttonLogout.gameObject.SetActive(false);
                //         break;
                //     }
                // }
                return true;
            }
            catch (RpcException ex)
            {
                Debug.LogError(ex);
                return false;
            }
        }
    }
}
