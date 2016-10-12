using System;

namespace ProblemSheet4
{
    public class CompositeIntegrator
    {
        private int newtonCotesOrder;
        const int maxOrder = 4;
        const int maxOrderLength = 5;

        private static double[,] weights = new double[maxOrder, maxOrderLength]
        {
            {0.5, 0.5, 0, 0, 0}, // Trapezium rule
            {1.0/6.0, 4.0/6.0, 1.0/6.0, 0, 0}, // Simpson's rule
            {1.0/8.0, 3.0/8.0, 3.0/8.0, 1.0/8.0,0},
            {7.0/90.0, 32.0/90.0, 12.0/90.0, 32.0/90.0, 7.0/90.0}
        };

        private double[] quadraturePoints;
        private double[] quadraturePointsFVal;

        public CompositeIntegrator()
        {
            newtonCotesOrder = 1;
            quadraturePoints = new double[newtonCotesOrder + 1];
            quadraturePointsFVal = new double[newtonCotesOrder + 1];
        }
        public CompositeIntegrator(CompositeIntegrator integrator)
        {
            newtonCotesOrder = integrator.newtonCotesOrder;
            quadraturePoints = new double[newtonCotesOrder + 1];
            quadraturePointsFVal = new double[newtonCotesOrder + 1];
        }
        public CompositeIntegrator(int newtonCotesOrder)
        {
            if (newtonCotesOrder == 1 || newtonCotesOrder == 2 || newtonCotesOrder == 3 || newtonCotesOrder == 4)
            {
                this.newtonCotesOrder = newtonCotesOrder;
                quadraturePoints = new double[newtonCotesOrder + 1];
                quadraturePointsFVal = new double[newtonCotesOrder + 1];
            }
            else throw new ArgumentOutOfRangeException("Wrong order! Only n = 1..4 are allowed!");
        }

        private void UpdateQuadraturePointsAndFvals(double a, double h, Func<double, double> f)
        {
            double delta = h / newtonCotesOrder;
            for (int i = 0; i <= newtonCotesOrder; i++)
            {
                quadraturePoints[i] = a + i * delta;
                quadraturePointsFVal[i] = f(quadraturePoints[i]);
            }
        }

        public double Integrate(Func<double, double> f, double a, double b, int N)
        {
            if (N <= 0) throw new ArgumentOutOfRangeException("N must be a positive integer!");

            double integral = 0;
            double h = (b - a) / N;
            for (int i = 0; i < N; i++)
            {
                UpdateQuadraturePointsAndFvals(a + i * h, h, f);
                double stepIncrement = 0.0;
                for (int j = 0; j <= newtonCotesOrder; j++)
                {
                    stepIncrement += weights[newtonCotesOrder - 1, j]
                    * quadraturePointsFVal[j];
                }
                integral += stepIncrement * h;
            }
            return integral;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Func<double, double> normDens
                = (x) => Math.Exp(-x * x / 2.0) / Math.Sqrt(2 * Math.PI);
            CompositeIntegrator integrator = new CompositeIntegrator(4);
            double integral = integrator.Integrate(normDens, 10, 0, 100);
            Console.WriteLine("{0}", integral);
            Console.ReadKey();
        }
    }
}
