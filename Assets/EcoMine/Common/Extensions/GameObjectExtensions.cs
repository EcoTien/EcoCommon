using System;
using UnityEngine;

namespace EcoMine.Common.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Check game object is prefab
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">GameObject null</exception>
        public static bool IsPrefab(this GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));

            return
                !gameObject.scene.IsValid()
                && !gameObject.scene.isLoaded
                && gameObject.GetInstanceID() >= 0
                && !gameObject.hideFlags.HasFlag(HideFlags.HideInHierarchy);
        }

        /// <summary>
        /// Try Get Component All Parent And This 
        /// </summary>
        /// <param name="gameObject">this</param>
        /// <param name="component">out object</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">GameObject null</exception>
        public static bool TryGetComponentAllParent<T>(this GameObject gameObject, out T component)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));
            
            if (gameObject.TryGetComponent(out component))
                return true;

            Transform currentTrans = gameObject.transform.parent;

            while (currentTrans != null)
            {
                if (currentTrans.TryGetComponent(out component))
                    return true;
                
                currentTrans = currentTrans.parent;
            }
            
            return false;
        }
        
        /// <summary>
        /// Get lowest child
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Transform GetLowestChild(this GameObject gameObject)
        {
            if (gameObject == null)
                throw new ArgumentNullException(nameof(gameObject));

            if (gameObject.transform.childCount == 0)
                return gameObject.transform;

            var childCount = gameObject.transform.childCount;
            int selectId = childCount - 1;

            Transform currentTrans = gameObject.transform.GetChild(selectId);
            
            while (selectId > 0)
            {
                if (currentTrans.gameObject.activeInHierarchy)
                    break;
                selectId--;

                if (selectId > 0)
                    currentTrans = gameObject.transform.GetChild(selectId);
                else
                    return gameObject.transform;
            }

            while (currentTrans != null)
            {
                if (currentTrans.childCount == 0)
                    return currentTrans;
                
                int childSelectId = childCount - 1;
                Transform selectTran = currentTrans.GetChild(childSelectId);
                
                while (childSelectId > 0)
                {
                    if (selectTran.gameObject.activeInHierarchy)
                        break;
                    
                    childSelectId--;

                    if (childSelectId > 0)
                        selectTran = currentTrans.GetChild(childSelectId);
                    else
                        return currentTrans.transform;
                }
                currentTrans = selectTran;
            }
            
            return null;
        }
    }
}
