using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTest
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
		{  get { return _CurrentRow; }
		}

        public Candidate(int[,] board)
        {
			// new board starts with row 0
			_CurrentRow = 0;

            _Board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];
            for (int y = 0; y < Settings.BOARD_SIZE; y++)
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                    _Board[x, y] = board[x, y];
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
			_CurrentRow = otherCandidate.CurrentRow + 1;
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

		public List<int> GetCombinations()
		{
			throw new NotImplementedException();
		}
    }
}
