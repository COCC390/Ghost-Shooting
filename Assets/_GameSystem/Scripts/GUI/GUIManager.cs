using UnityEngine;

namespace Konzit.GameSystem
{
    public class GUIManager : MonoBehaviour, IGUIManager
    {
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private PopupManager _popupManager;

        public void InitializeGUIManager()
        {
            //_popupManager = GetComponentInChildren<PopupManager>();

            if (_viewManager == null || _popupManager == null)
            {
                Debug.Log("Cant find view manager or popup manager");
                return;
            }

            _viewManager.Initialize();
            _popupManager.Initialize();
        }

        public BaseGUIElement GetView()
        {
            return _viewManager.Get();
        }

        public BaseGUIElement GetPopup<T>() where T : IGUIElement
        {
            return _popupManager.Get<T>();
        }

        public void Show<T>(GUIElementType elementType) where T : IGUIElement
        {
            switch (elementType)
            {
                case GUIElementType.View:
                    _viewManager.Show<T>();
                    break;
                case GUIElementType.Popup:
                    _popupManager.Show<T>();
                    break;
                default:
                    break;
            }
        }

        public void Hide<T>(GUIElementType elementType) where T : IGUIElement
        {
            switch (elementType)
            {
                case GUIElementType.View:
                    _viewManager.Hide();
                    break;
                case GUIElementType.Popup:
                    _popupManager.Hide<T>();
                    break;
                default:
                    break;
            }
        }
    }
}
