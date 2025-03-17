using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubble : MonoBehaviour
{
    public TextMeshProUGUI usernameTxt;
    public TextMeshProUGUI chatTxt;

    public void SetChat(string username, string chat)
    {
        this.usernameTxt.text = username;
        this.chatTxt.text = chat;
    }
}
