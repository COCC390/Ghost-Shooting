using UnityEngine;

namespace Konzit.GameSystem
{
    public interface IGUIElementManager
    {
        void Initialize();
        void Show<T>() where T : IGUIElement;
    }

    public interface IHideGUIElementManager
    {
        void Hide();
    }

    public interface IHideGUIElementManagerWithType
    {
        void Hide<T>() where T : IGUIElement;
    }
}
