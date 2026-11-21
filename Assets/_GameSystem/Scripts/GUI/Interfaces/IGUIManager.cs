using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IGUIManager
    {
        BaseGUIElement GetView();
        BaseGUIElement GetPopup<T>() where T : IGUIElement;
        void Show<T>(GUIElementType elementType) where T : IGUIElement;
        void Hide<T>(GUIElementType elementType) where T : IGUIElement;
    }

    public interface IGUIInternalCall
    {
        
    }
}
