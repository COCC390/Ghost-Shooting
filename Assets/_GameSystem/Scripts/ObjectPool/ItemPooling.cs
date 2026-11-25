using System;
using UnityEngine;

namespace Konzit.GameSystem
{
    public class ItemPooling : MonoBehaviour
    {
        private object _owner;
        private Action<ItemPooling> _returnToPool;

        public bool IsSpawned { get; private set; }

        public void ReturnToPool()
        {
            if (IsSpawned)
                _returnToPool?.Invoke(this);
        }

        internal void AttachToPool(object owner, Action<ItemPooling> returnToPool)
        {
            _owner = owner;
            _returnToPool = returnToPool;
        }

        internal bool IsOwnedBy(object owner)
        {
            return ReferenceEquals(_owner, owner);
        }

        internal void TakeFromPool()
        {
            IsSpawned = true;
            OnTakenFromPool();
            if (IsSpawned)
                gameObject.SetActive(true);
        }

        internal void StoreInPool()
        {
            IsSpawned = false;
            OnReturnedToPool();
            gameObject.SetActive(false);
        }

        internal void DetachFromPool()
        {
            IsSpawned = false;
            _owner = null;
            _returnToPool = null;
        }

        protected virtual void OnTakenFromPool() { }

        protected virtual void OnReturnedToPool() { }
    }
}
