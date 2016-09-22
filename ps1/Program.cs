using System;

namespace ProblemSheet1
{
	public class PS1
	{

		static uint FibonacciNthTerm(uint n) //computes the n:th term of the Fibonacci sequence
		{
			uint F_n = 1;
			uint F_n1 = 1;
			uint F_n2 = 0;
			if(n==0) F_n = 0;
			else if(n==1) F_n = 1;
			else {
				for(int i=1;i<n;i++) {
					F_n1 = F_n;
					F_n = F_n1 + F_n2;
					F_n2 = F_n1;
				}
			}
			return F_n;
		}

		static void OrderNumbers(double[] numbers)
		{
			bool swapped;
			double tmp;
			do {
				swapped = false;
				for(int i=0;i<numbers.Length-1;i++) {
					if(numbers[i] < numbers[i+1]) {
						tmp = numbers[i];
						numbers[i] = numbers[i+1];
						numbers[i+1] = tmp;
						swapped = true;
					}
				}
			} while(swapped);
		}
					

		public static void Main(string[] args)
		{
			Console.WriteLine("Exercise 1.1\n");

			double x = 10864;
			double y = 18817;
			
			double w_1 = Math.Pow(x,4);
			w_1 *= 9;
			w_1 -= Math.Pow(y,4);
			w_1 += 2*Math.Pow(y,2);
			double w_2 = (3*Math.Pow(x,2) - Math.Pow(y,2))*(3*Math.Pow(x,2) + Math.Pow(y,2)) + 2*Math.Pow(y,2);
			double w_3 = (9*Math.Pow(x,4) + 2*Math.Pow(y,2)) - Math.Pow(y,4);

			Console.WriteLine("w_1: {0}\nw_2: {1}\nw_3: {2}", w_1, w_2, w_3);

			Console.WriteLine("\nExercise 1.2\n");
			int i;
			//for(i=0;i<100;i++) Console.WriteLine("C# is easy.");

			Console.WriteLine("\nExercise 1.3\n");
			double S = Math.Pow(10,8);
			int repetitions = (int)Math.Pow(10,7);
			for(i=0;i<repetitions;i++) S += Math.Pow(10,-10);
			Console.WriteLine("S = {0}", S);

			Console.WriteLine("\nExercise 1.4\n");
			uint n = 12;
			uint Nth = FibonacciNthTerm(n);
			Console.WriteLine("{0}:th term of the Fibonacci sequence is {1}", n, Nth);

			Console.WriteLine("\nExercise 1.5\n");
			double[] number_list = {-78.9,-3.7,28.14,15.5,-112.3,7.2,-1.6,11.4};
			OrderNumbers(number_list);
			for(i=0;i<number_list.Length;i++) Console.WriteLine(number_list[i]);

		}
	}
}
