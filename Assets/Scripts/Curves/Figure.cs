using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace GMV.Curves
{
    public class Figure
    {
        public ObservableCollection<CubicBézierCurve> Curves { get; set; } = new ObservableCollection<CubicBézierCurve>();

        public List<Vector2> GetPoints()
        {
            var result = new List<Vector2>();
            foreach (var curve in Curves)
            {
                if (curve.RA == null || curve.RB == null || curve.RC == null) continue;

                var r = curve.GetPoints();
                result.AddRange(r);
            }
            return result;
        }
    }
}
