using System;
using System.Collections.Generic;
using MuziCharacter.DataModel;

namespace MuziCharacter
{
    [Serializable]
    public class SItemOption
    {
        public SOptionType OptionType;
        public List<SItemSingleOption> Options;
    }
    
    [Serializable]
    public class SItemSingleOption
    {
        public string DefaultValue;
        public string LinkedItemId;
        public int OptionOrdering; 
    }
    
    public enum SOptionType
    {
        Unspecified,
        ChangeColor,
        ChangeTexture,
    }
}