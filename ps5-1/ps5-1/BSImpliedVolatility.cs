/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * Calculates the implied volatility given call/put option price
 * Numerically solves for volatility in the Black-Scholes formula
 */

using System;

namespace BlackScholes
{
	public class BSImpliedVolatility
	{
		public static double CalculateImplVolatility (double S, double T, double K, double r,
		                                              double OptionPrice, bool CallOption, double x0)
		//CallOption is set to 'true' if the given option price is a call option, and to 'false' if it is a put option
		{
			Func<double,double> CalculateOptionPrice; //Assign the function to a method for call/put price calculation

			if (CallOption == true) { //If the option price is that of a call option
				CalculateOptionPrice
					= (sigma) => BlackScholesFormula.CalculateCallOptionPrice (S, T, K, sigma, r) - OptionPrice;
			}
			else if (CallOption == false) { //If the option price is that of a put option
				CalculateOptionPrice
					= (sigma) => BlackScholesFormula.CalculatePutOptionPrice (S, T, K, sigma, r) - OptionPrice;
			}
			else
				throw new ArgumentOutOfRangeException ("CallOption is TRUE if price is a call option price," +
					"and FALSE if put option price");

			//Find the roots of the function by passing it to the NewtonSolver
			double maxError = 1e-5;
			int maxIter = 10000;
			double ImplVolatility = NewtonSolver.Solver (CalculateOptionPrice, null, x0, maxError, maxIter);
			return ImplVolatility;
		}
	}
}

