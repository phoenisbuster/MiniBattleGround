using System;
using System.Collections.Generic;
using DTT.Utils.Extensions;
using UnityEngine;

namespace MuziCharacter
{
    public class WheelPanel : TWBoard
    {
        [SerializeField] private GameObject wheelButtonPrefab;
        [SerializeField] private Transform holder;

        public Action<float> OnEmotesChoice;

        private EmotesController _controller;
        protected override void Show()
        {
            base.Show();
            this.GetRectTransform().sizeDelta = new Vector2(400, 400);
        }

        public void UIFocus(bool isFocus)
        {
            Cursor.lockState = isFocus ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isFocus;
            Opsive.Shared.Events.EventHandler.ExecuteEvent(_controller.gameObject, "OnEnableGameplayInput", !isFocus);
        }

        private void OnDisable()
        {
            UIFocus(false);
        }

        protected override void DeleteMe(float timeOut = 0.1f)
        {
            timeOut = 0.25f;
            base.DeleteMe(timeOut);
        }

        public void SetData(List<string> emotesData, EmotesController controller)
        {
            foreach (Transform child in holder)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < emotesData.Count; i++)
            {
                var wheelButton = Instantiate(wheelButtonPrefab, holder).GetComponent<WheelButton>();
                wheelButton.gameObject.name = $"WheelButton_{i + 1}";
                wheelButton.SetData(emotesData.Count, i + 1, emotesData[i]);
                wheelButton.OnClick = choice =>
                {
                    OnEmotesChoice?.Invoke((float) choice);
                    UIFocus(false);
                    ClickX();
                };
            }
            _controller = controller;
            UIFocus(true);
        }
    }
}