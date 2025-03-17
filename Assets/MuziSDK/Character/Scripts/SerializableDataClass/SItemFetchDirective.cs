using System;

namespace MuziCharacter
{
    [Serializable]
    public class SItemFetchDirective
    {
        public bool GetOptions = true;
        public bool GetProperties = true;
        public bool GetPrice = true;
        public bool GetResources = true;
        public bool GetCategoryDetail = true;
        public string RelativePath = string.Empty;
    }
}