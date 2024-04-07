using GMV.Core.DrawSrtrategies;
using UnityEngine;

namespace GMV.Core
{
    public class GeometryBuilder : MonoBehaviour
    {
        public DrawSrtrategy DrawSrtrategy;
        private Figure figure;

        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private LineRendererFromTextFile lineRendererFromText;

        void Start()
        {
            Build();
        }

        public void Build()
        {
            Build(DrawSrtrategy);
        }

        public void TransformFigure(Vector3 offset, Vector3 angles)
        {
            figure.MakeEuclideanTransformations(offset, angles);
        }

        public void TransformDrawing(Vector3 offset, Vector3 angles)
        {
            lineRendererFromText.TransformOnFigure(offset, angles);
        }

        public void Build(DrawSrtrategy drawSrtrategy)
        {
            figure = Figure.Build(drawSrtrategy);
            _meshFilter.mesh = figure.Mesh;
            lineRendererFromText.Build();
            lineRendererFromText.TransferOnFigure(drawSrtrategy);
        }
    }
}