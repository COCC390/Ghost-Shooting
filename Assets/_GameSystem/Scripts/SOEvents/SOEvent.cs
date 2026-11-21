using System.Collections.Generic;
using UnityEngine;

namespace Konzit.GameSystem
{
    [CreateAssetMenu(fileName = "SOEvents", menuName = "Events/SOEvents")]
    public class SOEvent : ScriptableObject
    {
        private HashSet<SOEventListener> _eventListener = new();
        private List<SOEventListener> _tempRemove = new();
        private bool _isRasing = false;

        public void AddListener(SOEventListener listener)
        {
            if(!_eventListener.Contains(listener))
                _eventListener.Add(listener);
        }

        public void RemoveListener(SOEventListener listener)
        {
            if(!_isRasing)
            {
                _eventListener.Remove(listener);
            }
            else
            {
                _tempRemove.Add(listener);
            }
        }

        public void Raise()
        {
            _isRasing = true;
            foreach (SOEventListener listener in _eventListener)
            {
                listener.OnEventRaised();
            }
            _isRasing = false;

            LateRemove();
        }

        private void LateRemove()
        {
            if (_tempRemove.Count > 0)
            {
                foreach (SOEventListener listener in _tempRemove)
                {
                    _eventListener.Remove(listener);
                }
                _tempRemove.Clear();
            }
        }
    }
}