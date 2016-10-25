using System;

namespace BlackScholes
{
	public class MainClass
	{
		public static void Main()
		{
			double S = 100;
			double K = 100;
			double T = 1;
//			double sigma = 0.1;
			double r = 0.05;
			double CallPrice = 10;
			double PutPrice = 3;

//			double call = BlackScholesFormula.CalculateCallOptionPrice (S, T, K, sigma, r);
//			double put = BlackScholesFormula.CalculatePutOptionPrice (S, T, K, sigma, r);

			double Volatility1 = BSImpliedVolatility.CalculateImplVolatility (S, T, K, r, CallPrice, true, 0.5);
			double Volatility2 = BSImpliedVolatility.CalculateImplVolatility (S, T, K, r, PutPrice, false, 0.5);

//			Console.WriteLine ("Call: {0}\nPut: {1}\n", call, put);
			Console.Write ("{0}\n{1}\n", Volatility1, Volatility2);
			Console.ReadKey ();

		}
	}
}