using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    /// <summary>
    /// Крапля
    /// </summary>
    [CreateAssetMenu(fileName = "Drop", menuName = "DrawSrtrategies/Drop")]
    public class Drop : DrawSrtrategy
    {
        public float Radius = 1;
        public int Segments = 25;

        public override Mesh Draw()
        {          
            vertices = new Vector3[(Segments + 1) * (Segments + 1)];
            triangles = new int[Segments * Segments * 6];

            int index = 0;

            for (int i = 0; i <= Segments; i++)
            {
                float phi = 2 * Mathf.PI * i / Segments;

                for (int j = 0; j <= Segments; j++)
                {
                    float teta = Mathf.PI * j / Segments;
                    
                    var spherePoint = GeometryMath.GetSpherePoint(Radius, teta, phi);
                    var uv = UVCordinates.GetFromSpheerePoint(spherePoint);

                    float x = spherePoint.x;
                    
                    float y = spherePoint.y;

                    float z = spherePoint.z;

                    if (uv.V > 0)
                        z = z + Radius * Mathf.Pow((uv.V / (90 * Mathf.Deg2Rad)), 4);
                    
                    vertices[index] = new Vector3(x, y, z);
                    index++;
                }
            }

            index = 0;

            for (int i = 0; i < Segments; i++)
            {
                for (int j = 0; j < Segments; j++)
                {
                    int a = (Segments + 1) * i + j;
                    int b = (Segments + 1) * i + j + 1;
                    int c = (Segments + 1) * (i + 1) + j;
                    int d = (Segments + 1) * (i + 1) + j + 1;

                    triangles[index++] = a;
                    triangles[index++] = b;
                    triangles[index++] = c;
                    triangles[index++] = c;
                    triangles[index++] = b;
                    triangles[index++] = d;
                }
            }

            var mesh = BuildMesh();

            return mesh;
        }
    }
}