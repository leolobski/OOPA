/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * The program tests the classes with the provided data as input, printing out the exact option prices
 * alongside with the Monte Carlo estimates for comparison
 */

using System;

namespace BlackScholes
{
	public class MainClass
	{
		public static void Main()
		{
			//Use the given parameter values to test the implemented classes
			double S = 100;
			double K = 100;
			double[] T = {1,1,1,10,1};
			double[] sigma = {0.1,0.1,0.2,0.1,0.1};
			double[] r = {0.05,0,0.05,0.05,0.05};
			Func<double,double> putPayoff = (S0) => Math.Max(K-S0,0);
			Func<double,double> callPayoff = (S0) => Math.Max(S0-K,0);

			uint N=200;
			uint M=400;

			//For calculating the implied volatility
//			double CallPrice = 10;
//			double PutPrice = 3;

			double call; //Exact call option price using the Black-Scholes formula
			double put; //Exact put option price
//			double MCcall; //Monte Carlo estimate of the call option
//			double MCput; //MC estimate of the put option
			double FDcall, FDput;

			//Calculate the option prices both using the formula and the estimate for each set of parameters
			for(int i=0;i<5;i++) {

//				call = BlackScholesFormula.CalculateCallOptionPrice (S, T[i], K, sigma[i], r[i]);
				put = BlackScholesFormula.CalculatePutOptionPrice (S, T[i], K, sigma[i], r[i]);

//				BSFiniteDifferenceSolver CallSolver = new BSFiniteDifferenceSolver(T[i],callPayoff,r[i],sigma[i],5*K,N,M);
				BSFiniteDifferenceSolver PutSolver = new BSFiniteDifferenceSolver(T[i],putPayoff,r[i],sigma[i],5*K,N,M);

//				FDcall = CallSolver.Price(S);
				FDput = PutSolver.Price(S);

//				MCcall = BSMonteCarlo.CalculateCallOptionPrice (S, T[i], K[i], sigma[i], r[i]);
//				MCput = BSMonteCarlo.CalculatePutOptionPrice (S, T[i], K[i], sigma[i], r[i]);

				Console.Write ("Put: {0} FD Estimate: {1}\n", put, FDput);

			}

			//Now compute the implied volatilities for given call and put prices
//			double Volatility1 = BSImpliedVolatility.CalculateImplVolatility (S, T[0], K[0], r[0], CallPrice, true, 0.5);
//			double Volatility2 = BSImpliedVolatility.CalculateImplVolatility (S, T[0], K[0], r[0], PutPrice, false, 0.5);

//			Console.Write ("Implied volatility: {0}\nImplied volatility: {1}\n", Volatility1, Volatility2);

			Console.ReadKey ();

		}
	}
}