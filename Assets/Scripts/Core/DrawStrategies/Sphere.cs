using System;
using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    [CreateAssetMenu(fileName = "Spheere", menuName = "DrawSrtrategies/Spheere")]
    public class Sphere : DrawSrtrategy
    {
        public float Radius = 1;
        public int Segments = 25;

        public override Mesh Draw()
        {          
            vertices = new Vector3[(Segments + 1) * (Segments + 1)];
            triangles = new int[Segments * Segments * 6];
            uvCordinates = new Vector2[(Segments + 1) * (Segments + 1)];

            int index = 0;


            for (int i = 0; i <= Segments; i++)
            {
                float u = Mathf.PI * i / Segments;

                for (int j = 0; j <= Segments; j++)
                {
                    float v = 2 * Mathf.PI * j / Segments;

                    var vertex = GeometryMath.GetSpherePoint(Radius, u, v);

                    vertices[index] = vertex;
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