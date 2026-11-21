using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IGUIElement
    {
        void Initialize();
        void OnShow();
        void OnShown();
        void OnHide();
        void OnHidden();
    }

    public interface IGUIElementInternalHide
    {
        void Hide();
    }
}
