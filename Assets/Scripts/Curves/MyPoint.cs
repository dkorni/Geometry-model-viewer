using Newtonsoft.Json;
using UnityEngine;

namespace GMV.Curves
{
    public class MyPoint
    {
        public double x;
        public double y;
        public double weight = 1;

        public bool Moveable = false;

        public double X
        {
            get =>x; 
            set
            {
                x = value;
            }
        }


        public double Y
        {
            get => y; 
            set
            {
                y = value;
            }
        }
        public double Weight
        {
            get => weight;
            set
            {
                weight= value;
            }
        }

        public MyPoint(Vector2 point)
        {
            X = point.x;
            Y = point.y;
        }

        [JsonConstructor]
        public MyPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public MyPoint(CubicBézierCurve curve)
        {
            Curve = curve;
        }


        [JsonIgnore]
        public CubicBézierCurve Curve { get; set; }

        public Vector2 ToPoint
        {
            get
            {
                return new Vector2((float)X, (float)Y);
            }
        }

        public static MyPoint operator *(MyPoint lhs, double rhs)
        {
            return new MyPoint(lhs.Curve) { X = lhs.X * rhs, Y = lhs.Y * rhs, Weight = lhs.Weight * rhs };
        }

        public static MyPoint operator /(MyPoint lhs, double rhs)
        {
            return new MyPoint(lhs.Curve) { X = lhs.X / rhs, Y = lhs.Y / rhs, Weight = lhs.Weight / rhs };
        }

        public static MyPoint operator +(MyPoint lhs, MyPoint rhs)
        {
            return new MyPoint(lhs.Curve) { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Weight = lhs.Weight + rhs.Weight };
        }

        public override string ToString()
        {
            return $"{X};{Y}";
        }
    }
}
