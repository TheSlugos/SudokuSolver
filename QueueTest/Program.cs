using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Queue<int> myQueue = new Queue<int>();

            //bool emptyQueue = myQueue.Empty();

            //Console.WriteLine("Queue empty: {0}", emptyQueue);

            //try
            //{
            //    int myNumber = myQueue.Dequeue();
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("ERROR: {0}", ex.Message);
            //}

            //Console.WriteLine("Adding 1 to queue");
            //myQueue.Enqueue(1);
            //Console.WriteLine("Adding 5 to queue");
            //myQueue.Enqueue(5);
            //Console.WriteLine("Adding 2 to queue");
            //myQueue.Enqueue(2);
            //Console.WriteLine("Adding 10 to queue");
            //myQueue.Enqueue(10);
            //Console.WriteLine("Adding 15 to queue");
            //myQueue.Enqueue(15);

            //while (!myQueue.Empty())
            //{
            //    int number = -1;

            //    try
            //    {
            //        number = myQueue.Dequeue();
            //    }
            //    catch
            //    {
            //        Console.WriteLine("ERROR: no items on the queue");
            //    }

            //    Console.WriteLine("Number {0}", number);
            //}

            int[,] board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];

            for (int y = 0; y < Settings.BOARD_SIZE; y++)
            {
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                {
                    board[x, y] = 0;
                }
            }

			// row 0
			board[ 1, 0 ] = 5;
			board[ 2, 0 ] = 1;
			board[ 5, 0 ] = 8;
			board[ 6, 0 ] = 9;
			board[ 7, 0 ] = 7;
			// row 1
			board[ 2, 1 ] = 2;
			board[ 3, 1 ] = 1;
			board[ 8, 1 ] = 8;
			// row 2
			board[ 0, 2 ] = 6;
			board[ 1, 2 ] = 8;
			board[ 4, 2 ] = 7;
			board[ 5, 2 ] = 5;
			board[ 6, 2 ] = 2;
			board[ 7, 2 ] = 4;
			//row 3
			board[ 3, 3 ] = 5;
			// row 4
			board[ 1, 4 ] = 3;
			board[ 2, 4 ] = 4;
			board[ 6, 4 ] = 5;
			board[ 7, 4 ] = 8;
			// row 5
			board[ 5, 5 ] = 3;
			// row 6
			board[ 1, 6 ] = 4;
			board[ 2, 6 ] = 5;
			board[ 3, 6 ] = 8;
			board[ 4, 6 ] = 3;
			board[ 7, 6 ] = 9;
			board[ 8, 6 ] = 2;
			// row 7
			board[ 0, 7 ] = 1;
			board[ 5, 7 ] = 2;
			board[ 6, 7 ] = 7;
			// row 8
			board[ 1, 8 ] = 2;
			board[ 2, 8 ] = 3;
			board[ 3, 8 ] = 4;
			board[ 6, 8 ] = 8;
			board[ 7, 8 ] = 1;

			//board[ 7, 0 ] = 4;

			//Candidate candidate = new Candidate(board);
			//Candidate solution = default( Candidate );
			//int count = 0;
			//int valid = 0;

			//Console.Write(candidate);
			//Console.WriteLine( candidate.Validate().ToString() );
			//Console.ReadKey( true );

			//Queue<Candidate> candidateQueue = new Queue<Candidate>();
			//candidateQueue.Enqueue( candidate );

			//Stopwatch sw = new Stopwatch();
			//sw.Start();

			//// SOLUTION LOOP
			//while( !candidateQueue.Empty() )
			//{
			//	// get latest possible solution off the queue
			//	Candidate baseSolution = candidateQueue.Dequeue();

			//	// crude drop out
			//	if( baseSolution.CurrentRow == Settings.BOARD_SIZE )
			//	{
			//		solution = baseSolution;
			//		break;
			//	}

			//	//Console.WriteLine( baseSolution );
			//	// get missing numbers from the current row of base
			//	int[] missing = baseSolution.GetMissingNumbers();

			//	//Console.Write( "Missing numbers:" );

			//	//foreach( int i in missing )
			//	//{
			//	//	Console.Write( i.ToString() );
			//	//}
			//	//Console.WriteLine();

			//	if( missing.Length > 0 )
			//	{
			//		// get list of all possible combinations of missing values for this row
			//		ReadOnlyCollection<int[]> list = CombinationFinder.FindCombinations( missing );
			//		//Console.WriteLine( "Total combinations of missing numbers: {0}", list.Count.ToString() );

			//		// create a new candidate for each combination of missing values
			//		// fill the current row
			//		// if solution is valid add to the queue
			//		foreach( int[] values in list )
			//		{
			//			count++;

			//			// create a new possible solution from the base
			//			Candidate current = new Candidate( baseSolution );

			//			current.FillRow( values );

			//			//Console.WriteLine( "Solution {0}", count );
			//			//Console.WriteLine( current );

			//			if( current.Validate() )
			//			{
			//				candidateQueue.Enqueue( current );
			//				valid++;
			//			}
			//			//Console.WriteLine( current );
			//		}

			//		//Console.WriteLine( "Total valid solutions: {0}", candidateQueue.Count );

			//		//foreach( int[] i in combinationfinder.findcombinations( missing ) )
			//		//{
			//		//	foreach( int x in i )
			//		//	{
			//		//		console.write( x );
			//		//	}
			//		//	console.writeline();
			//		//}

			//		//Console.Write( "First combination: " );
			//		//foreach(int i in list[0])
			//		//{
			//		//	Console.Write( "{0} ", i );
			//		//}
			//		//Console.WriteLine();

			//		//current.FillRow( list[ 0 ] );

			//		//Console.WriteLine( current );
			//		//Console.WriteLine( "Current row for current is {0}", current.CurrentRow );
			//		//Console.WriteLine( "Current solution is valid: {0}", current.Validate() );
			//	}
			//}
			//sw.Stop();

			SudokuBoard mySuDoku = new SudokuBoard( board );
			Console.Write( mySuDoku.Board );
			Console.WriteLine( "Press a key to continue" );
			Console.ReadKey( true );
			if ( mySuDoku.Solve() )
			{
				Console.WriteLine( "Solution found:" );
				Console.Write( mySuDoku.Solution.ToString() );

			}
			else
			{
				Console.WriteLine( "No solution found." );
			}

			Console.WriteLine( "Solution, or otherwise, found in {0} ms", mySuDoku.TimeTaken );
			Console.WriteLine( "Solutions attempted: {0}", mySuDoku.Attempts );
			Console.WriteLine( "Valid solutions pushed to queue: {0}", mySuDoku.Valid );
		} // end Main
    }
}
