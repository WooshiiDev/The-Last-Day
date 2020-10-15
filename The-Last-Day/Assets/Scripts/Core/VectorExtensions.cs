using UnityEngine;

namespace LD
    {
    public static class VectorExtensions
        {
        public enum SwizzleType { XZY, YXZ, YZX, ZXY, ZYX }

        /// <summary>
        /// Switch vector XYZ components with other values
        /// </summary>
        /// <param name="swizzleType">The swizzle operation to perform</param>
        public static Vector3 Swizzle(this Vector3 vector, SwizzleType swizzleType)
            {
            return GetSwizzeVector (vector, swizzleType);
            }

        /// <summary>
        /// Switch the x and y values for the vector2
        /// </summary>
        public static Vector2 Swizzle(this Vector2 vector)
            {
            vector.Set (vector.y, vector.x);
            return vector;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 ToVector3Plane(this Vector2 vector)
            {
            return new Vector3(vector.x, 0, vector.y);
            }

        #region Helpers

        private static Vector3 GetSwizzeVector(Vector3 vector, SwizzleType swizzleType)
            {
            switch (swizzleType)
                {
                case SwizzleType.XZY:
                    vector.Set (vector.x, vector.z, vector.y);
                    break;
                case SwizzleType.YXZ:
                    vector.Set ( vector.z, vector.x, vector.y);
                    break;
                case SwizzleType.YZX:
                    vector.Set (vector.y, vector.z, vector.x);
                    break;
                case SwizzleType.ZXY:
                    vector.Set (vector.z, vector.x, vector.y);
                    break;
                case SwizzleType.ZYX:
                    vector.Set (vector.z, vector.y, vector.x);
                    break;
                }

            return vector;
            }

        #endregion
        }
    }
