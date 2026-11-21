using System.Collections.Generic;
using UnityEngine;

namespace Konzit.GameSystem
{
    [CreateAssetMenu(fileName = "GUIElementStored", menuName = "Game/GameSystems/GUIElementStored")]
    public class GUIElementStored : ScriptableObject
    {
        [SerializeField] private List<BaseGUIElement> _gUIElements;

        public BaseGUIElement GetGUIElement<T>()
        {
            var type = typeof(T);

            foreach (var element in _gUIElements)
            {
                if (element.GetType() == type)
                    return element;
            }
            return null;
        }
    }
}
