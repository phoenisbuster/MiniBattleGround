using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Networking;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldSelectionText : MonoBehaviour, IPointerClickHandler
{
    private TMP_InputField inputField;
    private float clickTimer = 0.54f;
    private int clickCount;
    private bool isFirstClick = true;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        if (isFirstClick)
        {
            isFirstClick = false;
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(clickTimer);
        EvaluateClickCount(clickCount);
        clickCount = 0;
        isFirstClick = true;
    }

    void EvaluateClickCount(int clickCount)
    {
        if (clickCount >= 3)
        {
            inputField.DeactivateInputField(true);
            inputField.ActivateInputField();
        }
    }

    public void OnInputSelected()
    {
        DisableCharacterMovement(true);
        GeneralChatManager.instance.ToggleTwFunction(true);
    }

    public void OnInputDeselected()
    {
        DisableCharacterMovement(false);
        GeneralChatManager.instance.ToggleTwFunction(false);
    }

    void DisableCharacterMovement(bool isChatFocused)
    {
        Opsive.Shared.Events.EventHandler.ExecuteEvent(NakamaContentManager.instance.controllablePlayer, "OnEnableGameplayInput", !isChatFocused);
        Cursor.lockState = isChatFocused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isChatFocused;
    }
}
