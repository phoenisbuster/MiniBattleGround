using System;

namespace MuziCharacter.DataModel
{
    [Serializable]
    public class CategoryDetail
    {
        public string categoryCode;
        public string parentCategoryCode;
        public string categoryName;
        public int lv;
        public string categoryRootPoint;
        public string categoryPoint;
    }
}