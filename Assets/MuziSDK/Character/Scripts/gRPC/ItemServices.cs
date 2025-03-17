using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MuziCharacter.DataModel;
using Muziverse.Proto.Item.Api.Item;
using Newtonsoft.Json;
using UnityEngine;
using Pageable = Muziverse.Proto.Common.Paging.Pageable;

namespace MuziCharacter
{
    public class ItemServices : BaseService
    {
        private Dictionary<CategoryCode, List<Item>> categorizedMaleItems = new Dictionary<CategoryCode, List<Item>>();
        public Dictionary<CategoryCode, List<Item>> CategorizedMaleItems => categorizedMaleItems;
        
        private Dictionary<CategoryCode, List<Item>> categorizedFemaleItems = new Dictionary<CategoryCode, List<Item>>();
        public Dictionary<CategoryCode, List<Item>> CategorizedFemaleItems => categorizedFemaleItems;

        private static ItemServices _instance;

        public static ItemServices Inst
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (new GameObject("gRPCItemService")).AddComponent<ItemServices>();
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
        public async Task<bool> QueryAllItemsByGender(SGender gender)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            var queriedBody = await QueryItemsByParentCategory(ParentCategoryCode.BODY, gender);
            var queriedFashion = await QueryItemsByParentCategory(ParentCategoryCode.FASHION, gender);
            return queriedBody && queriedFashion;
        }

#if ANHNGUYEN_LOCAL && UNITY_EDITOR
        [Sirenix.OdinInspector.Button] 
#endif
        public async Task<bool> QueryItemsByParentCategory(ParentCategoryCode parentCategoryCode, SGender gender, int pageNumber = 0, int pageSize = 10)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            var queriedItemsResult = await QueryItemsAsyncV2(
                new List<CategoryCode>(), 
                new List<ParentCategoryCode> { parentCategoryCode }, 
                new List<SRarity>(),
                new SItemFetchDirective(),
                gender,
                pageNumber,
                pageSize);

            if (queriedItemsResult == null)
            {
                return false;
            }
            
            var left = queriedItemsResult.totalItems - pageSize;
            
            while (left > 0)
            {
                pageNumber++;
                var moreItems = await QueryItemsAsyncV2(
                    new List<CategoryCode>(), 
                    new List<ParentCategoryCode> { parentCategoryCode }, 
                    new List<SRarity>(),
                    new SItemFetchDirective(),
                    gender,
                    pageNumber,
                    pageSize);
               
                left -= pageSize;
                queriedItemsResult.items.AddRange(moreItems.items);
            }

            switch (gender)
            {
                case SGender.Male:
                    foreach (var item in queriedItemsResult.items)
                    {
                        AddOrUpdate(ref categorizedMaleItems, item.categoryCode.ToCategoryCode(), item);
                    }
                    break;
                case SGender.Female:
                    foreach (var item in queriedItemsResult.items)
                    {
                        AddOrUpdate(ref categorizedFemaleItems, item.categoryCode.ToCategoryCode(), item);
                    }
                    break;
                case SGender.Unspecified:
                case SGender.Unisex:
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }

            return true;
        }
        
        void AddOrUpdate(ref Dictionary<CategoryCode, List<Item>> currentDict, CategoryCode category, Item item)
        {
            if (currentDict == null)
            {
                currentDict = new Dictionary<CategoryCode, List<Item>>();
            }

            if (!currentDict.ContainsKey(category))
            {
                currentDict.Add(category, new List<Item>(){item});
            }
            else
            {
                var exist = currentDict[category].Any(i => i.externalId == item.externalId);
                if (!exist)
                {
                    currentDict[category].Add(item);
                }
            }
        }

        async Task<ItemQueryResult> QueryItemsAsyncV2(
            List<CategoryCode> subCodes,
            List<ParentCategoryCode> parentCodes,
            List<SRarity> rarities,
            SItemFetchDirective fetchDirective,
            SGender gender,
            int pageNumber = 0,
            int pageSize = 10)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return null;
            }
            try
            {
                parentCodes ??= new List<ParentCategoryCode>();

                rarities ??= new List<SRarity>();

                fetchDirective ??= new SItemFetchDirective();
                
                return await QueryItemsAsyncV2(subCodes.ToList(), parentCodes.ToList(), rarities, fetchDirective, gender,
                    pageNumber, pageSize);
            }
            catch (RpcException e)
            {
                // Debug.LogError($"[gRPC] QueryItemsAsync() got error {e.Message}");
                Debug.LogException(e);
                return null;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }
        
        async Task<ItemQueryResult> QueryItemsAsyncV2(
            List<string> subCategories, 
            List<string> parentCategories, 
            List<SRarity> rarities,
            SItemFetchDirective fetchDirective,
            SGender gender,
            int pageNumber = 0,
            int pageSize = 10)
        {
            if (!await CheckChannelStatus())
            {
                Debug.LogError("GRPC Channel is not ready");
                return null;
            }
            var client = new ItemService.ItemServiceClient(Channel);
            try
            {
                var request = new QueryItemRequest()
                {
                    Query = new QueryItemRequest.Types.QueryItemQuery(),
                    Pageable = new Pageable(),
                    FetchDirective = fetchDirective.ToItemFetchDirective()
                };

                request.Query.Rarities = new QueryItemRequest.Types.RarityQuery();
                request.Query.Rarities.Rarities.AddRange(rarities.ToRepeatedField().Select(e => e.ToRarity()));
                request.Pageable.PageSize = pageSize;
                request.Pageable.PageNumber = pageNumber;
                request.Query.Properties = new QueryItemRequest.Types.PropertiesQuery();
                request.Query.Properties.QueryConditions.Add("validation.avatar.gender", gender.ToString().ToUpper());

                if (subCategories.Count > 0)
                {
                    request.Query.Category = new QueryItemRequest.Types.CategoryCodeQuery();
                    request.Query.Category.Code.AddRange(subCategories.ToRepeatedField());
                }
                else if (parentCategories.Count > 0)
                {
                    request.Query.ParentCategory = new QueryItemRequest.Types.CategoryCodeQuery();
                    request.Query.ParentCategory.Code.AddRange(parentCategories.ToRepeatedField());
                }

                LogRequest(GetType()+"QueryItemsAsync", request.ToString());
                LogMetadataAndChannel(Metadata, Channel);
                var rq = client.QueryItemsAsync(request, Metadata);
                var rp = await rq;
                // Debug.Log(rp);
                LogResponse(GetType()+"QueryItemsAsync", rp.ToString());
#if UNITY_EDITOR
                // Debug.Log("rp = " + rp);
                // File.WriteAllText(Application.streamingAssetsPath + $"/QueriedItemResponse.json" ,rp.ToString());
#endif

                var itemFilteredResult = await Task.Run(() => JsonConvert.DeserializeObject<ItemQueryResult>(rp.ToString()));
                return itemFilteredResult;
            }
            catch (RpcException e)
            {
                // Debug.LogError($"[gRPC] QueryItemsAsync() got error {e.Message}");
                Debug.LogException(e);
                return null;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }

        public void DiscardUserItem(Dictionary<string, UserItem> discardList)
        {
            foreach (var cat in categorizedFemaleItems)
            {
                cat.Value.RemoveAll(e => discardList.ContainsKey(e.externalId));
            }

            foreach (var cat in categorizedMaleItems)
            {
                cat.Value.RemoveAll(e => discardList.ContainsKey(e.externalId));
            }
        }
    }
}