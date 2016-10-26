using System;
using MathNet.Numerics.Distributions;

namespace BlackScholes
{
	public class BSMonteCarlo
	{
		public static double CalculateCallOptionPrice(double S, double T, double K, double sigma, double r)
		{
			int N = 1000000;
			double[] Z = new double[N];
			double difference;
			double max;
			double CumulSum = 0;
			Normal.Samples (Z, 0, 1);

			for (int i=0; i<N; i++) {

				difference = S * Math.Exp ((r - 0.5 * Math.Pow (sigma, 2)) * T + sigma * Math.Sqrt (T) * Z [i]) - K;

				if (difference >= 0)
					max = difference;
				else if (difference < 0)
					max = 0;
				else
					throw new ArgumentOutOfRangeException ("One of the arguments is not a number!");

				CumulSum += Math.Exp (-r * T) * max;
			}

			CumulSum /= N;
			return CumulSum;
		}

		public static double CalculatePutOptionPrice(double S, double T, double K, double sigma, double r)
		//Method for calculating the put price
		{
			double call = CalculateCallOptionPrice (S, T, K, sigma, r); //Use Monte Carlo to approximate the call option price
			double put = call + Math.Exp (-r * T) * K - S; //Use put-call parity to get the estimate of the put option price
			return put;
		}
	}
}

