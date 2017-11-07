// Finding all combinations of a list of numbers using Heap's Algorithm
// Wikipedia and CodeGurus

using System;

namespace CombinationFinder
{
    class Program
    {
		static int[] originalArray = { 4, 8, 9 };

        public static void PrintArray(int[] myArray)
        {
            for (int n = 0; n < myArray.Length; n++)
                Console.Write(myArray[n]);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] numbers = new int[originalArray.Length];
            originalArray.CopyTo(numbers, 0);
            FindCombinations(numbers, numbers.Length);
        }

        public static void Swap( ref int x, ref int y )
        {
            int tmp = x;
            x = y;
            y = tmp;
        }

        public static void FindCombinations(int[] myArray, int size)
        {
            if (size ==  1)
            {
                PrintArray(myArray);
            }
            else
            {
                for (int i = 0; i < size - 1; i++)
                {
					FindCombinations( myArray, size - 1 );

					if ( size % 2 == 0 )
                    {
                        //int tmp = myArray[i];
                        //myArray[i] = myArray[size - 1];
                        //myArray[size - 1] = tmp;
                        Swap(ref myArray[i], ref myArray[size - 1]);
                    }
                    else
                    {
                        //int tmp = myArray[0];
                        //myArray[0] = myArray[size - 1];
                        //myArray[size - 1] = tmp;
                        Swap(ref myArray[0], ref myArray[size - 1]);
                    }
                }

				FindCombinations(myArray, size - 1);
			}
        }
    }
}
