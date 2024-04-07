using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    /// <summary>
    /// Груша
    /// </summary>
    [CreateAssetMenu(fileName = "Pear", menuName = "DrawSrtrategies/Pear")]
    public class Pear : Sphere
    {
        public override Vector3 GetPoint(float radius, float u, float v)
        {
            var point = base.GetPoint(radius, u, v);

            if (point.z > Radius / 2)
                point.z = point.z + 2.5f * Radius * Mathf.Pow(point.z / Radius - 0.5f, 2);

            return point;
        }
    }
}