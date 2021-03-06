using System;

namespace NewtonMethod
{
    public class Solver
    {
        public static double NewtonSolver(Func<double, double> f, Func<double, double> df, double delta, double x0, double maxError, int maxIter)
        {
            double error = Math.Abs(f(x0));
            double x_n = x0;
            double y;
            double dy;
			if(df==null && delta!=0) {
				Func<double,double> df_2
					= (x) => 1/(2*delta)*(f(x+delta)-f(x-delta));
				df=df_2;
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
            double res=Solver.NewtonSolver(fn, null, 0.01, 2, 0.0001, 1000);
            Console.WriteLine("{0}",res);
            Console.ReadKey();
        }
    }
}
