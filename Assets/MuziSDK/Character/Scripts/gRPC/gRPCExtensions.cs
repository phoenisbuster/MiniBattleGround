using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Muziverse.Proto.Activity.Api.Inventory;
using Muziverse.Proto.Avatar.Api.Info;
using Muziverse.Proto.Common.Domain;
using Muziverse.Proto.Item.Domain;
using Enum = System.Enum;

namespace MuziCharacter
{
    /// <summary>
    /// The extension holds all gRPC data to normal serializable data in Unity client.
    /// When gRPC class changes due to server logics change, this class is the only one to be fixed. 
    /// </summary>
    public static class gRPCExtensions
    {
        // Util conversion
        // Gender Conversion
        public static Gender ToGender(this SGender sGender) => Enum.TryParse<Gender>(sGender.ToString(), out var gender)
            ? gender
            : Gender.Unspecified;

        public static SGender ToSGender(this Gender sGender) =>
            Enum.TryParse<SGender>(sGender.ToString(), out var gender) ? gender : SGender.Unspecified;

        // Rarity conversion
        public static SRarity ToSRarity(this Rarity rarity) =>
            Enum.TryParse<SRarity>(rarity.ToString(), out var sRarity) ? sRarity : SRarity.Unspecified;

        public static Rarity ToRarity(this SRarity r) =>
            Enum.TryParse<Rarity>(r.ToString(), out var sr) ? sr : Rarity.Unspecified;

        // resource type conversion
        public static SItemResourceType ToSItemResourceType(this ItemResourceType rs) =>
            Enum.TryParse<SItemResourceType>(rs.ToString(), out var _rs) ? _rs : SItemResourceType.ResourceUnspecified;

        public static ItemResourceType ToItemResourceType(this SItemResourceType rs) =>
            Enum.TryParse<ItemResourceType>(rs.ToString(), out var _rs) ? _rs : ItemResourceType.ResourceUnspecified;

        // resource option type
        public static SOptionType ToSOptionType(this OptionType optionType) =>
            Enum.TryParse<SOptionType>(optionType.ToString(), out var rt) ? rt : SOptionType.Unspecified;

        public static OptionType ToOptionType(this SOptionType optionType) =>
            Enum.TryParse<OptionType>(optionType.ToString(), out var rt) ? rt : OptionType.Unspecified;


        // string to resourece type
        // resource type conversion
        public static SItemResourceType ToItemResourceType(this string rs)
        {
            switch (rs)
            {
                case "BASE_IMG":
                    return SItemResourceType.BaseImg;
                case "FBX":
                    return SItemResourceType.Fbx;
                case "NORMAL_IMG":
                    return SItemResourceType.NormalImg;
                case "RME_IMG":
                    return SItemResourceType.RmeImg;
                case "THUMBNAIL_ICON":
                    return SItemResourceType.ThumbnailIcon;
                default:
                    return SItemResourceType.ResourceUnspecified;
            }
        }

        // resource option
        public static SItemOption ToSItemOption(this ItemOption option)
        {
            return null;
        }


        public static ItemOption ToItempOption(this SItemOption sOption)
        {
            return null;
        }


        // List<string> to Repeated<>
        public static RepeatedField<T> ToRepeatedField<T>(this List<T> list)
        {
            var rt = new RepeatedField<T>();
            foreach (var t in list)
            {
                if (!rt.Contains(t))
                {
                    rt.Add(t);
                }
            }

            return rt;
        }

        public static List<T> ToList<T>(this RepeatedField<T> repeatedFields)
        {
            var rt = new List<T>();
            foreach (var t in repeatedFields)
            {
                rt.Add(t);
            }

            return rt;
        }

        //ResourceData conversion
        public static SResourceData ToSResourceData(this ResourceData rd)
        {
            var srd = new SResourceData
            {
                // ResourceGroups = rd.ResourceGroups,
                ResourceId = rd.ResourceId,
                ResourceType = rd.ResourceType.ToSItemResourceType(),
                ResourcePath = rd.ResourcePath
            };

            return srd;
        }

        // fetch directive
        public static ItemFetchDirective ToItemFetchDirective(this SItemFetchDirective s)
        {
            return new ItemFetchDirective()
            {
                GetOptions = s.GetOptions,
                GetPrice = s.GetPrice,
                GetProperties = s.GetProperties,
                GetResources = s.GetResources,
                GetCategoryDetail = s.GetCategoryDetail,
                RelativePath = s.RelativePath
            };
        }

        // struct to client struct
        public static SStruct ToSStruct(this Struct _struct)
        {
            var ret = new SStruct();
            ret.Fields = new List<KeyValue>();
            foreach (var pair in _struct.Fields)
            {
                ret.Fields.Add(new KeyValue()
                {
                    Key = pair.Key,
                    Value = pair.Value.StringValue
                });
            }
            // ret.Fields = _struct.Fields.ToDictionary(e => e.Key, e => e.Value.ToObjectValue());
            return ret;
        }

        //static object ToObjectValue(this Value _protoValue)
        //{
        //    switch (_protoValue.KindCase)
        //    {
        //        case Value.KindOneofCase.None:
        //        case Value.KindOneofCase.NullValue:
        //            return null;
        //        case Value.KindOneofCase.NumberValue:
        //            return _protoValue.NumberValue;
        //        case Value.KindOneofCase.StringValue:
        //            return _protoValue.StringValue;
        //        case Value.KindOneofCase.BoolValue:
        //            return _protoValue.BoolValue;
        //        case Value.KindOneofCase.StructValue:
        //            return _protoValue.StructValue;
        //        case Value.KindOneofCase.ListValue:
        //            return _protoValue.ListValue;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        static Value ToProtoValue(this string s)
        {
            if (double.TryParse(Convert.ToString(s), out double num))
            {
                return Value.ForNumber(num);
            }

            if (bool.TryParse(Convert.ToString(s), out bool _bool))
            {
                return Value.ForBool(_bool);
            }

            var stringValue = Convert.ToString(s);
            return !string.IsNullOrEmpty(stringValue) ? Value.ForString(stringValue) : Value.ForNull();
        }

        public static Struct ToStruct(this SStruct s)
        {
            Struct ret = null;
            if (s.Fields != null && s.Fields.Count > 0)
            {
                ret = new Struct();

                foreach (var pair in s.Fields)
                {
                    ret.Fields.Add(pair.Key, pair.Value.ToProtoValue());
                }
            }

            return ret;
        }


        // category to list
        public static List<string> ToList(this List<ParentCategoryCode> codes) =>
            codes.Select(code => code.ToString()).ToList();

        public static List<string> ToList(this List<CategoryCode> codes) =>
            codes.Select(code => code.ToString()).ToList();

        // ItemRequest conversion
        public static ItemRequest ToItemRequest(this SItemRequest s)
        {
            var itemRequest = new ItemRequest();
            itemRequest.ItemExternalId = s.ItemExternalId;
            if (s.Customization != null)
            {
                itemRequest.Customization = s.Customization.ToStruct();
            }

            return itemRequest;
        }

        public static InventoryItemType ToInventoryItemType(this SInventoryItemType s)
        {
            return Enum.TryParse<InventoryItemType>(s.ToString(), out var i) ? i : InventoryItemType.NonNft;
        }
        
        public static SInventoryItemType ToSInventoryItemType(this InventoryItemType i)
        {
            return Enum.TryParse<SInventoryItemType>(i.ToString(), out var s) ? s : SInventoryItemType.NonNft;
        }
        
        // public static SAvatarInfoResponse ToSAvatarInfo
    }

}