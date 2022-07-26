using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    public interface Board
    {
        public string[,] Boxes { get; set; }
        public void SetBoard() { }
        public void UpdateBoard(MoveHistory moves) { }
        public void PrintBoard() { }
    }

    public class GomokuBoard : Board
    {
        private readonly string box = " . ";
        public string Box { get; set; }
        public int BoardRow { get; set; }
        public int BoardColumn { get; set; }
        public string[,] Boxes { get; set; }  

        public GomokuBoard(int boardRow, int boardColumn)
        {
            BoardRow = boardRow;
            BoardColumn = boardColumn;
            Boxes = new string[BoardRow, BoardColumn];
        }

        public void SetBoard()
        {
            for (int i = 0; i < BoardRow; i++)
            {
                for (int j = 0; j < BoardColumn; j++)
                {
                    Boxes[i, j] = box;
                }
            }
        }

        public void UpdateBoard(MoveHistory moves)
        {
            for (int i = 0; i < moves.CountMoves(); i++)
            {
                int printx = moves.Moves[i].X - 1; //minus one as array starts from 0,0 
                int printy = moves.Moves[i].Y - 1;
                string piece = moves.Moves[i].Player.AssignedPiece();
                Boxes[printx, printy] = piece;
            }
        }

        public void PrintBoard()
        {
            //Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" 1  2  3  4  5  6  7  8  9  10 11 12 13 14 15");
            for (int i = 0; i < BoardRow; ++i)
            {
                for (int j = 0; j < BoardColumn; ++j)
                {
                    // reverse as two dimension array stores in reverse
                    Console.Write(Boxes[j, i]);
                }
                Console.WriteLine(i+1);
            }
        }
    }
}
