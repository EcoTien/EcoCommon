using System;
using System.Collections;
using UnityEngine;

namespace JacatGames.Common.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void DelayCall(this MonoBehaviour mono, float duration, Action callBack, bool ignoreTimeScale = true)
        {
            if (ignoreTimeScale)
                mono.StartCoroutine(IEDelayCallIgnoreTimeScale(duration, callBack));
            else
                mono.StartCoroutine(IEDelayCall(duration, callBack));
        }

        static IEnumerator IEDelayCallIgnoreTimeScale(float duration, Action callBack)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            callBack?.Invoke();
        }

        static IEnumerator IEDelayCall(float duration, Action callBack)
        {
            float startTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup - startTime < duration)
                yield return null;
            callBack?.Invoke();
        }
    }
}