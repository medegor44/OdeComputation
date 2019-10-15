using System;

namespace OdeComputation
{
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2() { X = a.X + b.X, Y = a.Y + b.Y };
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2 { X = a.X - b.X, Y = a.Y - b.Y };
        }

        public static Vector2 operator *(Vector2 a, double k)
        {
            return new Vector2 { X = a.X * k, Y = a.Y * k };
        }

        public static Vector2 operator /(Vector2 a, double k)
        {
            if (Math.Abs(k) < double.Epsilon)
                throw new ArgumentException();

            return a * (1 / k);
        }

        public override string ToString()
        {
            return string.Format("({0:0.000}, {1:0.000})", X, Y);
        }
    }
}
