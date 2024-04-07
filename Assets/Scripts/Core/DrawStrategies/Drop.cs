using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    /// <summary>
    /// Крапля
    /// </summary>
    [CreateAssetMenu(fileName = "Drop", menuName = "DrawSrtrategies/Drop")]
    public class Drop : Sphere
    {
        public override Vector3 GetPoint(float radius, float u, float v)
        {
            var point = base.GetPoint(radius, u, v);         
            
            if (u > 0)
                point.z = point.z + Radius * Mathf.Pow((u / (90 * Mathf.Deg2Rad)), 4);

            return point;
        }
    }
}