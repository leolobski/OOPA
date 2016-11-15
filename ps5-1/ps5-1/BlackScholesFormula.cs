/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * Class for calculating call and put option prices using the Black-Scholes formula
 */

using System;
using MathNet.Numerics.Distributions; //Use the cumulative distribution of the standard normal

namespace BlackScholes
{
	public static class BlackScholesFormula
	//Class for pricing call and put options
	{
		public static double CalculateCallOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for calculating the call option price
		{
			//Take care that the arguments make sense
			// S/K appears in the logarithm, so S,K must be strictly positive, T is time so is nonnegative
			if(S*K<=0 || T<0)
				throw new ArgumentOutOfRangeException ("S, K or T cannot be negative, S or K cannot be 0!");

			//Evaluate constants d1 and d2 appearing in the Black-Scholes formula
			double d1 = (Math.Log (S / K) + (r + Math.Pow (sigma, 2) / 2.0) * T) / (sigma * Math.Sqrt (T));
			double d2 = d1 - sigma * Math.Sqrt (T);

			//Use the Black-Scholes formula for the call option price
			double call = S * Normal.CDF (0, 1, d1) - K * Math.Exp (-r * T) * Normal.CDF (0, 1, d2);
			return call;
		}

		public static double CalculatePutOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for calculating the put option price
		{
			//Use the method for calculating the call option price
			double call = CalculateCallOptionPrice (S, T, K, sigma, r);
			double put = call + Math.Exp (-r * T) * K - S; //Use put-call parity to get the put option price
			return put;
		}
	}
}

