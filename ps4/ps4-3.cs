using System;

using MathNet.Numerics.LinearAlgebra;

namespace NewtonMethod
{
    public class Solver
    {
        private const double delta = 1e-8; //for partial derivative approximation
        private double tol;
        private int maxIt;

        public Solver(double tolerance, int maximumIterations)
        {
            tol = tolerance;
            maxIt = maximumIterations;
        }

        public Matrix<double> ApproxJacobian(Func<Vector<double>,Vector<double>> F, Vector<double> x)
        {
            Matrix<double> J;
//          Vector<double> f = (1/2.0*delta)*(F(x+delta)-F(x-delta));
            for(int i=0;i<x.Count;i++) {
                for(int j=0;j<x.Count;j++) {
                    J[i,j] = (1/2.0*delta)*(F[i](x[j]+delta)-F[i](x[j]-delta));
		}

        public static double NewtonSolver2(Func<double, double> f, Func<double, double> df, double x0, double maxError, int maxIter)
        {
            double error = Math.Abs(f(x0));
            double x_n = x0;
            double y;
            double dy;
            if (df == null)
            {
                Func<double, double> df_2
                    = (x) => 1 / (2 * delta) * (f(x + delta) - f(x - delta));
                df = df_2;
            }

            for (int i = 0; i < maxIter && error >= maxError; i++)
            {
                y = f(x_n);
                dy = df(x_n);
                x_n = x_n - y / dy;
                error = Math.Abs(f(x_n));
            }
            return x_n;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Func<double, double> fn
                = (x) => Math.Pow(x, 2) - 2;
            //			Func<double, double> df
            //				= (x) => 2*x;
            double res = Solver.NewtonSolver2(fn, null, 2, 0.0001, 1000);
            Console.WriteLine("{0}", res);
            Console.ReadKey();
        }
    }
}
