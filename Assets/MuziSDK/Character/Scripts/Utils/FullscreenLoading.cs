using System;
using UnityEngine;

namespace MuziCharacter
{
    public static class FullscreenLoading
    {
        private static TWFullscreenLoading _loading;

        private static int _loadingCount = 0;

        public static void ShowLoading(string message = "", bool hasCoverImage = true, bool isFocus = false)
        {
            if (_loading == null || _loading.gameObject == null)
            {
                _loading = TW.AddTWByName_s("TWFastLoadingV2").GetComponent<TWFullscreenLoading>();
            }
            
            if (!hasCoverImage)
            {
                _loading.SetTransparent();
            }
            else
            {
                _loading.SetOpaque();
            }

            if (!string.IsNullOrEmpty(message))
            {
                _loading.AddText(message);
            }
            
            _loading.isForcus = isFocus;
            _loadingCount++;
        }

        public static void AddText(string message)
        {
            if (_loading != null && _loading.gameObject != null)
            {
                _loading.AddText(message);
            }
        }

        public static void HideLoading(bool instant = false, Action onHideDone = null)
        {
            if (_loading != null && _loading.gameObject != null)
            {
                _loading.Hide(instant);
                _loadingCount = 0;
            }
        }

        public static void HideIfLoadingIsLast(bool instant = false, Action onHideDone = null)
        {
            _loadingCount--;
            if (_loadingCount == 0 && _loading != null && _loading.gameObject != null)
            {
                _loading.Hide(instant);
                _loadingCount = 0;
            }
        }

        public static bool Hidden => _loading == null || _loading.gameObject == null || !_loading.gameObject.activeInHierarchy;
    }
}