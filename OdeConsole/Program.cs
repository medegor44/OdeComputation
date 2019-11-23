using System;
using System.Globalization;
using System.Threading;
using BoundaryOdeComputation;
using static System.Math;

namespace OdeConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var comp = new BoundaryOdeComputer((x) => -5.0, (x) => 4.0, (x) => -2*Exp(2*x), 10);

            var vals = comp.CalcBoundaryOde();

            foreach (var v in vals)
                Console.WriteLine(v);
        }
    }
}
