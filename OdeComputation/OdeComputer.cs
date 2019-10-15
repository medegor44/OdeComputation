using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeComputation
{
    public class OdeComputer
    {
        private Func<double, Vector2, Vector2> f;
        private Vector2 x;
        private double t;
        private double h;


        public OdeComputer(Func<double, Vector2, Vector2> func, Vector2 initial, double time, double step)
        {
            f = func;
            x = initial;
            t = time;
            h = step;
        }

        public Vector2 NextValue()
        {
            var q1 = f(t, x)*h;
            var q2 = f(t + h / 2, x + q1 / 2) * h;
            var q3 = f(t + h, x - q1 + q2*2) * h;
            t += h;

            return x += (q1 + q2 * 4 + q3) / 6.0;
        }
    }
}
