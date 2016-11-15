/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * Newton method for numerical equation solving
 */

using System;

namespace BlackScholes
{
	public class NewtonSolver
	{
		public static double Solver(Func<double, double> f, Func<double, double> df, double x0,
		                            double maxError, int maxIter)
		//The solver approximates the solutions of f(x)=0. Input:
		//	f - f(x)
		//	df - df(x)/dx (=null if the derivative to be approximated)
		//	x0 - initial guess (start of the iterations)
		//	maxError - maximal deviation from the true value
		//	maxIter - maximal number of iterations
		{
			if(maxError<=0 || maxIter<=0)
				throw new ArgumentOutOfRangeException ("maxError and maxIter must be positive!");

			double error = Math.Abs(f(x0));
			double x_n = x0;
			double y;
			double dy;
			double delta = 1e-8; //For finite difference approximation of derivative

			//If no expression for the derivative is given, approximate it by the finite differences
			if(df==null) {
				Func<double,double> df_approx
					= (x) => 1/(2*delta)*(f(x+delta)-f(x-delta));
				df=df_approx;
			}

			//Iterate for the maximal number of iterations allowed or until the desired error is reached
			for (int i = 0; i < maxIter && error >= maxError; i++)
			{
				y = f(x_n);
				dy = df(x_n);
				if (dy == 0)
					dy = delta; //Take care that dy is not zero
				x_n = x_n - y / dy;
				error = Math.Abs(f(x_n));
			}
			return x_n;
		}
	}
}