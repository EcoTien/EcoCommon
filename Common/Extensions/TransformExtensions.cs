using UnityEngine;

namespace EcoMine.Common.Extensions
{
    public static class TransformExtensions
    {
        #region LocalScale
        public static void SetLocalScale(this Transform transform, Vector3 localScale) => transform.localScale = localScale;
        #endregion
    }
}