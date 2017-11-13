using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    // Candidate class holds a possible solution for Sodoku
    class Candidate
    {
        int[,] _Board;
		int _CurrentRow;	// row being worked on in this candidate, could also determine by finding first row with 0 in it

        public int [,] Board
        {
            get
            {
                return _Board;
            }
        }

		public int CurrentRow
		{
			get { return _CurrentRow; }
		}

        public Candidate(int[,] board)
        {
			// new board starts with row 0
			_CurrentRow = 0;

            _Board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];
            for (int y = 0; y < Settings.BOARD_SIZE; y++)
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                    _Board[x, y] = board[x, y];

			// pre process
			FindPossibleValues();
        }

        // copy constructor
        public Candidate(Candidate otherCandidate)
        {
			// create new board for this candidate
            this._Board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];

			// copy existing board
            for (int y = 0; y < Settings.BOARD_SIZE; y++)
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                    _Board[x, y] = otherCandidate._Board[x, y];

			// set current row for this candidate
			_CurrentRow = otherCandidate.CurrentRow;
        }

		// draw the board
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Settings.BOARD_SIZE; y++)
            {
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                {
                    sb.Append(_Board[x, y].ToString());
                    if (x != 8)
                        sb.Append(" ");
                }
                // end of line
                sb.AppendLine();
            }

            return sb.ToString();
        }

		/// <summary>
		/// GetMissingNumbers
		/// Returns the missing numbers for the current row
		/// params: none
		/// returns: int array containing numbers missing from this row
		/// </summary>
		/// <returns></returns>
		public int[] GetMissingNumbers()
		{
			List<int> testValues = new List<int>();

			for( int i = 0; i < Settings.BOARD_SIZE; i++ )
			{
				testValues.Add( i + 1 );
			}

			// go through the current row
			for( int x = 0; x < Settings.BOARD_SIZE; x++ )
			{
				if( _Board[ x, _CurrentRow ] == 0 )
				{
					continue;
				}

				testValues.Remove( _Board[ x, _CurrentRow ] );
			}

			return testValues.ToArray();
		}

		public void NextRow()
		{
			_CurrentRow++;
		}

		public List<int> GetCombinations()
		{
			throw new NotImplementedException();
		}

		public bool Validate()
		{
			bool result = true;

			// go through each column and check for validity
			for( int col = 0; col < Settings.BOARD_SIZE; col++ )
			{
				if( !ValidateColumn( col ) )
				{
					result = false;
					break;
				}
			}

			if( result )
			{
				for( int cell = 0; cell < Settings.BOARD_SIZE; cell++ )
				{
					// go through each cell and check for validity
					if( !ValidateCell( cell ) )
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		private bool ValidateColumn( int column )
		{
			bool result = true;

			for( int row = 0; row < Settings.BOARD_SIZE - 1; row++ )
			{
				if( _Board[ column, row ] == 0 )
					continue;

				for( int j = row + 1; j < Settings.BOARD_SIZE; j++ )
				{
					if( _Board[ column, row ] == _Board[ column, j ] )
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		private bool ValidateCell( int cell )
		{
			bool result = true;

			int startcolumn = ( cell % 3 ) * 3;
			int startrow = ( int )( cell / 3 ) * 3;
			// Console.WriteLine( "Cell {0} starts at ({1},{2})", cell, column, row );

			// copy cell to linear array
			int[] temp = new int[ Settings.BOARD_SIZE ];

			for ( int row = 0; row < 3; row++ )
			{
				for ( int column = 0; column < 3; column++ )
				{
					int index = row * 3 + column;
					temp[ index ] = _Board[ startcolumn + column, startrow + row ];
				}
			}

			//Console.Write( "Cell {0}: ", cell );
			//foreach( int i in temp )
			//{
			//	Console.Write( "{0} ", i );
			//}
			//Console.WriteLine();

			// check linear array for duplicates
			for( int index = 0; index < temp.Length - 1; index++ )
			{
				if( temp[ index ] == 0 )
					continue;

				for( int j = index + 1; j < temp.Length; j++ )
				{
					if( temp[ index ] == temp[ j ] )
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		public void FillRow(int[] numbers)
		{
			int currentNumberIndex = 0;

			for ( int column = 0; column < Settings.BOARD_SIZE; column++)
			{
				if ( _Board[column, _CurrentRow] == 0 )
				{
					_Board[ column, _CurrentRow ] = numbers[ currentNumberIndex ];
					currentNumberIndex++;
				}
			}

			// this row is full move to next row
			_CurrentRow++;
		}

		/// <summary>
		/// Go through board and determine possible values for each cell
		/// </summary>
		private void FindPossibleValues()
		{
			// Queue to store single possible values
			Queue < int[] > singleValues = new Queue<int[]>();

			// store possible values
			List<int>[,] possibleValues = new List<int>[ Settings.BOARD_SIZE, Settings.BOARD_SIZE ];
			for( int row = 0; row < Settings.BOARD_SIZE; row++ )
			{
				for( int col = 0; col < Settings.BOARD_SIZE; col++ )
				{
					if( _Board[ col, row ] == 0 )
					{
						// empty cell

						// fill a list with all numbers
						possibleValues[ col, row ] = new List<int>();
						for( int n = 0; n < Settings.BOARD_SIZE; n++ )
						{
							possibleValues[ col, row ].Add( n + 1 );
						}

						// go through this row and remove all found values
						for( int columns = 0; columns < Settings.BOARD_SIZE; columns++ )
						{
							// skip empty cells
							if( _Board[ columns, row ] == 0 )
								continue;

							// remove found values from list of possible values
							possibleValues[ col, row ].Remove( _Board[ columns, row ] );
						}

						// go through this column and remove all found values
						for ( int rows = 0; rows < Settings.BOARD_SIZE; rows++)
						{
							// skip empty cells
							if( _Board[ col, rows ] == 0 )
								continue;

							// remove found values from list of possible values
							possibleValues[ col, row ].Remove( _Board[ col, rows ] );
						}

						// go through this square
						int squareX = col / 3;
						int squareY = row / 3;

						for ( int y = 0; y < 3; y++)
						{
							for ( int x = 0; x < 3; x++)
							{
								// skip empty cells
								if( _Board[ x + squareX * 3, y + squareY * 3 ] == 0 )
									continue;

								// remove found value from possible values
								possibleValues[ col, row ].Remove( _Board[ x + squareX * 3, y + squareY * 3 ] );
							}
						}

						// check if possible values is only 1
						if( possibleValues[ col, row ].Count == 1 )
						{
							int[] tmp = new int[ 3 ];
							tmp[ 0 ] = col;
							tmp[ 1 ] = row;
							tmp[ 2 ] = possibleValues[ col, row ][ 0 ];

							singleValues.Enqueue( tmp );
						}
					} //empty cell
				} // col
			} // row - creating lists of possible values

			// go through single values queue and place any numbers in there
			// while queue is not empty
			// dequeue array
			// set location to value x - index 0, y - 1, value - 2
			//possibleValues[x,y] to null
			// go through column x and remove value from any possible values
			// if possible values count is 1 add to queue, if zero set to null
			// go through row y and remove value from any possible values
			// if possible values count is 1 add to queue, if zero set to null
			// go through this cell and remove value from possible values
			// is possible values count is 1 add to queue, if zero set to null
		} // FindPossibleValues 
	} // end class Candidate
}
