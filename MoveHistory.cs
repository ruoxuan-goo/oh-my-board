using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    //SINGLETON
    public sealed class MoveHistory
    {
        public  List<Move> Moves { get; set; }
        public List<Move> ReverseMoves { get; set; }
        public int index = 0;
        private static MoveHistory instance;
        private MoveHistory()
        {
            Moves = new List<Move>();
            ReverseMoves = new List<Move>();
        }

        public static MoveHistory Instance
        {
            get{
                if (instance == null)
                {
                    instance = new MoveHistory();
                }
                return instance;
            }
            
        }

        public int CountMoves()
        {
            return Moves.Count;
        }
    }
}
