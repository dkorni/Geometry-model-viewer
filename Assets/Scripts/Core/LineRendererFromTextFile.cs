using GMV.Core;
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

        private DrawSrtrategy _currentDrawStrategy;

        public void Start()
        {
            // Build();
        }

        public void TransferOnFigure<T>(T figure) where T : DrawSrtrategy
        {
            _pointsOnFigure = new Vector3[_pointCount+1];

            switch (figure)
            {
                case Sphere:

                    var sphere = figure as Sphere;

                    for (int i = 0; i <= _pointCount; i++)
                        _pointsOnFigure[i] = GeometryMath.GetSpherePoint(sphere.Radius, _uvPoints[i].x, _uvPoints[i].y);

                    break;
            }

            lineRenderer.SetPositions(_pointsOnFigure);
            _currentDrawStrategy = figure;
        }

        public void TransformOnFigure(Vector2 translation, Vector3 angles)
        {
            for (int i = 0; i < _uvPoints.Length; i++)
            {
                var initialVertice = _uvOriginalPoints[i];

                // https://docs.unity3d.com/ScriptReference/Matrix4x4.Translate.html
                // https://docs.unity3d.com/ScriptReference/Matrix4x4.Rotate.html
                Matrix4x4 m = Matrix4x4.Translate(translation);

                Vector3 rotatedVector = m.MultiplyPoint(initialVertice);

                _uvPoints[i] = rotatedVector;
            }

            TransferOnFigure(_currentDrawStrategy);
        }

        public void Build()
        {
            LoadDrawingPointsFromFile();
            NormilizeIntoUV();
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
    }
}