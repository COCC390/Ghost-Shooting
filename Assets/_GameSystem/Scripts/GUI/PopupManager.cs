using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class PopupManager : BaseGUIElementManager
    {
        private Dictionary<Type, BasePopup> _popupCaches;
        private readonly HashSet<BaseGUIElement> _visiblePopups = new();
        public bool HasVisiblePopups => _visiblePopups.Count > 0;
        public event Action VisibilityChanged;
        protected override IEnumerator ShowInternal(BaseGUIElement popup)
        {
            if (_visiblePopups.Add(popup)) VisibilityChanged?.Invoke();
            yield return base.ShowInternal(popup);
        }
        protected override IEnumerator HideInternal(BaseGUIElement popup)
        {
            yield return base.HideInternal(popup);
            if (_visiblePopups.Remove(popup)) VisibilityChanged?.Invoke();
        }
        private void OnDisable() { _visiblePopups.Clear(); VisibilityChanged?.Invoke(); }
        private void OnEnable()
        {
            if (_popupCaches == null) return;
            foreach (var popup in _popupCaches.Values)
                if (popup != null && popup.gameObject.activeInHierarchy) _visiblePopups.Add(popup);
            VisibilityChanged?.Invoke();
        }

        public void Initialize()
        {
            _popupCaches = new Dictionary<Type, BasePopup>();
        }

        public BasePopup Get<T>()
        {
            var popupType = typeof(T);

            if (_popupCaches.ContainsKey(popupType))
                return _popupCaches[popupType];
            return null;
        }

        public void Show<T>() where T : IGUIElement
        {
            var popupType = typeof(T);

            if (_popupCaches.ContainsKey(popupType))
            {
                var popup = _popupCaches[popupType];
                popup.HideAction += Hide<T>;
                StartCoroutine(ShowInternal(popup));
            }
            else
            {
                var popup = (BasePopup)viewStored.GetGUIElement<T>();
                if (popup == null)
                {
                    Debug.Log($"Can't find view with type {popupType}");
                    return;
                }

                popup.gameObject.SetActive(false);
                var popupInstant = GameObject.Instantiate(popup, this.transform);
                popupInstant.Initialize();
                popup.gameObject.SetActive(true);
                StartCoroutine(ShowInternal(popupInstant));
                popupInstant.HideAction += Hide<T>;

                _popupCaches.Add(popupType, popupInstant);
            }
        }

        public void Hide<T>() where T : IGUIElement
        {
            var popupType = typeof(T);
            if (_popupCaches.ContainsKey(popupType))
            {
                var popup = _popupCaches[popupType];
                popup.HideAction -= Hide<T>;
                StartCoroutine(HideInternal(popup));
            }
        }

    }
}
