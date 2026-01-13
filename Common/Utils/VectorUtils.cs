using System.Collections.Generic;
using UnityEngine;

namespace EcoMine.Common.Utils
{
    public class VectorUtils
    {
        public static bool IsRotationInRange(float rotation, float minAngle, float maxAngle)
        {
            float center = (minAngle + maxAngle) * 0.5f;
            float halfRange = Mathf.Abs(maxAngle - minAngle) * 0.5f;

            float delta = Mathf.Abs(Mathf.DeltaAngle(rotation, center));

            return delta <= halfRange;
        }

        public static float GetAngleCenter(float minAngle, float maxAngle)
        {
            float delta = Mathf.DeltaAngle(minAngle, maxAngle);
            return Mathf.Repeat(minAngle + delta * 0.5f, 360f);
        }

        public static Vector3 RandomVector(Vector3 min, Vector3 max)
        {
            return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
        }

        public static List<Vector3> DeterministicGeneratePoints(
            Vector3 origin,
            int count,
            Vector3 radius
        )
        {
            var results = new List<Vector3>(count);

            results.Add(origin);

            if (count <= 1)
                return results;

            int seed =
                origin.x.GetHashCode() ^
                origin.y.GetHashCode() ^
                origin.z.GetHashCode();

            var random = new System.Random(seed);

            for (int i = 1; i < count; i++)
            {
                float x = (float)(random.NextDouble() * 2 - 1) * radius.x;
                float y = (float)(random.NextDouble() * 2 - 1) * radius.y;
                float z = (float)(random.NextDouble() * 2 - 1) * radius.z;

                results.Add(origin + new Vector3(x, y, z));
            }

            return results;
        }
    }
}