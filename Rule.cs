using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyBoard
{
    public abstract class Rule
    {
        public abstract bool CheckWinStrategy(Board board, int inputx, int inputy);
    }

    public class GomakuRule : Rule
    {
        public override bool CheckWinStrategy(Board board, int inputx, int inputy)
        {
            int score = 0;
            int x = inputx - 1;
            int y = inputy - 1;
            int tempX = x, tempY = y;

            //1
            while (y > 0)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[x, --y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;

            while (y < 14)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[x, ++y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY; score = 0;

            //2 
            while (x > 0)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[--x, y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;

            while (x < 14)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[++x, y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY; score = 0;

            //3
            while (y > 0 && x > 0)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[--x, --y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;

            while (y < 14 && x < 14)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[++x, ++y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY; score = 0;

            //4 
            while (y > 0 && x < 14)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[++x, --y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            x = tempX; y = tempY;

            while (y < 14 && x > 0)
            {
                if (board.Boxes[tempX, tempY] == board.Boxes[--x, ++y])
                {
                    score++;
                }
                else
                    break;
            }
            if (score >= 4)
            {
                return true;
            }
            return false;
        }
    }
}
