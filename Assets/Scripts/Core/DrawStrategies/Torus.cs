using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    [CreateAssetMenu(fileName = "Torus", menuName = "DrawSrtrategies/Torus")]
    public class Torus : DrawSrtrategy
    {
        public float Radius;
        public int Segments = 25;

        public int Sides = 12;
        public float MajorRadius = 1f;
        public float MinorRadius = 0.2f;

        public override Mesh Draw()
        {
            vertices = new Vector3[Segments * Sides];
            triangles = new int[Segments * Sides * 6];

            int index = 0;

            for (int i = 0; i < Segments; i++)
            {
                float theta = (2 * Mathf.PI * i) / Segments;

                for (int j = 0; j < Sides; j++)
                {
                    float phi = (2 * Mathf.PI * j) / Sides;

                    float x = GeometryMath.GetTorusX(MajorRadius, MinorRadius, theta, phi);
                    float y = GeometryMath.GetTorusY(MinorRadius, phi);
                    float z = GeometryMath.GetTorusZ(MajorRadius, MinorRadius, theta, phi);

                    vertices[index] = new Vector3(x, y, z);
                    index++;
                }
            }

            index = 0;

            for (int i = 0; i < Segments; i++)
            {
                for (int j = 0; j < Sides; j++)
                {
                    int a = i * Sides + j;
                    int b = (i * Sides + (j + 1) % Sides);
                    int c = ((i + 1) % Segments) * Sides + j;
                    int d = ((i + 1) % Segments) * Sides + (j + 1) % Sides;

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