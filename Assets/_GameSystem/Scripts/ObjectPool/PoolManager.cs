using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Konzit.GameSystem
{
    [Serializable]
    public class PoolManager<T> : IDisposable where T : ItemPooling
    {
        [SerializeField] private T _itemPooling;
        [SerializeField] private Transform _parent;
        [SerializeField, Min(1)] private int _defaultCapacity = 10;
        [SerializeField, Min(1)] private int _maxSize = 100;

        private readonly HashSet<T> _items = new();
        private ObjectPool<T> _pool;
        private bool _disposed;

        public int CountInactive => _pool?.CountInactive ?? 0;
        public int CountActive => _items.Count - CountInactive;

        public void Initialize()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PoolManager<T>));
            if (_pool != null)
                return;
            if (_itemPooling == null)
                throw new InvalidOperationException("Assign a pooling prefab before initializing the pool.");

            int maxSize = Mathf.Max(1, _maxSize);
            _pool = new ObjectPool<T>(
                CreateItem, null, null, DestroyItem,
                collectionCheck: true,
                defaultCapacity: Mathf.Clamp(_defaultCapacity, 1, maxSize),
                maxSize: maxSize);
        }

        private T CreateItem()
        {
            var item = UnityEngine.Object.Instantiate(_itemPooling, _parent);
            item.gameObject.SetActive(false);
            item.AttachToPool(this, ReturnItem);
            _items.Add(item);
            return item;
        }

        public T GetItem()
        {
            return GetItem(_parent != null ? _parent.position : Vector3.zero,
                _parent != null ? _parent.rotation : Quaternion.identity);
        }

        public T GetItem(Vector3 position, Quaternion rotation)
        {
            Initialize();
            var item = _pool.Get();
            while (item == null)
            {
                _items.Remove(item);
                item = _pool.Get();
            }

            item.transform.SetParent(_parent, false);
            item.transform.SetPositionAndRotation(position, rotation);
            item.TakeFromPool();
            return item;
        }

        public void ReturnPool(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (!item.IsOwnedBy(this))
                throw new InvalidOperationException("This item belongs to a different pool.");
            if (!item.IsSpawned)
                return;

            item.StoreInPool();
            item.transform.SetParent(_parent, false);
            _pool.Release(item);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _pool?.Clear();
            foreach (var item in _items)
            {
                if (item == null)
                    continue;

                item.DetachFromPool();
                DestroyObject(item.gameObject);
            }
            _items.Clear();
            _pool = null;
        }

        private void ReturnItem(ItemPooling item)
        {
            ReturnPool((T)item);
        }

        private void DestroyItem(T item)
        {
            _items.Remove(item);
            if (item == null)
                return;

            item.DetachFromPool();
            DestroyObject(item.gameObject);
        }

        private static void DestroyObject(GameObject item)
        {
            if (Application.isPlaying)
                UnityEngine.Object.Destroy(item);
            else
                UnityEngine.Object.DestroyImmediate(item);
        }
    }
}
