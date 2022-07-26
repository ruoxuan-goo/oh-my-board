using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    public interface Piece
    {
        public string Shape { get; set; }
        public string Colour { get; set; }
    }

    public class BlackPiece : Piece
    {
        public string Shape { get; set; }
        public string Colour { get; set; }

        public BlackPiece()
        {
            this.Colour = "Black";
            this.Shape = " X ";
        }
    }

    public class WhitePiece : Piece
    {
        public string Shape { get; set; }
        public string Colour { get; set; }
        public WhitePiece()
        {
            this.Colour = "White";
            this.Shape = " O ";
        }
    }
}
