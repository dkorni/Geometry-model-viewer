using GMV.Core;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GMV.Curves
{
    public class CurveFigureLineRenderer : LineRendererFromTextFile
    {
        protected override void LoadDrawingPointsFromFile()
        {
            var figure = JsonConvert.DeserializeObject<Figure>(File.ReadAllText(_assetPath), new Vec2Converter());
            _points = figure.GetPoints().Select(x => new Vector3(-x.y, 0, -x.x)).ToArray();
        }
    }
}