using System;
using System.Collections;
using UnityEngine;

namespace JacatGames.Common.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void DelayCall(this MonoBehaviour mono, float duration, Action callBack, bool ignoreTimeScale = true)
        {
            mono.StartCoroutine(IEDelayCall(duration, callBack, ignoreTimeScale));
        }

        static IEnumerator IEDelayCall(float duration, Action callBack, bool ignoreTimeScale)
        {
            if (ignoreTimeScale)
                yield return new WaitForSecondsRealtime(duration);
            else
                yield return new WaitForSeconds(duration);
            
            callBack?.Invoke();
        }
    }
}