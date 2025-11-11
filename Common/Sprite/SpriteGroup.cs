using System;
using EcoMine.Common.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EcoMine.Common
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public sealed class SpriteGroup : MonoBehaviour
    {
        [SerializeField, Range(0, 1f), OnValueChanged("AlphaChange")/*, ShowIf("IsPlayingMode")*/] private float _alphaDebugger = 1f;
        
        /*[HideInInspector] public SpriteData[] spritesData;*/
        [HideInInspector] public SpriteRenderer[] spriteRenderers;

        public void SetAlpha(float alpha)
        {
            _alphaDebugger = alpha;
            foreach (var spriteRenderer in spriteRenderers)
                spriteRenderer.color = spriteRenderer.color.SetAlpha(alpha);
        }

        private void AlphaChange()
        {
            SetAlpha(_alphaDebugger);
        }
        
#if UNITY_EDITOR
        private void LateUpdate()
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        }
#endif 
        
        /*private bool IsPlayingMode => Application.isPlaying;
#if UNITY_EDITOR
        private void LateUpdate()
        {
            if(IsPlayingMode) return;
            
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
            spritesData = new SpriteData[spriteRenderers.Length];
            for (var i = 0; i < spriteRenderers.Length; i++)
            {
                SpriteRenderer spriteRenderer = spriteRenderers[i];
                SpriteData spriteData = new SpriteData();
                spriteData.renderer = spriteRenderer;
                spriteData.alpha = spriteRenderer.color.GetAlpha();
                spritesData[i] = spriteData;
            }
        }
#endif*/
    }
}