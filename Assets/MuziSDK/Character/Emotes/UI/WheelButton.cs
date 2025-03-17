using System;
using DG.Tweening;
using DTT.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MuziCharacter
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class WheelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float _padding = 0.005f;
        private Button _mainButton;

        private int _order;
        private Image _uiPolygon;

        public Action<int> OnClick;

        [SerializeField] private TextMeshProUGUI emoteText;
        

        // Start is called before the first frame update
        void Awake()
        {
            _uiPolygon = GetComponent<Image>();
            _mainButton = GetComponent<Button>();
            _mainButton.onClick.AddListener(Click);
            this.GetRectTransform().SetAnchor(RectAnchor.STRETCH_FULL, true, true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(1.1f, 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, 0.2f);
        }

        private void Click()
        {
            OnClick?.Invoke(_order);
        }

        public void SetData(int totalButtons, int order, string emotesData)
        {
            _order = order;
            var angle = 1f / (float) totalButtons;
            _uiPolygon.fillAmount = angle - _padding;
            this.GetRectTransform().Rotate(Vector3.forward * 360f / totalButtons * _order, Space.Self);
            emoteText.text = emotesData;
            gameObject.SetActive(false);
            ShowEffect(_order * 0.03f);
        }

        void ShowEffect(float delay)
        {
            gameObject.SetActive(true);
            transform.DOShakePosition(0.1f).SetDelay(delay);
            transform.DOScale(1.1f, 0.1f).SetDelay(delay);
            transform.DOScale(1f, 0.1f).SetDelay(delay + 0.1f);
        }
    }
}