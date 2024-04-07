using GMV.Core.DrawSrtrategies;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GMV.Core
{
    public class LineRendererFromTextFile : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private string lineAssetName;

        private float _width;
        private float _height;

        private int _pointCount;

        private Vector3[] _points; // original points

        private Vector3[] _pointsOnFigure;

        private Vector2[] _uvOriginalPoints;
        private Vector2[] _uvPoints;

        private Vector2 _uvCenter;

        private DrawSrtrategy _currentDrawStrategy;

        public void Start()
        {
            // Build();
        }

        public void TransferOnFigure<T>(T figure, Vector3 figureTranslation, Vector3 figureRotation) where T : DrawSrtrategy
        {
            _pointsOnFigure = new Vector3[_pointCount + 1];

            switch (figure)
            {
                case Sphere:

                    var sphere = figure as Sphere;

                    for (int i = 0; i <= _pointCount; i++)
                    {
                        var point = sphere.GetPoint(sphere.Radius, _uvPoints[i].x, _uvPoints[i].y);
                       
                        // переносимо точку малюнка з урахуванням повороту і зсуву фігури
                        var transformMatrix = Matrix4x4.Translate(figureTranslation) * Matrix4x4.Rotate(Quaternion.Euler(figureRotation.x, figureRotation.y, figureRotation.z));
                        point = transformMatrix.MultiplyPoint3x4(point);
                        _pointsOnFigure[i] = point;
                    }

                    break;
            }

            lineRenderer.SetPositions(_pointsOnFigure);
            _currentDrawStrategy = figure;
        }

        public void TransformOnFigure(Vector2 translation, float rotationAngle, Vector3 figureTranslation, Vector3 figureRotation)
        {
            for (int i = 0; i < _uvPoints.Length; i++)
            {
                var initialVertice = _uvOriginalPoints[i];

                // https://docs.unity3d.com/ScriptReference/Matrix4x4.Translate.html
                // https://docs.unity3d.com/ScriptReference/Matrix4x4.Rotate.html

                Matrix4x4 translateToOrigin = Matrix4x4.Translate(_uvCenter);
                Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.Euler(new Vector3(0, 0, rotationAngle)));
                Matrix4x4 translateBack = Matrix4x4.Translate(-_uvCenter);

                Matrix4x4 m = translateToOrigin*Matrix4x4.Translate(translation) * rotation * translateBack;

                Vector3 rotatedVector = m.MultiplyPoint(initialVertice);

                _uvPoints[i] = rotatedVector;
            }

            TransferOnFigure(_currentDrawStrategy, figureTranslation, figureRotation);
        }

        public void Build()
        {
            LoadDrawingPointsFromFile();
            NormilizeIntoUV();
            CalculateUVCenter();
        }

        private void LoadDrawingPointsFromFile()
        {
            List<Vector3> points = new List<Vector3>();

            var path = Path.Combine(Application.streamingAssetsPath, lineAssetName);

            using (var reader = new StreamReader(path))
            {
                var sizes = reader.ReadLine().Split(" ");
                _width = float.Parse(sizes[0]);
                _height = float.Parse(sizes[1]);

                _pointCount = int.Parse(reader.ReadLine());

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(" ");

                    var x = float.Parse(line[0]);
                    var y = float.Parse(line[1]);

                    points.Add(new Vector3(x, 0, y));
                }

                _points = points.ToArray();

                lineRenderer.positionCount = _pointCount + 1;
                lineRenderer.SetPositions(_points);
            }
        }

        private void NormilizeIntoUV()
        {
            var uvPoints = new List<Vector2>();
            foreach (var point in _points)
            {
                var u = (point.x * Mathf.PI / 6) / _width;
                var v = (point.z * Mathf.PI / 6) / _height;

                uvPoints.Add(new Vector2(u, v));
            }

            _uvPoints = uvPoints.ToArray();
            _uvOriginalPoints = uvPoints.ToArray();
        }

        private void CalculateUVCenter()
        {
            _uvCenter = Vector2.zero;
            foreach (Vector2 uvPoint in _uvOriginalPoints)
            {
                _uvCenter += uvPoint;
            }

            _uvCenter /= _uvOriginalPoints.Length;
        }
    }
}