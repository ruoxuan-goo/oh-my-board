using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Player Player { get; set; }

        public Move(int x, int y, Player player)
        {
            X = x;
            Y = y;
            Player = player;
        }
        public string ToCSVString()
        {
            return String.Format("{0},{1},{2},{3}", X, Y, Player.PlayerName, Player.PlayerType);
        }

    }
}
