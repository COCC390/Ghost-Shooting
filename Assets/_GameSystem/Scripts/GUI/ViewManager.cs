using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class ViewManager : BaseGUIElementManager
    {
        private Dictionary<Type, BaseView> _viewCaches;
        private BaseView _currentView;

        public void Initialize()
        {
            _viewCaches = new Dictionary<Type, BaseView>();
        }

        public BaseView Get()
        {
            if (_currentView == null)
                return null;
            return _currentView;
        }

        public void Show<T>() where T : IGUIElement
        {
            var viewType = typeof(T);

            if (_viewCaches.ContainsKey(viewType))
            {
                var view = _viewCaches[viewType];
                StartCoroutine(ShowInternal(view));
            }
            else
            {
                var view = (BaseView)viewStored.GetGUIElement<T>();
                if (view == null)
                {
                    Debug.Log($"Can't find view with type {viewType}");
                    return;
                }

                view.gameObject.SetActive(false);
                var viewInstant = GameObject.Instantiate(view, this.transform);
                viewInstant.Initialize();
                view.gameObject.SetActive(true);
                StartCoroutine(ShowInternal(viewInstant));
                _viewCaches.Add(viewType, viewInstant);
            }
        }

        protected override IEnumerator ShowInternal(BaseGUIElement guiElement)
        {
            if (_currentView != null)
            {
                _currentView.OnHide();
                if (_currentView.HideAnimation != null)
                {
                    _currentView.HideAnimation.PlayAnimation();
                    yield return new WaitUntil(() => _currentView.HideAnimation.AnimationCompleted);
                }
                _currentView.gameObject.SetActive(false);
                _currentView.OnHidden();
            }

            _currentView = (BaseView)guiElement;
            yield return base.ShowInternal(guiElement);
        }

        public void Hide()
        {
            if (_currentView == null) return;
            StartCoroutine(HideInternal(_currentView));
        }
    }
}
