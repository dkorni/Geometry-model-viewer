using System.Collections.Generic;
using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    /// <summary>
    /// Часник
    /// </summary>
    [CreateAssetMenu(fileName = "Garlic", menuName = "DrawSrtrategies/Garlic")]
    public class Garlic : Sphere
    {
        public override Vector3 GetPoint(float radius, float u, float v)
        {
            var point = base.GetPoint(radius, u, v);

            point.x = point.x * (1 + 0.5f * Mathf.Abs(Mathf.Sin(2 * v)));

            point.y = point.y * (1 + 0.5f * Mathf.Abs(Mathf.Sin(2 * v)));

            if (u > 0)
                point.z = point.z + Radius * Mathf.Pow((u / (90 * Mathf.Deg2Rad)), 5);

            return point;
        }
    }
}