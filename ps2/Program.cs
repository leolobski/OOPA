using System;

namespace ProblemSheet2
{
	public class PS2
	{

		static void PrintArray<T> (T[] array) //Exercise 2.1
		{
			for(int i=0;i<array.Length;i++) Console.WriteLine("{0}", array[i]);
		}

		static void SortingMethod<T> (T[] numbers) where T : System.IComparable<T> //Exercise 2.2
		{
			bool swapped;
			T tmp;
			do {
				swapped = false;
				for(int i=0;i<numbers.Length-1;i++) {
					if(numbers[i].CompareTo(numbers[i+1]) < 0) {
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
			int[] array = {23,5,56,3,1,4,345,2,5};
			float[] flarray = {2.4f,4.7f,1.6f,45.3f,2.2f,4.0f,111.1f,34.5f};
			SortingMethod<int> (array);
			SortingMethod<float> (flarray);
			PrintArray<int> (array);
			PrintArray<float> (flarray);
		}
	}
}
