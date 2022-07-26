using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    //ABSTRACT FACTORY DESIGN
    public interface GameFactory
    {
        Board CreateBoard();
        Piece CreateWhitePiece();
        Piece CreateBlackPiece();
        HelpSystem CreateHelpSystem();
    }

    public class GomokuFactory : GameFactory
    {
        public Board CreateBoard()
        {
            Board gomakuBoard = new GomokuBoard(15, 15);
            return gomakuBoard;
        }

        public Piece CreateWhitePiece()
        {
            Piece whitePiece = new WhitePiece();
            return whitePiece;
        }

        public Piece CreateBlackPiece()
        {
            Piece blackPiece = new BlackPiece();
            return blackPiece;
        }

        public HelpSystem CreateHelpSystem()
        {
            return new GomakuHelpSystem();
        }
    }
}
