using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecficComboFinder
{
	class Program
	{
		static void Main( string[] args )
		{
			SpecificComboFinderClass scf = new SpecificComboFinderClass();
			scf.test();
		}
	}

	public class SpecificComboFinderClass
	{
		List<int[]> combinations;

		public void test()
		{
			List<int> lista = new List<int>();
			lista.Add( 1 );
			lista.Add( 2 );
			lista.Add( 3 );

			List<int> listb = new List<int>();
			listb.Add( 1 );
			listb.Add( 2 );
			listb.Add( 3 );

			List<int> listc = new List<int>();
			listc.Add( 2 );
			listc.Add( 3 );

			List<List<int>> allValues = new List<List<int>>();
			allValues.Add( lista );
			allValues.Add( listc );
			allValues.Add( listb );

			int[] combo = new int[ allValues.Count ];

			combinations = new List<int[]>();

			FindSpecificCombo( allValues, 0, combo );
		}

		public void FindSpecificCombo( List<List<int>> numbers, int index, int[] combination )
		{
			if( index == numbers.Count )
			{
				for( int x = 0; x < combination.Length; x++ )
				{
					Console.Write( combination[ x ] );
				}
				Console.WriteLine();

				int[] tmp = (int[])combination.Clone();
				combinations.Add( tmp );
			}
			else
			{
				for( int n = 0; n < numbers[ index ].Count; n++ )
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
