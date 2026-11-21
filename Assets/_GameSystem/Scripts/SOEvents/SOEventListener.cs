using UnityEngine;
using UnityEngine.Events;

namespace Konzit.GameSystem
{
    public class SOEventListener : MonoBehaviour
    {
        [SerializeField] private SOEvent _eventManager;
        [SerializeField] private UnityEvent _gameEvent;

        public void OnEnable() => _eventManager?.AddListener(this);
        public void OnDisable() => _eventManager?.RemoveListener(this);

        public void OnEventRaised() => _gameEvent?.Invoke();
    }
}
