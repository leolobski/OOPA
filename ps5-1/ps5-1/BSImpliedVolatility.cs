using System;

namespace BlackScholes
{
	public class BSImpliedVolatility
	{
		public static double CalculateImplVolatility (double S, double T, double K, double r, double OptionPrice, bool CallOption, double x0)
		{
			Func<double,double> CalculateOptionPrice;
			if (CallOption == true) {
				CalculateOptionPrice
					= (sigma) => BlackScholesFormula.CalculateCallOptionPrice (S, T, K, sigma, r) - OptionPrice;
			}
			else if (CallOption == false) { //If option price is that of a put option
				CalculateOptionPrice
					= (sigma) => BlackScholesFormula.CalculatePutOptionPrice (S, T, K, sigma, r) - OptionPrice;
			}
			else
				throw new ArgumentOutOfRangeException ("CallOption is TRUE if price is a call option price, and FALSE if put option price");

			double maxError = 1e-5;
			int maxIter = 10000;
			double ImplVolatility = NewtonSolver.Solver (CalculateOptionPrice, null, x0, maxError, maxIter);
			return ImplVolatility;
		}
	}
}

