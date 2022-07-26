using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    public abstract class Player
    {
        public string PlayerName { get; set; }
        public string PlayerType { get; set; }
        public Piece Piece { get; set; }
        GameFactory gomokuFactory= new GomokuFactory();
        
        //when player is created, assign piece
        public string AssignedPiece()
        {

            if (PlayerName == "P1")
            {
                Piece = gomokuFactory.CreateWhitePiece();
                return Piece.Shape;
            }
            else
            {
                Piece = gomokuFactory.CreateBlackPiece();
                return Piece.Shape;
            }
        }

        public string GetPlayerName()
        {
            return PlayerName;
        }

    }

    public class HumanPlayer: Player
    {
        public HumanPlayer(string playerName)
        {
            PlayerName = playerName;
            PlayerType = "Human";
        }
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string playerName)
        {
            PlayerName = playerName;
            PlayerType = "Computer";
        }
    }
}
