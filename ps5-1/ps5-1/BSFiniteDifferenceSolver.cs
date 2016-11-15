using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;

namespace BlackScholes
{
	public class BSFiniteDifferenceSolver
	{
		//parameters of the model
		// T, time to maturity
		// sigma, volatility
		private Func<double, double> g; // = g(x), the payoff function
		private double r; // the risk free rate

		//parameters for discretisation
		// R, defines the domain of g(x) as [0,R)
		private int N; // number of time steps
		private int M; //number of space partitions

		//calculated parameters
		private double tau; // tau = T/N
		private double h; // h = R/(M-1)
		private double gamma; // gamma = g((M-1)*h)-g((M-2)*h)

		// matrix needed by the iterative solver, S=(I+tau*A)
		private Matrix<double> S;

		// for solving linear system iteratively
		private Iterator<double> monitor;
		private BiCgStab solver;

		public BSFiniteDifferenceSolver ( double T, //time to maturity
		                                 Func<double, double> payoffFunction,
		                                 double riskFreeRate,
		                                 double sigma, //volatility
		                                 double R, //domain
		                                 uint numTimeSteps,
		                                 uint numSpacePartitions)
		{
			//initialise the class parameters
			g = payoffFunction;
			r = riskFreeRate;
			N = Convert.ToInt32(numTimeSteps);
			M = Convert.ToInt32(numSpacePartitions);

			//calculate the parameters needed for the algorithm
			double rplus = Math.Max (r, 0);
			double rminus = -Math.Min (r, 0);
			tau = T / N;
			h = R / (M - 1);
			gamma = g ((M - 1) * h) - g ((M - 2) * h);

			//functions for initialisation of matrix elements
			Func<int,double> a
				= (m) => -0.5 * Math.Pow (sigma * m, 2) + rplus * m;

			Func<int,double> b
				= (m) => Math.Pow (sigma * m, 2) + rplus * m + rminus * m + r;

			Func<int,double> c
				= (m) => -0.5 * Math.Pow (sigma * m, 2) - rminus * m;

			//construct the matrix A
			Func<int, int, double> matrixEntry = (i, j) =>
			{
				if ((i==0 && j==0) || (i==M-1 && (j==M-2 || j==M-1))) return 1;
				else if (i == j) return b(i);
				else if (i > 0 && i < M-1 && j == i - 1) return a(i);
				else if (i > 0 && i < M-1 && j == i + 1) return c(i);
				else return 0;
			};

			Matrix<double> A = Matrix<double>.Build.Sparse(M, M, matrixEntry);
			Matrix<double> I = Matrix<double>.Build.SparseIdentity(M);
			//obtain the matrix S
			S = (I + tau*A);
		}

		//Method for calculating the vector B (bOld) at time (n-1) from the vector U (uOld) at (n-1)
		private Vector<double> BOld(int n, Vector<double> uOld)
		{
			Vector<double> bOld = Vector<double>.Build.Dense (M);
			bOld [0] = Math.Exp (-r * tau * n) * g (0);
			for (int i=1; i<M-1; i++)
				bOld [i] = uOld [i - 1];
			bOld [M - 1] = gamma;
			return bOld;
		}

		//Returns the vector U (u0) at time 0
		private Vector<double> ApproxInitialCondition()
		{
			Vector<double> u0 = Vector<double>.Build.Dense(M);
			for (int m = 0; m < M; m++)
				u0 [m] = g (m * h);
			return u0;
		}

		//Iterative solver of the PDE approximation
		//This method is written using the lecture slides as a model (Lectures 5 and 6, solving 1D Heat Equation)
		private Vector<double> Solve()
		{
			// Stop calculation if 1000 iterations reached during calculation
			IterationCountStopCriterion<double> iterationCountStopCriterion
				= new IterationCountStopCriterion<double>(1000);

			// Stop calculation if residuals are below 1e-8
			// i.e. the calculation is considered converged
			ResidualStopCriterion<double> residualStopCriterion
				= new ResidualStopCriterion<double>(1e-8);

			// Create monitor with defined stop criteria
			monitor = new Iterator<double>(iterationCountStopCriterion,
			                               residualStopCriterion);
			solver = new BiCgStab();
//			SetUpSolver(); //do the initialisation stuff???

			Vector<double> uOld = ApproxInitialCondition();
			Vector<double> bOld = Vector<double>.Build.Dense (M);
			Vector<double> uNew = Vector<double>.Build.Dense (M);
			for (int n = 1; n < N+1; n++)
			{
				// solve for uNew in (I + tau*A)*uNew = bOld i.e. in S*uNew = bOld
				bOld = BOld (n,uOld); 
				uNew = S.SolveIterative(bOld, solver, monitor);
				uOld = uNew;
				Console.Write("Step {0}, ", n);
			}
			Console.WriteLine();
			return uNew;
		}

		public double Price(double S)
		{
			Vector<double> U_N = Solve (); 
			int m = Convert.ToInt32(Math.Round (S / h, MidpointRounding.AwayFromZero));
			return U_N [m];
		}
	}
}