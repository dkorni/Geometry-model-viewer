using System;
using UnityEngine;

namespace GMV.Core
{
    public class UVCordinates
    {
        public float U { get; set; }
        public float V { get; set; }

        public UVCordinates(float u, float v)
        {
            U = u;
            V = v;
        }

        /// <summary>
        /// Конвертація координати точки сфери у UV координати
        /// </summary>
        /// <param name="latitude">Широта</param>
        /// <param name="longitude">Довгота</param>
        /// <returns></returns>
        public static UVCordinates GetFromSpheerePoint(float latitude, float longitude)
        {
            var u = (float)(latitude / (2 * Math.PI) + 0.5f);
            var v = (float)(longitude / (Math.PI) + 0.5f);
            return new UVCordinates(u, v);
        }

        public static UVCordinates GetFromSpheerePoint(Vector3 point)
        {
            point = point.normalized;
            var u = (float)( Math.Atan2(point.x, point.y) / (2 * Math.PI) + 0.5);
            var v = (float)(Math.Asin(point.z) / Math.PI + .5);

            return new UVCordinates(u, v);
        }

        public Vector2 ToVector2() => new Vector2(U, V);
    }
}