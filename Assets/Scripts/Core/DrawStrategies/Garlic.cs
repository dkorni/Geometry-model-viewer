using System.Collections.Generic;
using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    /// <summary>
    /// Часник
    /// </summary>
    [CreateAssetMenu(fileName = "Garlic", menuName = "DrawSrtrategies/Garlic")]
    public class Garlic : DrawSrtrategy
    {
        public float Radius = 1;
        public int Segments = 25;

        public override Mesh Draw()
        {          
            vertices = new Vector3[(Segments + 1) * (Segments + 1)];
            triangles = new int[Segments * Segments * 6];
            uvCordinates = new Vector2[(Segments + 1) * (Segments + 1)];

            int index = 0;

            var list_horizontal = new List<Vector3>();

            for (int i = 0; i <= Segments; i++)
            {
                float phi = 2 * Mathf.PI * i / Segments;

                for (int j = 0; j <= Segments; j++)
                {
                    float teta = Mathf.PI * j / Segments;

                    var spherePoint = GeometryMath.GetSpherePoint(Radius, teta, phi);
                    var uv = UVCordinates.GetFromSpheerePoint(spherePoint);

                    float x = spherePoint.x * (1 + 0.5f*Mathf.Abs(Mathf.Sin(2*uv.U)));
                    
                    float y = spherePoint.y;

                    float z = spherePoint.z * (1 + 0.5f * Mathf.Abs(Mathf.Sin(2 * uv.U)));

                    if (uv.V > 0)
                        z = z + Radius * Mathf.Pow((uv.V / (90 * Mathf.Deg2Rad)), 5);                    

                    vertices[index] = new Vector3(x, y, z);
                    uvCordinates[index] = uv.ToVector2();
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