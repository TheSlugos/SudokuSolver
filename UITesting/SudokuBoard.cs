using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using UITesting;

namespace SudokuSolver
{
	class SudokuBoard
	{
		// Candidate
		Candidate _Board;

		int _ValidSolutionsOnQueue;
		int _SolutionCombinationsAttempted;

		Stopwatch _sw;

		// holds the solution ????
		Candidate _Solution;

		public int Attempts
		{
			get { return _SolutionCombinationsAttempted; }
		}

		public int Valid
		{
			get { return _ValidSolutionsOnQueue; }
		}

		public long TimeTaken
		{
			get { return _sw.ElapsedMilliseconds; }
		}

		//public int[,] Solution
		//{
		//	get { return _Solution.Board; }
		//}

		public Candidate Solution
		{
			get { return _Solution; }
		}

		public Candidate Board
		{
			get { return _Board; }
		}

		public SudokuBoard( int[,] board )
		{
			// should check for board size and not null
			// for now assume it is a full board

			// copy the board
			_Board = new Candidate( board );

			// initialise
			_ValidSolutionsOnQueue = 0;
			_SolutionCombinationsAttempted = 0;
			_sw = new Stopwatch();
			_Solution = default( Candidate );
		}

		public bool TargettedSolve( Form1 theForm, bool doPre )
		{
			return false;
		}

		public bool Solve(Form1 theForm, bool doPre)
		{
			bool result = false;
			int currentRow;
			long lastTime = 0;

			_sw.Start();

			// preprocess the board
			if( doPre )
			{
				//MessageBox.Show( "Pre Stuff" );
				_Board.FindPossibleValues();
			}
			// create a candidate
			// create the queue to hold the solution candidates and add the board to it
			Queue<Candidate> candidateQueue = new Queue<Candidate>();
			candidateQueue.Enqueue( _Board );
			currentRow = 0;

			// SOLUTION LOOP
			while( !candidateQueue.Empty() )
			{
				// get latest possible solution off the queue
				Candidate baseSolution = candidateQueue.Dequeue();

				//if( baseSolution.CurrentRow != currentRow )
				//{
				//	if( theForm.InvokeRequired )
				//	{
				//		currentRow = baseSolution.CurrentRow;
				//		theForm.Invoke( theForm.rowLabelDelegate, new object[] { currentRow, Valid, Attempts, _sw.ElapsedMilliseconds } );
				//	}
				//}
				long thisTime = _sw.ElapsedMilliseconds;
				if( thisTime - lastTime > 100 || currentRow != baseSolution.CurrentRow)
				{
					lastTime = thisTime;
					currentRow = baseSolution.CurrentRow;

					if( theForm.InvokeRequired )
					{
						theForm.Invoke( theForm.rowLabelDelegate, new object[] { baseSolution.CurrentRow, Valid, Attempts, lastTime } );
					}
				}

				// crude drop out
				if( baseSolution.CurrentRow == Settings.BOARD_SIZE )
				{
					if( theForm.InvokeRequired )
					{
						theForm.Invoke( theForm.rowLabelDelegate, new object[] { currentRow, Valid, Attempts, lastTime } );
					}

					_Solution = baseSolution;
					result = true;
					break;
				}

				// get missing numbers from the current row of base
				int[] missing = baseSolution.GetMissingNumbers();

				if( missing.Length > 0 )
				{
					// get list of all possible combinations of missing values for this row
					ReadOnlyCollection<int[]> list = CombinationFinder.FindCombinations( missing );
					//Console.WriteLine( "Total combinations of missing numbers: {0}", list.Count.ToString() );

					// create a new candidate for each combination of missing values
					// fill the current row
					// if solution is valid add to the queue
					foreach( int[] values in list )
					{
						_SolutionCombinationsAttempted++;

						// create a new possible solution from the base
						Candidate current = new Candidate( baseSolution );

						current.FillRow( values );

						//Console.WriteLine( "Solution {0}", count );
						//Console.WriteLine( current );

						if( current.Validate() )
						{
							candidateQueue.Enqueue( current );
							_ValidSolutionsOnQueue++;
						}
						//Console.WriteLine( current );
					}
				}
				else
				{
					baseSolution.NextRow();
					candidateQueue.Enqueue( baseSolution );
				}
			}

			_sw.Stop();

			return result;
		}
	}
}
