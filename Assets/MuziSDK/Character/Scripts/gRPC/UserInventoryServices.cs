using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using MuziCharacter.DataModel;
using Muziverse.Proto.Activity.Api.Inventory;
using Newtonsoft.Json;
using UnityEngine;
using Pageable = Muziverse.Proto.Common.Paging.Pageable;

namespace MuziCharacter
{
    public class UserInventoryServices : BaseService
    {
        
        private static UserInventoryServices _instance;

        public static UserInventoryServices Inst
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (new GameObject("grpcUserInventoryServices")).AddComponent<UserInventoryServices>();
                }

                return _instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }
        
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> EstablishInventoryAsync(List<SItemRequest> newItems,
            List<SInventoryItemRequest> modifiedItems, List<long> deletedItems)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            try
            {
                var client = new Muziverse.Proto.Activity.Api.Inventory.UserInventoryService.UserInventoryServiceClient(Channel);
                var requestParam = new EstablishInventoryRequest();
                foreach (var itemId in newItems)
                {
                    requestParam.NewItem.Add(itemId.ItemExternalId, new EstablishInventoryRequest.Types.InventoryItemRequest()
                    {
                        InventoryItemType = InventoryItemType.NonNft,
                        IsEquipped = false
                    });
                }

                if (deletedItems != null && deletedItems.Count > 0)
                {
                    requestParam.DeletedItem.AddRange(deletedItems);
                }
                
                if (modifiedItems != null && modifiedItems.Count > 0)
                {
                    foreach (var item in modifiedItems)
                    {
                        var inventoryItemRequest = new EstablishInventoryRequest.Types.InventoryItemRequest();
                        if (item.Customization != null)
                        {
                            inventoryItemRequest.Customization = item.Customization.ToStruct();
                        }

                        inventoryItemRequest.InventoryItemType = item.InventoryItemType.ToInventoryItemType();
                        inventoryItemRequest.IsEquipped = item.IsEquipped;
                        requestParam.ModifiedItem.Add(item.ItemId, inventoryItemRequest);
                    }
                }
                
                LogRequest(GetType() + ".EstablishInventoryAsync", requestParam.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.EstablishInventoryAsync(requestParam, Metadata);
                var response = await request;
                LogResponse(GetType() + ".EstablishInventoryAsync", response.ToString());
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

        public Dictionary<CategoryCode, List<UserItem>> CategorizedFemaleItems;
        public Dictionary<CategoryCode, List<UserItem>> CategorizedMaleItems;
        public Dictionary<long, UserItem> IDUserItems = new Dictionary<long, UserItem>();
        public Dictionary<string, UserItem> ExternalIdUserItems = new Dictionary<string, UserItem>();


        void AddOrUpdate(ref Dictionary<CategoryCode, List<UserItem>> currentDict,
            List<UserItem> userItems)
        {
            if (currentDict == null)
            {
                currentDict = new Dictionary<CategoryCode, List<UserItem>>();
            }

            foreach (var userItem in userItems)
            {
                userItem.isOwned = true;
                if (!currentDict.ContainsKey(userItem.itemCategoryCode.ToCategoryCode()))
                {
                    currentDict.Add(userItem.itemCategoryCode.ToCategoryCode(), new List<UserItem>(){userItem});
                }
                else
                {
                    if (currentDict[userItem.itemCategoryCode.ToCategoryCode()].Contains(userItem))
                    {
                        currentDict[userItem.itemCategoryCode.ToCategoryCode()].Add(userItem);
                    }
                }

                if (!ExternalIdUserItems.ContainsKey(userItem.itemExternalId))
                {
                    ExternalIdUserItems.Add(userItem.itemExternalId, userItem);
                }
                else
                {
                    ExternalIdUserItems[userItem.itemExternalId] = userItem;
                }
                
                if (!IDUserItems.ContainsKey(userItem.inventoryItemId))
                {
                    IDUserItems.Add(userItem.inventoryItemId, userItem);
                }
                else
                {
                    IDUserItems[userItem.inventoryItemId] =  userItem;
                }
            }
        }

        public async Task<bool> GetAllUserAvatarsItems()
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            try
            {
                if (CategorizedMaleItems != null) CategorizedMaleItems.Clear();
                if (CategorizedFemaleItems != null) CategorizedFemaleItems.Clear();
                if (IDUserItems != null) IDUserItems.Clear();
                if (ExternalIdUserItems != null) ExternalIdUserItems.Clear();
                foreach (ParentCategoryCode parentCode in (ParentCategoryCode[]) Enum.GetValues(typeof(ParentCategoryCode)))
                {
                    if (parentCode == ParentCategoryCode.FURNITURE) continue;
                    await GetUserAvatarItemsByCategory(parentCode, "FEMALE");
                    await Task.Yield();
                    await GetUserAvatarItemsByCategory(parentCode, "MALE");
                    await Task.Yield();
                }
               
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
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> GetUserAvatarItemsByCategory(ParentCategoryCode parentCategoryCode, string gender)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            const int defaultPageSize = 40;
            try
            {
                var client = new Muziverse.Proto.Activity.Api.Inventory.UserInventoryService.UserInventoryServiceClient(Channel);
                
                var requestParam = new UserInventoryFilterRequest();
                requestParam.ParentCategoryCode = parentCategoryCode.ToString();
                requestParam.ItemDataFetchDirective = (new SItemFetchDirective()).ToItemFetchDirective();
                requestParam.Properties.Add("validation.avatar.gender", gender.ToUpper()) ;
                requestParam.Pageable = new Pageable()
                {
                    PageNumber = 0,
                    PageSize = defaultPageSize
                };
                requestParam.Sort = UserInventoryFilterRequest.Types.UserInventorySort.Newest;
                
                LogRequest(GetType()+"FilterAsync", requestParam.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var request = client.FilterAsync(requestParam, Metadata);
                
                var response = await request;
                
                LogResponse(GetType()+"FilterAsync", response.ToString());
                
                var categorizedItems =
                    await Task.Run(() => JsonConvert.DeserializeObject<ItemFilterResult>(response.ToString()));
                var left = categorizedItems.totalItems - defaultPageSize;
                var pageNumber = 0;
                while (left > 0)
                {
                    requestParam.Pageable = new Pageable()
                    {
                        PageNumber = ++pageNumber,
                        PageSize = defaultPageSize
                    };
                    LogRequest(GetType()+$"FilterAsync #{pageNumber}", requestParam.ToString());
                    LogMetadataAndChannel(Metadata, Channel);
                    requestParam.Sort = UserInventoryFilterRequest.Types.UserInventorySort.Newest;
                    request = client.FilterAsync(requestParam, Metadata);
                    response = await request;
                    LogResponse(GetType()+"FilterAsync #{pageNumber}", response.ToString());
                    
                    var moreItems = await Task.Run(() => JsonConvert.DeserializeObject<ItemFilterResult>(response.ToString()));
                    categorizedItems.data.AddRange(moreItems.data);
                    left -= defaultPageSize;
                }

              
                if (gender.ToUpper() == "FEMALE")
                {
                    if (categorizedItems != null && categorizedItems.data != null)
                    {
                        AddOrUpdate(ref CategorizedFemaleItems, categorizedItems.data);
                    }
                } 
                else if (gender.ToUpper() == "MALE")
                {
                    if (categorizedItems != null && categorizedItems.data != null)
                    {
                        AddOrUpdate(ref CategorizedMaleItems, categorizedItems.data);
                    }
                }
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
        
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> GetAllItemByCategoryAsync(string categoryCode, string parentCategoryCode, SItemFetchDirective directive)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            try
            {
                var client = new Muziverse.Proto.Activity.Api.Inventory.UserInventoryService.UserInventoryServiceClient(Channel);
                var requestParam = new UserInventoryFilterRequest();

                if (categoryCode == null)
                {
                    categoryCode = string.Empty;
                }
                
                if (parentCategoryCode == null)
                {
                    parentCategoryCode = string.Empty;
                }

                requestParam.CategoryCode = categoryCode;
                requestParam.ParentCategoryCode = parentCategoryCode;
                requestParam.ItemDataFetchDirective = directive.ToItemFetchDirective();
                requestParam.Pageable = new Pageable()
                {
                    PageNumber = 0,
                    PageSize = 12
                };

                requestParam.Sort = UserInventoryFilterRequest.Types.UserInventorySort.Newest;
                
                var request = client.FilterAsync(requestParam, Metadata);
                var response = await request;
                
                Debug.Log(response);
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

        // [SerializeField] private ItemFilterResult queryItemResult;
        // [SerializeField] private Dictionary<SupportedAvatarType, List<UserItem>> itemsBySupportedType;
        //
        //
        // public async Task GetAllItemByIdsAsync(List<long> itemIds, SItemFetchDirective directive)
        // {
        //     if (!await CheckChannelStatus())
        //     {
        //         Debug.LogError("GRPC Channel is not ready");
        //         return;
        //     }
        //     try
        //     {
        //         var client = new Muziverse.Proto.Activity.Api.Inventory.UserInventoryService.UserInventoryServiceClient(Channel);
        //         var requestParam = new GetAllItemByIdsRequest();
        //         requestParam.InventoryItemId.AddRange(itemIds);
        //         requestParam.ItemDataFetchDirective = directive.ToItemFetchDirective();
        //         var request = client.GetAllItemByIdsAsync(requestParam, Metadata);
        //         var response = await request;
        //
        //         queryItemResult = await Task.Run(() => JsonConvert.DeserializeObject<ItemFilterResult>(response.ToString()));
        //         await Task.Run(() =>
        //         {
        //         });
        //         Debug.Log(response);
        //     }
        //     catch (RpcException e)
        //     {
        //         Debug.LogException(e);
        //     }
        //     catch (Exception e)
        //     {
        //         Debug.LogException(e);
        //     }
        // }
    }
}