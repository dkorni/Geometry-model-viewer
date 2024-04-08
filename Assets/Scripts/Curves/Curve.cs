using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
namespace GMV.Curves
{
    public enum CurveBuildStatus
    {
        WaitingForFirstPoint,
        WaitingForSecondPoint,
        WaitingForThirdPoint,
        WaitingForFourthPoint,
        ReadyToBuild
    }


    public class CubicBézierCurve : INotifyPropertyChanged
    {
        [JsonIgnore]
        private MyPoint _ra;
        public MyPoint RA
        {
            get => _ra;
            set
            {
                _ra = value; ;
                NotifyPropertyChanged("RA");
            }
        }

        [JsonIgnore]
        private MyPoint _rb;
        public MyPoint RB
        {
            get => _rb;
            set
            {
                _rb = value;
                NotifyPropertyChanged("RB");
            }
        }

        [JsonIgnore]
        public MyPoint _rc;
        public MyPoint RC
        {
            get => _rc;
            set
            {
                _rc = value;
                NotifyPropertyChanged("RC");
            }
        }


        [JsonIgnore]
        public MyPoint _rd;
        public MyPoint RD
        {
            get => _rd;
            set
            {
                _rd = value;
                NotifyPropertyChanged("RD");
            }
        }


        [JsonConstructor]
        public CubicBézierCurve(MyPoint ra, MyPoint rb, MyPoint rc, MyPoint rd)
        {
            RA = ra;
            if(RA !=null) RA.Curve = this;

            RB = rb;
            if (RB != null) RB.Curve = this;

            RC = rc;
            if (RC != null) RC.Curve = this;

            RD = rd;
            if (RD != null) RD.Curve = this;
        }

        public CubicBézierCurve()
        {
        }

        public CurveBuildStatus Status { get; set; } = CurveBuildStatus.WaitingForFirstPoint;

        public Vector2[] GetPoints()
        {
            double eps = 1e-6;
            double tMin = 0;
            double tMax = 1 + eps;
            double step = 0.05;
            var points = new List<Vector2>();

            for (double t = tMin; t < tMax; t += step)
            {
                points.Add(Point(t));
            }

            return points.ToArray();
        }

        public bool AddPoint(Vector2 point)
        {
            var pt = new MyPoint(this) { X = point.x, Y = point.y, Weight = 1 };
            if (RA == null)
            {
                RA = pt;
                Status = CurveBuildStatus.WaitingForSecondPoint;
                return true;
            }
            else if (RD == null)
            {
                RD = pt;
                Status = CurveBuildStatus.WaitingForThirdPoint;
                return true;
            }
            else if (RC == null)
            {
                pt.Moveable = true;
                RC = pt;
                Status = CurveBuildStatus.WaitingForFourthPoint;
                return true;
            }
            else
            {
                pt.Moveable = true;
                RB = pt;
                Status = CurveBuildStatus.ReadyToBuild;
                return false;
            }
        }

        private Vector2 Point(double t)
        {

            double wa = RA.Weight * Math.Pow(1 - t, 3);
            double wb = 3 * RB.Weight * t * Math.Pow(1 -t, 2);
            double wc = 3 * RC.Weight * Math.Pow(t, 2) * (1-t);
            double wd = RD.Weight * Math.Pow(t, 3);

            var top = RA * wa + RB * wb + RC * wc + RD * wd;
            double bottom = wa + wb + wc + wd;

            return (top / bottom).ToPoint;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
