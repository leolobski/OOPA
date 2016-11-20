/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * The program tests the finite difference solver class with the provided input, printing out the difference
 * between the finite difference estimate and the exact value
 */

using System;

namespace BlackScholes
{
	public class MainClass
	{
		public static void Main()
		{
			// Use the given parameter values to test the implemented class
			double r = 0.05;
			double sigma = 0.1;
			double K = 100;
			double T = 1;
			double S0 = 100;

			double error;
			Func<double, double> putPayoff = (S) => Math.Max(K - S, 0); // = g(x), the payoff function

			uint N = 200; // Number of timesteps
			uint M = 100; // Number of space partitions
			int numberCalculations = 1;

			// Get the exact value using the Black-Scholes formula
			double putPrice = BlackScholesFormula.CalculatePutOptionPrice(S0, T, K, sigma, r);

			// Use finite difference estimation to approximate the option price and compare it to the exact value
/*
			for (int i = 0; i < numberCalculations; i++, M*=2)
			{
				BSFiniteDifferenceSolver solver = 
					new BSFiniteDifferenceSolver (T, putPayoff, r, sigma, 5*K, N, M);

				error = Math.Abs (putPrice - solver.Price (S0));
				Console.WriteLine ("Space partitions: {0}, time steps: {1}, error: {2}", M, N, error);
			}
			Console.WriteLine();
*/
			N = 10;
			M = 1600;
			for (int i = 0; i < numberCalculations; i++, N*=2)
			{
				BSFiniteDifferenceSolver solver =
					new BSFiniteDifferenceSolver(T, putPayoff, r, sigma, 5*K, N, M);

				error = Math.Abs(putPrice - solver.Price(S0));
				Console.WriteLine("Space partitions: {0}, time steps: {1}, error: {2}", M, N, error);
			}

			Console.ReadKey();

		}

	}
}