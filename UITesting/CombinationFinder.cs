using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
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

		public static ReadOnlyCollection<int[]> FindTargettedCombinations(List<int[]> numbers)
		{
			// create new list for this lot of combinations
			_myList = new List<int[]>();

			int[] combination = new int[ numbers.Count ];

			FindSpecificCombo( numbers, 0, combination );

			return _myList.AsReadOnly();
		}

		private static void FindSpecificCombo( List<int[]> numbers, int index, int[] combination )
		{
			if( index == numbers.Count )
			{
				for( int x = 0; x < combination.Length; x++ )
				{
					Console.Write( combination[ x ] );
				}
				Console.WriteLine();

				int[] tmp = ( int[] )combination.Clone();
				_myList.Add( tmp );
			}
			else
			{
				for( int n = 0; n < numbers[ index ].Length; n++ )
				{
					// make sure current element of array is cleared
					combination[ index ] = 0;

					bool duplicate = false;
					// check for duplicate value
					// check up to this value and if duplicate found in array skip this value
					for( int j = 0; j <= index; j++ )
					{
						if( combination[ j ] == numbers[ index ][ n ] )
						{
							duplicate = true;
							break;
						}
					}

					if( !duplicate )
					{
						combination[ index ] = numbers[ index ][ n ];
						FindSpecificCombo( numbers, index + 1, combination );
					}
				}
			}
		}
	}
}
