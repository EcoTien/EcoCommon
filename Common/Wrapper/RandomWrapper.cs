using UnityEngine;

namespace JacatGames.Common.Utils
{
    public static class RandomWrapper
    {
        public static bool Rate(float rate)
        {
            float random = UnityEngine.Random.Range(0f, 100f);
            return rate >= random;
        }
    }
}