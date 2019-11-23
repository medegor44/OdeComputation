using System;
using System.Collections.Generic;
using OdeComputation;

namespace BoundaryOdeComputation
{
    using RealFunc = Func<double, double>;

    public class BoundaryOdeComputer
    {
        private double[] a;
        private double[] b;
        private double[] g;
        private int n;
        public double h { get; }

        public BoundaryOdeComputer(RealFunc p, RealFunc q, RealFunc f, int n)
        {
            this.n = n;

            a = new double[n + 1];
            b = new double[n + 1];
            g = new double[n + 1];

            h = 1.0 / n;

            CalcCoeffs(p, q, f, n);
        }

        private void CalcCoeffs(RealFunc p, RealFunc q, RealFunc f, int n)
        {
            double x = h;

            for (int i = 1; i < n; i++)
            {
                a[i] = h * h * q(x) - h * p(x) - 2;
                b[i] = 1 + p(x)*h;
                g[i] = h * h * f(x);

                x += h;
            }
        }

        public Vector2[] CalcBoundaryOde()
        {
            var u = new double[n + 1];
            var alpha = new double[n + 1];
            var beta = new double[n + 1];

            alpha[0] = -1 / (h - 1);
            beta[0] = 0;

            for (int i = 1; i < n + 1; i++)
            {
                alpha[i] = -b[i] / (alpha[i - 1] + a[i]);
                beta[i] = (g[i] - beta[i - 1]) / (alpha[i - 1] + a[i]);
            }

            u[n] = 1;

            for (int i = n - 1; i >= 0; i--)
            {
                u[i] = alpha[i] * u[i + 1] + beta[i];
            }

            var x = 0.0;

            var ux = new List<Vector2>();
            for (int i = 0; i <= n; i++)
            {
                ux.Add(new Vector2 { X = x, Y = u[i] });
                x += h;
            }

            return ux.ToArray();
        }
    }
}
