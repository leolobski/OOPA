using System;

namespace BlackScholes
{
	public class NewtonSolver
	{
		public static double Solver(Func<double, double> f, Func<double, double> df, double x0, double maxError, int maxIter)
		{
			double error = Math.Abs(f(x0));
			double x_n = x0;
			double y;
			double dy;
			double delta = 1e-8;

			if(df==null) {
				Func<double,double> df_approx
					= (x) => 1/(2*delta)*(f(x+delta)-f(x-delta));
				df=df_approx;
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
}