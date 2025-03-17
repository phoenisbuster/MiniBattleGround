using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

namespace GameAudition
{
    public class ChatItem : MonoBehaviour
    {
        public TMP_Text textMessage;

        public void Init(string textMessage, string textUsername, string colorCode)
        {
            this.textMessage.text = $"<color={colorCode}>{textUsername}</color>: {textMessage}";
        }
    }
}
