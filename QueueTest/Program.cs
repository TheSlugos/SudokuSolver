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
            Queue<int> myQueue = new Queue<int>();

            bool emptyQueue = myQueue.Empty();

            Console.WriteLine("Queue empty: {0}", emptyQueue);

            //try
            //{
            //    int myNumber = myQueue.Dequeue();
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine("ERROR: {0}", ex.Message);
            //}

            Console.WriteLine("Adding 1 to queue");
            myQueue.Enqueue(1);
            Console.WriteLine("Adding 5 to queue");
            myQueue.Enqueue(5);
            Console.WriteLine("Adding 2 to queue");
            myQueue.Enqueue(2);
            Console.WriteLine("Adding 10 to queue");
            myQueue.Enqueue(10);
            Console.WriteLine("Adding 15 to queue");
            myQueue.Enqueue(15);

            while (!myQueue.Empty())
            {
                int number = -1;

                try
                {
                    number = myQueue.Dequeue();
                }
                catch
                {
                    Console.WriteLine("ERROR: no items on the queue");
                }

                Console.WriteLine("Number {0}", number);
            }

            int[,] board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];

            for (int y = 0; y < Settings.BOARD_SIZE; y++)
            {
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                {
                    board[x, y] = 0;
                }
            }

			// row 0
			board[ 1, 0 ] = 3;
			board[ 3, 0 ] = 6;
			board[ 5, 0 ] = 5;
			// row 1
			board[ 0, 1 ] = 8;
			board[ 7, 1 ] = 3;
			// row 2
			board[ 2, 2 ] = 4;
			board[ 4, 2 ] = 1;
			board[ 7, 2 ] = 5;
			board[ 8, 2 ] = 9;
			//row 3
			board[ 3, 3 ] = 7;
			board[ 5, 3 ] = 1;
			board[ 8, 3 ] = 6;
			// row 4
			board[ 1, 4 ] = 6;
			board[ 7, 4 ] = 1;
			// row 5
			board[ 0, 5 ] = 5;
			board[ 3, 5 ] = 3;
			board[ 5, 5 ] = 2;
			// row 6
			board[ 0, 6 ] = 9;
			board[ 1, 6 ] = 5;
			board[ 4, 6 ] = 2;
			board[ 6, 6 ] = 3;
			// row 7
			board[ 1, 7 ] = 7;
			board[ 8, 7 ] = 8;
			// row 8
			board[ 3, 8 ] = 1;
			board[ 5, 8 ] = 7;
			board[ 7, 8 ] = 9;

			board[ 7, 0 ] = 4;

			Candidate candidate = new Candidate(board);
			Candidate solution = default( Candidate );
			int count = 0;
			int valid = 0;

			Console.Write(candidate);
			Console.WriteLine( candidate.Validate().ToString() );
			Console.ReadKey( true );

			Queue<Candidate> candidateQueue = new Queue<Candidate>();
			candidateQueue.Enqueue( candidate );

			Stopwatch sw = new Stopwatch();
			sw.Start();

			// SOLUTION LOOP
			while( !candidateQueue.Empty() )
			{
				// get latest possible solution off the queue
				Candidate baseSolution = candidateQueue.Dequeue();

				// crude drop out
				if( baseSolution.CurrentRow == Settings.BOARD_SIZE )
				{
					solution = baseSolution;
					break;
				}

				//Console.WriteLine( baseSolution );
				// get missing numbers from the current row of base
				int[] missing = baseSolution.GetMissingNumbers();

				//Console.Write( "Missing numbers:" );

				//foreach( int i in missing )
				//{
				//	Console.Write( i.ToString() );
				//}
				//Console.WriteLine();

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
						count++;

						// create a new possible solution from the base
						Candidate current = new Candidate( baseSolution );

						current.FillRow( values );

						//Console.WriteLine( "Solution {0}", count );
						//Console.WriteLine( current );

						if( current.Validate() )
						{
							candidateQueue.Enqueue( current );
							valid++;
						}
						//Console.WriteLine( current );
					}

					//Console.WriteLine( "Total valid solutions: {0}", candidateQueue.Count );

					//foreach( int[] i in combinationfinder.findcombinations( missing ) )
					//{
					//	foreach( int x in i )
					//	{
					//		console.write( x );
					//	}
					//	console.writeline();
					//}

					//Console.Write( "First combination: " );
					//foreach(int i in list[0])
					//{
					//	Console.Write( "{0} ", i );
					//}
					//Console.WriteLine();

					//current.FillRow( list[ 0 ] );

					//Console.WriteLine( current );
					//Console.WriteLine( "Current row for current is {0}", current.CurrentRow );
					//Console.WriteLine( "Current solution is valid: {0}", current.Validate() );
				}
			}
			sw.Stop();
			switch( solution )
			{
				case default( Candidate ):
					Console.WriteLine( "No solution found" );
					break;
				default:
					Console.WriteLine( "SOLUTION IS:" );
				Console.WriteLine( solution );
					break;
			}

			Console.WriteLine( "Puzzle solved in {0} ms", sw.ElapsedMilliseconds);
			Console.WriteLine( "Solutions attempted: {0}", count );
			Console.WriteLine( "Valid solutions pushed to queue: {0}", valid );
		} // end Main
    }
}
