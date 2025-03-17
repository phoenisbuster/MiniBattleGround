using System.Collections;
using UnityEngine;
using TMPro;

namespace GameAudition
{
    public class DH_DialogBubble : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dialogText;
        [SerializeField] private float _showDuration = 8f;
        [SerializeField] private GameObject _displayNameObject;

        private int _lineLength = 35;
        private float _fadeTime = 0.1f;
        private bool _isShowingText;
        private float _showTimer = 0;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        private void Update()
        {
            if (_isShowingText)
            {
                _showTimer += Time.deltaTime;
                if (_showTimer >= _showDuration)
                {
                    StartCoroutine(Hide());
                    _isShowingText = false;
                    _showTimer = 0;
                    if (_displayNameObject != null) _displayNameObject.SetActive(true);
                }
            }
        }

        public void Show(string message)
        {
            if (_displayNameObject != null) _displayNameObject.SetActive(false);
            StartCoroutine(DisplayTextInstantly(message));
        }

        public IEnumerator Hide()
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < _fadeTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = 1.0f - Mathf.Clamp01(elapsedTime / _fadeTime);
            }
        }

        IEnumerator DisplayTextInstantly(string message)
        {
            if (_isShowingText)
                yield return StartCoroutine(Hide());
            else yield return null;

            _isShowingText = true;
            _showTimer = 0;
            _dialogText.text = string.Empty;

            if (message.Length > _lineLength)
            {
                var lastSpaceIndex = message.Substring(0, _lineLength).LastIndexOf(' ');
                if (Mathf.Abs(_lineLength - lastSpaceIndex) >= 10)
                    lastSpaceIndex = _lineLength;
                message = message.Substring(0, lastSpaceIndex) + "\n" + message.Substring(lastSpaceIndex);
            }

            _dialogText.text = message;

            float elapsedTime = 0.0f;
            while (elapsedTime < _fadeTime)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime / _fadeTime);
            }
        }
    }
}