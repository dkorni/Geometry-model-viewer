using System;
using UnityEngine;

namespace GMV.Core.DrawSrtrategies
{
    public abstract class DrawSrtrategy : ScriptableObject
    {
        public Vector3 Center = Vector3.zero;
        protected Vector3[] vertices;
        protected int[] triangles;
        protected Vector2[] uvCordinates;

        public abstract Mesh Draw();

        protected Mesh BuildMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvCordinates;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}