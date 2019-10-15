using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OdeComputation;

namespace OdeConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //Vector2 f(double t, Vector2 v)
            //{
            //    double x = 2 * (v.X - 1) * (v.Y - 1);
            //    double y = v.Y * v.Y - v.X * v.X;

            //    return new Vector2 { X = x, Y = y };
            //}

            Vector2 f(double t, Vector2 v)
            {
                double x = v.Y;
                double y = -Math.Sin(v.X);

                return new Vector2 { X = x, Y = y };
            }


            var comp = new OdeComputer(f, new Vector2 { X = 10, Y = -2 }, 0, 0.1);

            for (int i = 0; i < 100; i++)
            {
                var v = comp.NextValue();
                Console.WriteLine(v.ToString());
            }
        }
    }
}
