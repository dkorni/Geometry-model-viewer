using UnityEngine;

namespace GMV.Core
{
    public static class GeometryMath
    {
        public static Vector3 GetSpherePoint(float radius, float u, float v)
        {
            return new Vector3(
                GetSpheereX(radius, u, v),
                GetSpheereY(radius, u, v),
                GetSpheereZ(radius, u)
                );
        }

        public static float GetSpheereX(float radius, float u, float v)
        {
            return radius * Mathf.Cos(u) * Mathf.Cos(v);
        }

        public static float GetSpheereY(float radius, float u, float v)
        {
            return radius * Mathf.Cos(u) * Mathf.Sin(v);
        }

        public static float GetSpheereZ(float radius, float u)
        {
            return radius * Mathf.Sin(u);
        }

        public static float GetTorusX(float majorRadius, float minorRadius, float u, float v)
        {
            return (majorRadius + minorRadius * Mathf.Cos(v)) * Mathf.Cos(u);
        }

        public static float GetTorusY(float minorRadius, float v)
        {
            return minorRadius * Mathf.Sin(v);
        }

        public static float GetTorusZ(float majorRadius, float minorRadius, float u, float v)
        {
            return (majorRadius + minorRadius * Mathf.Cos(v)) * Mathf.Sin(u);
        }
        public static float GetCylinderX(float radius, float teta)
        {
            return radius * Mathf.Cos(teta);
        }

        public static float GetCylinderY(float radius, float teta)
        {
            return radius * Mathf.Sin(teta);
        }

    }
}