using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using OdeComputation;

namespace OdeWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Vector2 origin = new Vector2 { X = 0, Y = 0 };
        private int step = 80;

        public MainWindow()
        {
            InitializeComponent();

            DrawGrid();
            DrawOde();
        }

        List<Vector2> GetOdeSolution(Vector2 initial)
        {
            Vector2 f(double t, Vector2 v)
            {
                double x = 2 * (v.X - 1) * (v.Y - 1);
                double y = v.Y * v.Y - v.X * v.X;

                return new Vector2 { X = x, Y = y };
            }

            List<Vector2> vectors = new List<Vector2>();
            vectors.Add(initial);

            var comp = new OdeComputer(f, vectors[0], 0, 0.1);

            for (int i = 0; i < 100; i++)
                vectors.Add(comp.NextValue());

            return vectors;
        }

        void DrawOde()
        {
            var vectors = new List<Vector2>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                var ang = 2 * Math.PI * i / n;

                var add = new Vector2
                {
                    X = Math.Cos(ang) / 4,
                    Y = Math.Sin(ang) / 4
                };

                DrawVectors(GetOdeSolution(origin + add).ToArray());
            }
        }

        private Line GetLine(double x1, double y1, double x2, double y2, Brush b)
        {
            var line = new Line();
            line.Stroke = b;
            line.StrokeThickness = 0.5;
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;

            return line;
        }

        private void DrawGrid()
        {
            double h = Cnvs.Height;
            double w = Cnvs.Width;

            for (int i = 0; i < Cnvs.Height; i += step)
            {
                Cnvs.Children.Add(GetLine(0, i, w, i, Brushes.Gray));
                Cnvs.Children.Add(GetLine(i, 0, i, h, Brushes.Gray));
            }

            Cnvs.Children.Add(GetLine(0, h / 2, w, h / 2, Brushes.Black));
            Cnvs.Children.Add(GetLine(w / 2, 0, w / 2, h, Brushes.Black));
        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            string[] text = initialText.Text.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
            double x = double.Parse(text[0]);
            double y = double.Parse(text[1]);

            origin = new Vector2 { X = x, Y = y };
            Cnvs.Children.Clear();
            DrawGrid();
            DrawOde();
        }

        bool IsNormalNumber(double x)
        {
            return !(double.IsNaN(x) || double.IsInfinity(x));
        }

        public void DrawVectors(Vector2[] vectors)
        {
            double h = Cnvs.Height;
            double w = Cnvs.Width;

            var points = new List<Point>();

            foreach (var v in vectors)
            {
                double x = v.X;
                double y = v.Y;

                var ell = new Ellipse();
                ell.Width = 6;
                ell.Height = 6;
                ell.Stroke = Brushes.Red;
                ell.Fill = Brushes.Red;

                var x1 = 2 * x * step + w / 2 - ell.Width/2;
                var y1 = h / 2 - 2 * y * step - ell.Height/2;

                if (IsNormalNumber(x1) && IsNormalNumber(y1))
                    points.Add(new Point(x1, y1));

                if (0 <= x1 && x1 < w && 0 <= y1 && y1 < h)
                {
                    ell.RenderTransform = new TranslateTransform(x1, y1);
                    Cnvs.Children.Add(ell);
                }
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                var line = GetLine(points[i].X, points[i].Y, points[i+1].X, points[i+1].Y, Brushes.Red);
                Cnvs.Children.Add(line);
            }
        }
    }
}
