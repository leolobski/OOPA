/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
*/

using System;
using MathNet.Numerics.Distributions; //Use the cumulative distribution of the standard normal

namespace BlackScholes
{
	public static class BlackScholesFormula
	//Class for pricing call and put options
	{
		public static double CalculateCallOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for calculating the call price
		{
			double d1 = (Math.Log (S / K) + (r + Math.Pow (sigma, 2) / 2.0) * T) / (sigma * Math.Sqrt (T)); //Constant in Black-Scholes formula
			double d2 = d1 - sigma * Math.Sqrt (T); //Another constant in the formula
			double call = S * Normal.CDF (0, 1, d1) - K * Math.Exp (-r * T) * Normal.CDF (0, 1, d2); //The Black-Scholes formula for the call option price
			return call;
		}

		public static double CalculatePutOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for calculating the put price
		{
			double call = CalculateCallOptionPrice (S, T, K, sigma, r); //Use the Black-Scholes formula to get the call option price
			double put = call + Math.Exp (-r * T) * K - S; //Use put-call parity to get the put option price
			return put;
		}
	}
}