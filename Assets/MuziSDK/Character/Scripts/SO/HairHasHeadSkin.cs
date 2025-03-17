using System.Collections.Generic;
using UnityEngine;

namespace MuziCharacter
{
    [CreateAssetMenu(fileName = "HairWithHeadSkin", menuName = "Character/HairData", order = 0)]
    public class HairHasHeadSkin : ScriptableObject
    {
        public List<string> hairExternalIds;
    }
}