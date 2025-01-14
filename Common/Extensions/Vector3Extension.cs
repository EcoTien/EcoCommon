using UnityEngine;

namespace EcoMine.Common.Extensions
{
    public static class Vector3Extension
    {
        public static Vector3 ToEuler360(this Vector3 vector3)
        {
            float x = (vector3.x + 360) % 360;
            float y = (vector3.y + 360) % 360;
            float z = (vector3.z + 360) % 360;
            return new Vector3(x, y, z);
        }
    }
}