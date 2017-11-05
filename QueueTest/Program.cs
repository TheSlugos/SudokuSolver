using System;
using System.Collections.Generic;
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

            board[0, 0] = 1;
            board[1, 0] = 2;
            board[2, 0] = 3;

            Candidate candidate = new Candidate(board);
            Console.WriteLine(candidate);

            Console.WriteLine("Adding another value to board");

            board[3, 0] = 4;

            Console.WriteLine(candidate);

            // make a copy of this candidate
            Candidate newCandidate = new Candidate(candidate);

            Console.WriteLine("Add another value to candidate using Property");

            candidate.Board[3, 0] = 5;

            Console.WriteLine(candidate);

            Console.WriteLine("Copy of candidate before adding the 5");

            Console.WriteLine(newCandidate);

            Console.WriteLine("Changing new candidate");

            newCandidate.Board[3, 0] = 4;

            Console.WriteLine(newCandidate);

            Console.WriteLine("original candidate");

            Console.WriteLine(candidate);

            Console.WriteLine("Find Missing characters test");

            int[] row = { 9, 4, 3, 0, 2, 5, 6, 7, 8 };

            int[] testValues = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for(int i=0; i < row.Length; i++)
            {
                if (row[i] == 0)
                    continue;

                // have a number that is not 0
                int[] temp = new int[testValues.Length - 1];
                int copied = 0;
                for(int j = 0; j < testValues.Length; j++)
                {
                    if ( testValues[j] != row[i])
                    {
                        temp[j - copied] = testValues[j];
                    }
                    else
                    {
                        copied++;
                    }
                }

                testValues = temp;
            }

            if ( testValues.Length > 0 )
            {
                Console.Write("Missing values are: ");

                for ( int i =0;i<testValues.Length;i++)
                {
                    Console.Write(testValues[i]);

                    if (i != testValues.Length - 1)
                        Console.Write(" ");
                    else
                        Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Row is complete");
            }

        } // end Main
    }
}
