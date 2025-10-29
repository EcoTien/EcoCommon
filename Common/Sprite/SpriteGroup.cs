using System.Linq;

namespace EcoMine.Common
{
    using System.Collections.Generic;
    using UnityEngine;

    [DisallowMultipleComponent]
    public sealed class SpriteGroup : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _alpha = 1f; 
        [SerializeField] private bool _visible = true; // ẩn/hiện cả nhóm

        public float alpha
        {
            get => _alpha;
            set
            {
                value = Mathf.Clamp01(value);
                if (!Mathf.Approximately(_alpha, value))
                {
                    _alpha = value;
                    _dirtyAlpha = true;
                    _anyDirty = true;
                }
            }
        }

        public bool visible
        {
            get => _visible;
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    _dirtyVisible = true;
                    _anyDirty = true;
                }
            }
        }

        struct Cached
        {
            public SpriteRenderer r;
            public float baseA;
            public bool lastEnabled;
            public Color lastColor;
        }

        private readonly List<Cached> _items = new List<Cached>(64);
        private bool _anyDirty = true;
        private bool _dirtyAlpha = true;
        private bool _dirtyVisible = true;

        private static List<SpriteRenderer> _temp = new List<SpriteRenderer>(128);

        void OnEnable()
        {
            Refresh();
            Apply();
        }

        void OnValidate()
        {
            _alpha = Mathf.Clamp01(_alpha);
            _anyDirty = _dirtyAlpha = _dirtyVisible = true;
        }

        void LateUpdate()
        {
            if (_anyDirty) Apply();
        }

        void OnTransformChildrenChanged()
        {
            Refresh();
            _anyDirty = true;
        }

        public void Refresh()
        {
            _items.Clear();
            _temp.Clear();

            _temp = GetComponentsInChildren<SpriteRenderer>(true).ToList();

            for (int i = 0, n = _temp.Count; i < n; i++)
            {
                var r = _temp[i];
                if (r == null) continue;

                var c = r.color;
                _items.Add(new Cached
                {
                    r = r,
                    baseA = c.a,
                    lastEnabled = r.enabled,
                    lastColor = c
                });
            }

            _anyDirty = _dirtyAlpha = _dirtyVisible = true;
        }

        public void Apply()
        {
            if (!_anyDirty) return;

            bool targetEnabled = _visible && (_alpha > 0f);

            for (int i = 0, n = _items.Count; i < n; i++)
            {
                var it = _items[i];
                var r = it.r;
                if (r == null) continue;

                if (it.lastEnabled != targetEnabled)
                {
                    r.enabled = targetEnabled;
                    it.lastEnabled = targetEnabled;
                }

                if (_dirtyAlpha)
                {
                    float newA = it.baseA * _alpha;
                    if (!Mathf.Approximately(it.lastColor.a, newA))
                    {
                        var col = it.lastColor;
                        col.a = newA;
                        r.color = col;
                        it.lastColor = col;
                    }
                }

                _items[i] = it;
            }

            _dirtyAlpha = _dirtyVisible = false;
            _anyDirty = false;
        }

        public void SetAlphaImmediate(float value)
        {
            value = Mathf.Clamp01(value);
            if (!Mathf.Approximately(_alpha, value))
            {
                _alpha = value;
                _dirtyAlpha = _anyDirty = true;
            }

            Apply();
        }

        public void SetVisibleImmediate(bool value)
        {
            if (_visible != value)
            {
                _visible = value;
                _dirtyVisible = _anyDirty = true;
            }

            Apply();
        }

        public void RebaselineBaseAlpha()
        {
            for (int i = 0, n = _items.Count; i < n; i++)
            {
                var it = _items[i];
                if (it.r == null) continue;
                var c = it.r.color;
                it.baseA = c.a; 
                it.lastColor = c;
                _items[i] = it;
            }

            _dirtyAlpha = _anyDirty = true;
            Apply();
        }
    }
}