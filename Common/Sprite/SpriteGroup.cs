using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class SpriteGroup : MonoBehaviour
{
    private static readonly int COLOR_ID = Shader.PropertyToID("_Color");
    private static readonly int MAINTEX_ID = Shader.PropertyToID("_MainTex");

    [Range(0, 1f)]
    [SerializeField, OnValueChanged("ApplyAlphaToAll")] private float _alpha = 1f;

    [ShowInInspector] private readonly Dictionary<SpriteRenderer, MaterialPropertyBlock> _renderers = new();

    private void OnEnable()
    {
        RefreshChildren();
        ApplyAlphaToAll();
    }

    private void OnTransformChildrenChanged()
    {
        RefreshChildren();
    }

    private void RefreshChildren()
    {
        SpriteRenderer[] allChildren = GetComponentsInChildren<SpriteRenderer>(true);
        List<SpriteRenderer> toRemove = new List<SpriteRenderer>();
        foreach (var sr in _renderers.Keys)
        {
            if (sr == null || !sr.transform.IsChildOf(transform))
                toRemove.Add(sr);
        }
        foreach (var sr in toRemove)
            _renderers.Remove(sr);

        foreach (var sr in allChildren)
        {
            if (!_renderers.ContainsKey(sr))
            {
                var block = new MaterialPropertyBlock();
                if (sr.sprite != null)
                    block.SetTexture(MAINTEX_ID, sr.sprite.texture);

                _renderers.Add(sr, block);
            }
        }

        ApplyAlphaToAll();
    }

    private void ApplyAlphaToAll()
    {
        foreach (var pair in _renderers)
        {
            SpriteRenderer sr = pair.Key;
            MaterialPropertyBlock block = pair.Value;

            if (sr == null) continue;

            Color baseColor = Color.white;
            if (sr.sharedMaterial != null && sr.sharedMaterial.HasProperty(COLOR_ID))
                baseColor = sr.sharedMaterial.GetColor(COLOR_ID);

            baseColor.a = _alpha;
            block.SetColor(COLOR_ID, baseColor);
            sr.SetPropertyBlock(block);
        }
    }

    public void SetAlpha(float alpha)
    {
        _alpha = Mathf.Clamp01(alpha);
        ApplyAlphaToAll();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            RefreshChildren();
            ApplyAlphaToAll();
        }
    }
#endif
}
