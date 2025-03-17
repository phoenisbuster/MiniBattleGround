using UnityEngine;
using TMPro;

namespace GameAudition
{
    public class SettingsItem : MonoBehaviour
    {
        public TMP_Text _field_Text;

        public void Init(string value)
        {
            _field_Text.text = value;
        }
    }
}