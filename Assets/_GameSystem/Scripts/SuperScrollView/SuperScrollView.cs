using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Konzit.GameSystem
{
    [RequireComponent(typeof(ScrollRect))]
    public class SuperScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private RectTransform _content;
        [SerializeField] private ScrollViewItem _scrollViewItemPrefab;
        [SerializeField, Min(0)] private int _extraVisibleItems = 2;
        [SerializeField, Min(0f)] private float _itemSpacing;

        private readonly List<ScrollViewItem> _itemPool = new();
        private IScrollDataSource _dataSource;
        private float _itemHeight;
        private int _firstBoundIndex = -1;
        private bool _isInitialized;

        private void Awake()
        {
            ValidateReferences();
        }

        private void OnEnable()
        {
            if (_scrollRect != null)
            {
                _scrollRect.onValueChanged.AddListener(OnScrollChanged);
            }
        }

        private void Start()
        {
            Refresh();
        }

        private void OnDisable()
        {
            if (_scrollRect != null)
            {
                _scrollRect.onValueChanged.RemoveListener(OnScrollChanged);
            }
        }

        public void SetData<TModel>(IReadOnlyList<TModel> data)
        {
            _dataSource = new ScrollDataSource<TModel>(data);
            Refresh();
        }

        public void Clear()
        {
            _dataSource = null;
            Refresh();
        }

        public void Refresh()
        {
            if (!isActiveAndEnabled || !ValidateReferences())
            {
                return;
            }

            Canvas.ForceUpdateCanvases();
            InitializeIfNeeded();
            UpdateContentSize();
            RebindVisibleItems(true);
        }

        private bool ValidateReferences()
        {
            return _scrollRect != null &&
                   _viewport != null &&
                   _content != null &&
                   _scrollViewItemPrefab != null;
        }

        private void InitializeIfNeeded()
        {
            if (_isInitialized)
            {
                return;
            }

            var itemRect = _scrollViewItemPrefab.transform as RectTransform;
            _itemHeight = itemRect.rect.height;
            if (_itemHeight <= 0f)
            {
                Debug.LogError("Scroll view item height must be greater than zero.", this);
                return;
            }

            _content.anchorMin = new Vector2(0f, 1f);
            _content.anchorMax = new Vector2(1f, 1f);
            _content.pivot = new Vector2(0.5f, 1f);
            _scrollViewItemPrefab.gameObject.SetActive(false);
            _isInitialized = true;
        }

        private void UpdateContentSize()
        {
            var dataCount = _dataSource?.Count ?? 0;
            var contentHeight = dataCount == 0 ? 0f : dataCount * GetItemStride() - _itemSpacing;
            _content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Max(_viewport.rect.height, contentHeight));
        }

        private void RebindVisibleItems(bool force)
        {
            if (!_isInitialized)
            {
                return;
            }

            var dataCount = _dataSource?.Count ?? 0;
            if (dataCount == 0)
            {
                SetPoolActiveCount(0);
                _firstBoundIndex = -1;
                return;
            }

            var visibleItemCount = Mathf.CeilToInt(_viewport.rect.height / GetItemStride());
            var poolSize = Mathf.Min(dataCount, visibleItemCount + _extraVisibleItems);
            EnsurePoolSize(poolSize);

            var maxFirstIndex = Mathf.Max(0, dataCount - poolSize);
            var scrollOffset = Mathf.Max(0f, _content.anchoredPosition.y);
            var firstVisibleIndex = Mathf.FloorToInt(scrollOffset / GetItemStride());
            var firstIndex = Mathf.Clamp(firstVisibleIndex - _extraVisibleItems / 2, 0, maxFirstIndex);

            if (!force && firstIndex == _firstBoundIndex)
            {
                return;
            }

            _firstBoundIndex = firstIndex;
            for (var poolIndex = 0; poolIndex < poolSize; poolIndex++)
            {
                var dataIndex = firstIndex + poolIndex;
                var item = _itemPool[poolIndex];
                PositionItem(item.transform as RectTransform, dataIndex);
                item.Bind(_dataSource.Get(dataIndex), dataIndex);
                item.gameObject.SetActive(true);
            }

            SetPoolActiveCount(poolSize);
        }

        private void EnsurePoolSize(int requiredPoolSize)
        {
            while (_itemPool.Count < requiredPoolSize)
            {
                var item = Instantiate(_scrollViewItemPrefab, _content);
                item.gameObject.SetActive(false);
                _itemPool.Add(item);
            }
        }

        private void SetPoolActiveCount(int activeCount)
        {
            for (var index = activeCount; index < _itemPool.Count; index++)
            {
                var item = _itemPool[index];
                item.Unbind();
                item.gameObject.SetActive(false);
            }
        }

        private void PositionItem(RectTransform itemRect, int dataIndex)
        {
            itemRect.anchorMin = new Vector2(0f, 1f);
            itemRect.anchorMax = new Vector2(1f, 1f);
            itemRect.pivot = new Vector2(0.5f, 1f);
            itemRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _itemHeight);
            itemRect.anchoredPosition = new Vector2(0f, -dataIndex * GetItemStride());
        }

        private float GetItemStride()
        {
            return _itemHeight + _itemSpacing;
        }

        private void OnScrollChanged(Vector2 _)
        {
            RebindVisibleItems(false);
        }

        private interface IScrollDataSource
        {
            int Count { get; }
            object Get(int index);
        }

        private sealed class ScrollDataSource<TModel> : IScrollDataSource
        {
            private readonly IReadOnlyList<TModel> _data;

            public ScrollDataSource(IReadOnlyList<TModel> data)
            {
                _data = data ?? Array.Empty<TModel>();
            }

            public int Count => _data.Count;

            public object Get(int index)
            {
                return _data[index];
            }
        }
    }
}
