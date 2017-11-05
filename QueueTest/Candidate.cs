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

        public int [,] Board
        {
            get
            {
                return _Board;
            }
        }

        public Candidate()
        {

        }

        public Candidate(int[,] board)
        {
            _Board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];
            for (int y = 0; y < Settings.BOARD_SIZE; y++)
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                    _Board[x, y] = board[x, y];
        }

        // copy constructor
        public Candidate(Candidate otherCandidate)
        {
            this._Board = new int[Settings.BOARD_SIZE, Settings.BOARD_SIZE];

            for (int y = 0; y < Settings.BOARD_SIZE; y++)
                for (int x = 0; x < Settings.BOARD_SIZE; x++)
                    _Board[x, y] = otherCandidate._Board[x, y];
        }

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
    }
}
