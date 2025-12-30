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
    }
}