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

        private Vector3 _figureTranslation;
        private Vector3 _figureRotation;

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
            lineRendererFromText.TransferOnFigure(DrawSrtrategy, offset, angles);
            _figureTranslation = offset;
            _figureRotation = angles;
        }

        public void TransformDrawing(Vector3 offset, float rotationAngle)
        {
            lineRendererFromText.TransformOnFigure(offset, rotationAngle, _figureTranslation, _figureRotation);
        }

        public void Build(DrawSrtrategy drawSrtrategy)
        {
            figure = Figure.Build(drawSrtrategy);
            _meshFilter.mesh = figure.Mesh;
            lineRendererFromText.Build();
            lineRendererFromText.TransferOnFigure(drawSrtrategy, _figureTranslation, _figureRotation);
            DrawSrtrategy = drawSrtrategy;
        }
    }
}