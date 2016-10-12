using System;

namespace ProblemSheet3
{

	public class previousNumbers
	{
		public uint F_n;
		public uint F_n2;
	}

    public class PS3
    {

		static uint FibonacciNthTerm(uint n) //computes the n:th term of the Fibonacci sequence
		{
			Hashtable pNumbers = new Hashtable();
			previousNumbers numbers = new previousNumbers();

			numbers.F_n = 1;
			uint F_n1 = 1;
			numbers.F_n2 = 0;
			if(n==0) numbers.F_n = 0;
			else if(n==1) numbers.F_n = 1;
			else {
				for(int i=1;i<n;i++) {
					F_n1 = numbers.F_n;
					numbers.F_n = F_n1 + numbers.F_n2;
					numbers.F_n2 = F_n1;
				}
			}
			return numbers.F_n;
		}

        public static void Main(string[] args)
        {
            uint n1=FibonacciNthTerm(4);
			uint n2=FibonacciNthTerm(5);
			uint n3=FibonacciNthTerm(6);

			Console.WriteLine("{0},{1},{2}",n1,n2,n3);
        }
    }
}
