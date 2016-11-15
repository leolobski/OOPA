/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * Class for approximating the option prices using Monte Carlo method
 */

using System;
using MathNet.Numerics.Distributions;

namespace BlackScholes
{
	public class BSMonteCarlo
	{
		public static double CalculateCallOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for approximating the call price
		{
			if(S*K<=0 || T<0)
				throw new ArgumentOutOfRangeException ("S, K or T cannot be negative, S or K cannot be 0!");

			int N = 1000000; //Number of data points used by the Monte Carlo approximation
			double[] Z = new double[N];
			double difference;
			double max;
			double CumulSum = 0; //Cumulative sum for the calculation

			Normal.Samples (Z, 0, 1); //Sample N standard normal deviates

			//Perform the calculation for each sampled point and add the result to the CumulSum
			for (int i=0; i<N; i++) {

				difference = S * Math.Exp ((r - 0.5 * Math.Pow (sigma, 2)) * T + sigma * Math.Sqrt (T) * Z [i]) - K;

				if (difference >= 0)
					max = difference;
				else if (difference < 0)
					max = 0;
				else
					throw new ArgumentOutOfRangeException ("One of the arguments is out of range!");

				CumulSum += Math.Exp (-r * T) * max;
			}

			CumulSum /= N;
			return CumulSum;
		}

		public static double CalculatePutOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for approximating the put price
		{
			//Use the method to approximate the call option price
			double call = CalculateCallOptionPrice (S, T, K, sigma, r);

			//Use put-call parity to get the estimate of the put option price
			double put = call + Math.Exp (-r * T) * K - S;
			return put;
		}
	}
}

