using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MuziCharacter.DataModel
{
    [Serializable]
    public class Item
    {
        public string externalId;
        public string rarity;
        public string description;
        public string title;
        public string categoryCode;
        public string parentCategoryCode;
        public List<Resource> resources;
        // public List<object> options;
        public List<Property> properties;
        // public List<object> prices;
        public CategoryDetail categoryDetail;
        
        public string UniqueItemKey => categoryCode + "@" + externalId;

        public override string ToString()
        {
            return $"Item {categoryCode}@{externalId}";
        }

        public bool IsFullSet => categoryCode == CategoryCode.FASHION_FULLSET.ToString();

        public bool IsFashionBasic
        {
            get
            {
                return categoryCode == CategoryCode.FASHION_SHIRT.ToString() ||
                       categoryCode == CategoryCode.FASHION_SHOE.ToString() ||
                       categoryCode == CategoryCode.FASHION_PANTS.ToString();
            }
        }

        public Texture2D ThumbnailTex()
        {
            if (resources == null) return null;
            var rs = resources.FirstOrDefault(e => e.resourceType == "THUMBNAIL_ICON");
            if (rs != null)
            {
                return rs.Texture(RootFolder()) as Texture2D;
            }

            return null;
        }
        public Texture2D MainTex()
        {
            if (resources == null) return null;
            var rs = resources.FirstOrDefault(e => e.resourceType == "BASE_IMG");
            if (rs != null)
            {
                return rs.Texture(RootFolder()) as Texture2D;
            }

            return null;
        }
        
        public Texture2D NormalTex()
        {
            if (resources == null) return null;
            var rs = resources.FirstOrDefault(e => e.resourceType == "NORMAL_IMG");
            if (rs != null)
            {
                return rs.Texture(RootFolder()) as Texture2D;
            }

            return null;
        }
        
        public Texture2D RMETex()
        {
            if (resources == null) return null;
            return ToResource(resources.FirstOrDefault(e => e.resourceType == "RME_IMG"));
        }

        private Texture2D ToResource(Resource rs)
        {
            if (rs != null)
            {
                return rs.Texture(RootFolder()) as Texture2D;
            }

            return null;
        }
        
        public string RootFolder()
        {
            var genderProperty = properties?.FirstOrDefault(p => p.propertyPath == "validation.avatar.gender");
            string gender = "";
            if (genderProperty == null)
            {
                gender = externalId.Split("_")[0].ToUpper();
            }
            else
            {
                gender = genderProperty.propertyValue;
            }

            var folderPath = Path.Combine(Application.persistentDataPath, gender, categoryCode, externalId);
            return folderPath;
        }
    }
}