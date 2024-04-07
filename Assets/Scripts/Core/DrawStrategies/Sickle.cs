using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    /// <summary>
    /// Серп
    /// </summary>
    [CreateAssetMenu(fileName = "Sickle", menuName = "DrawSrtrategies/Sickle")]
    public class Sickle : Sphere
    {
        public override Vector3 GetPoint(float radius, float u, float v)
        {
            var point = base.GetPoint(radius, u, v);

            point.x = point.x + Mathf.Pow(point.z, 2) / Radius;
            point.z = point.z * 2;

            return point;
        }
    }
}