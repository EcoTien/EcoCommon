using UnityEngine;

namespace EcoMine.Common.Extensions
{
    public static class VectorExtension
    {
        public static Vector3 ToEuler360(this Vector3 vector3)
        {
            float x = (vector3.x + 360) % 360;
            float y = (vector3.y + 360) % 360;
            float z = (vector3.z + 360) % 360;
            return new Vector3(x, y, z);
        }

        #region Mutiply

        public static Vector3 Multiply(this Vector3 origin, Vector3 target)
        {
            return new Vector3(origin.x * target.x, origin.y * target.y, origin.z * target.z);
        }
        
        public static Vector3 Multiply(this Vector3 origin, float target)
        {
            return Multiply(origin, new Vector3(target, target, target));
        }
        
        public static Vector3 MultiplyX(this Vector3 origin, float target)
        {
            return new Vector3(origin.x * target, origin.y, origin.z);
        }
        
        public static Vector3 MultiplyY(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y * target, origin.z);
        }
        
        public static Vector3 MultiplyZ(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y, origin.z * target);
        }

        #endregion

        #region Add
        public static Vector3 Add(this Vector3 origin, Vector3 target)
        {
            return new Vector3(origin.x + target.x, origin.y + target.y, origin.z + target.z);
        }
        
        public static Vector3 Add(this Vector3 origin, float target)
        {
            return Add(origin, new Vector3(target, target, target));
        }
        
        public static Vector3 AddX(this Vector3 origin, float target)
        {
            return new Vector3(origin.x + target, origin.y, origin.z);
        }
        
        public static Vector3 AddY(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y + target, origin.z);
        }
        
        public static Vector3 AddZ(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y, origin.z + target);
        }
        #endregion
        
        #region Divide
        public static Vector3 Divide(this Vector3 origin, Vector3 target)
        {
            return new Vector3(origin.x / target.x, origin.y / target.y, origin.z / target.z);
        }
        
        public static Vector3 Divide(this Vector3 origin, float target)
        {
            return Divide(origin, new Vector3(target, target, target));
        }
        
        public static Vector3 DivideX(this Vector3 origin, float target)
        {
            return new Vector3(origin.x / target, origin.y, origin.z);
        }
        
        public static Vector3 DivideY(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y / target, origin.z);
        }
        
        public static Vector3 DivideZ(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y, origin.z / target);
        }
        #endregion
        
        #region Subtract
        public static Vector3 Subtract(this Vector3 origin, Vector3 target)
        {
            return new Vector3(origin.x - target.x, origin.y - target.y, origin.z - target.z);
        }
        
        public static Vector3 Subtract(this Vector3 origin, float target)
        {
            return Subtract(origin, new Vector3(target, target, target));
        }
        
        public static Vector3 SubtractX(this Vector3 origin, float target)
        {
            return new Vector3(origin.x - target, origin.y, origin.z);
        }
        
        public static Vector3 SubtractY(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y - target, origin.z);
        }
        
        public static Vector3 SubtractZ(this Vector3 origin, float target)
        {
            return new Vector3(origin.x, origin.y, origin.z - target);
        }
        #endregion
    }
}