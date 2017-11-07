using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTest
{
	/// <summary>
	/// Used to find all possible combinations of an array of numbers
	/// </summary>
	public class CombinationFinder
	{
		static List<int[]> _myList;

		/// <summary>
		/// Swap two integers
		/// </summary>
		/// <param name="x">The first value to swap</param>
		/// <param name="y">The second value to swap</param>
		private static void Swap( ref int x, ref int y )
		{
			int tmp = x;
			x = y;
			y = tmp;
		}

		/// <summary>
		/// Get Combinations uses Heap's algorithm to determine all possible
		/// combinations of an array of values
		/// </summary>
		/// <param name="numbers">Array of integer values</param>
		/// <param name="size">size of combinations to find</param>
		private static void GetCombinations(int[] numbers, int size)
		{
			if ( size == 1 )
			{
				// copy the array and add the copy
				int[] temp = (int[])numbers.Clone();

				_myList.Add( temp );
			}
			else
			{
				for( int i = 0; i < size - 1; i++ )
				{
					GetCombinations( numbers, size - 1 );

					if( size % 2 == 0 )
					{ 
						Swap( ref numbers[ i ], ref numbers[ size - 1 ] );
					}
					else
					{ 
						Swap( ref numbers[ 0 ], ref numbers[ size - 1 ] );
					}
				}

				GetCombinations( numbers, size - 1 );
			}
		}

		public static ReadOnlyCollection<int[]> FindCombinations(int[] numbers)
		{
			// create new list for this lot of combinations
			_myList = new List<int[]>();

			GetCombinations( numbers, numbers.Length );

			return _myList.AsReadOnly();
		}
	}
}
