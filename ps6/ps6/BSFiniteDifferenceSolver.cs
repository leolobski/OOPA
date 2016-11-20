/*
 * Object-Oriented Programming with Applications 2016
 * Leo Lobski
 * s1450191
 * 
 * Class for solving the Black-Scholes PDE with finite difference approximation
 */

using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;

namespace BlackScholes
{
	public class BSFiniteDifferenceSolver
	{
		// Input parameters of the model
		// T, time to maturity
		// sigma, volatility
		private Func<double, double> g; // = g(x), The payoff function
		private double r; // The risk free rate

		// Input parameters for discretisation
		private double R; // Defines the domain of g(x) as [0,R)
		private int N; // Number of time steps
		private int M; // Number of space partitions

		// Calculated parameters
		private double tau; // tau = T/N
		private double h; // h = R/(M-1)
		private double gamma; // gamma = g((M-1)*h)-g((M-2)*h)

		// Matrix needed by the iterative solver, S=(I+tau*A)
		private Matrix<double> S;

		// Parameters for the iterative solver
		private Iterator<double> monitor;
		private BiCgStab solver;

		// Constructor to initialise the model and discretisation parameters
		public BSFiniteDifferenceSolver(double T, //time to maturity
		                                Func<double, double> payoffFunction, // =g(x)
		                                double riskFreeRate,
		                                double sigma, //volatility
		                                double domain,
		                                uint numTimeSteps,
		                                uint numSpacePartitions)
		{
			// Make sure time to maturity is nonnegative
			if (T < 0) throw new ArgumentOutOfRangeException("T cannot be negative!");

			// Make sure the domain of the payoff function is a positive interval
			if (domain <= 0) throw new ArgumentOutOfRangeException("The truncated domain must be positive!");

			// Avoid division by 0 and make sure space discretisation is sensible (i.e. M != 0)
			if (numTimeSteps == 0 || numSpacePartitions == 0 || numSpacePartitions == 1)
				throw new ArgumentOutOfRangeException("Incorrect discretisation parameters!");

			// Initialise the input parameters of class parameters
			g = payoffFunction;
			r = riskFreeRate;
			R = domain;
			N = Convert.ToInt32(numTimeSteps);
			M = Convert.ToInt32(numSpacePartitions);

			// Calculate the parameters needed for the algorithm
			double rplus = Math.Max(r, 0);
			double rminus = -Math.Min(r, 0);
			tau = T / N;
			h = R / (M - 1);
			gamma = g((M - 1) * h) - g((M - 2) * h);

			// Functions for initialisation of the elements of matrix A

			Func<int, double> a
				= (m) => -0.5 * Math.Pow(sigma * m, 2) - rminus * m;

			Func<int, double> b
				= (m) => Math.Pow(sigma * m, 2) + rplus * m + rminus * m + r;

			Func<int, double> c
				= (m) => -0.5 * Math.Pow(sigma * m, 2) - rplus * m;

			// Combine the functions to give each element of A
			Func<int, int, double> matrixEntry = (i, j) =>
			{
				if ((i == 0 && j == 0) || (i == M - 1 && (j == M - 2 || j == M - 1))) return 1;
				else if (i == j) return b(i);
				else if (i > 0 && i < M - 1 && j == i - 1) return a(i);
				else if (i > 0 && i < M - 1 && j == i + 1) return c(i);
				else return 0;
			};

			// Construct the matrix A and the identity matrix I
			Matrix<double> A = Matrix<double>.Build.Sparse(M, M, matrixEntry);
			Matrix<double> I = Matrix<double>.Build.SparseIdentity(M);

			// Initialise the matrix S
			S = (I + tau * A);
		}

		// Method for calculating the vector B at time (n-1) from the vector U at (n-1)
		private Vector<double> GetB(int n, Vector<double> U)
		{
			Vector<double> B = Vector<double>.Build.Dense(M);

			// Set the elements of B as in the algorithm
			B[0] = Math.Exp(-r * tau * n) * g(0);
			for (int i = 1; i < M - 1; i++)
				B[i] = U[i];
			B[M - 1] = gamma;
			return B;
		}

		// Returns the vector U (u0) at time 0
		private Vector<double> InitialCondition()
		{
			Vector<double> u0 = Vector<double>.Build.Dense(M);

			// Discretise the initial condition (i.e. the payoff function)
			for (int m = 1; m < M - 1; m++)
				u0[m] = g(m * h);
			return u0;
		}

		// Method to set up parameters for iterative solver
		// Code for this method taken from the lecture slides (Lectures 5 and 6, solving 1D Heat Equation)
		private void SetUpSolver()
		{
			// Stop after 1000 iterations
			IterationCountStopCriterion<double> iterationCountStopCriterion
				= new IterationCountStopCriterion<double>(1000);

			// Stop after the error is less than 1e-8
			ResidualStopCriterion<double> residualStopCriterion
				= new ResidualStopCriterion<double>(1e-8);

			monitor = new Iterator<double>(iterationCountStopCriterion, residualStopCriterion);
			solver = new BiCgStab();
		}

		// Iterative solver of the PDE approximation
		// This method is written using the lecture slides as a model (Lectures 5 and 6, solving 1D Heat Equation)
		private Vector<double> FDSolve()
		{
			SetUpSolver();

			Vector<double> B = Vector<double>.Build.Dense(M);
			Vector<double> U = InitialCondition(); // Initialise U with the discretised initial condition

			for (int n = 1; n < N + 1; n++)
			{
				// Solve for U in S*U = B
				B = GetB(n, U);
				U = S.SolveIterative(B, solver, monitor);

				// Do not continue the calculation if the result did not converge
				if (monitor.Status == IterationStatus.Diverged || monitor.Status == IterationStatus.StoppedWithoutConvergence
				    || monitor.Status == IterationStatus.Failure)
					throw new OperationCanceledException("The iterative solver did not converge!");
			}
			return U;
		}

		public double Price(double S0)
		{
			// Make sure the given S0 is within the truncated domain of g(x)
			if (S0 <= 0 || S0 >= R) throw new ArgumentOutOfRangeException("The argument must lie in (0,R)");

			Vector<double> U_N = FDSolve(); // Obtain the approximate solution
			// Choose the point closest to S0 in the discretisation of R and return the value of the approximation at that point
			int m = Convert.ToInt32(Math.Round(S0 / h, MidpointRounding.AwayFromZero));
			return U_N[m];
		}
	}
}