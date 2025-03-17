using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatNotice : MonoBehaviour
{
    public TextMeshProUGUI noticeTxt;

    public void SetNotice(string notice)
    {
        noticeTxt.text = notice;
    }
}
