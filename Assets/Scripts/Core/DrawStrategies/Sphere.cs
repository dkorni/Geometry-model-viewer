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

            int index = 0;


            for (int i = 0; i <= Segments; i++)
            {
                float u = Mathf.PI * (-0.5f + (1f * i / Segments));

                for (int j = 0; j <= Segments; j++)
                {
                    float v = 2 * Mathf.PI * j / Segments;

                    var vertex = GetPoint(Radius, u, v);

                    vertices[index] = vertex;
                    index++;
                }
            }

            index = 0;

            for (int i = 0; i < Segments; i++)
            {
                for (int j = 0; j < Segments; j++)
                {
                    int vertexIndex = i * (Segments + 1) + j;
                    triangles[index++] = vertexIndex;
                    triangles[index++] = vertexIndex + Segments + 1;
                    triangles[index++] = vertexIndex + 1;

                    triangles[index++] = vertexIndex + 1;
                    triangles[index++] = vertexIndex + Segments + 1;
                    triangles[index++] = vertexIndex + Segments + 2;
                }
            }

            var mesh = BuildMesh();

            return mesh;
        }

        public virtual Vector3 GetPoint(float radius, float u, float v) => GeometryMath.GetSpherePoint(radius, u, v);
    }
}