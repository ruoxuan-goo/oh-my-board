using System;
using System.IO;

namespace OhMyBoard
{
    //COMMAND DESIGN
    public interface ICommand
    {
        public void Execute();
    }

    public class MoveCommand : ICommand
    {
        private Move Move { get; set; }
        private MoveHistory movehistory = MoveHistory.Instance;

        public MoveCommand(int x, int y, Player player)
        {
            Move = new Move(x, y, player);
        }

        public void Execute()
        {
            if (movehistory.ReverseMoves.Count != 0)
            {
                movehistory.ReverseMoves.Clear(); //clear all moves that has been undo
            }
            MoveHistory.Instance.Moves.Add(Move);
            MoveHistory.Instance.index++;
        }
    }

    public class UndoCommand : ICommand
    {
        private MoveHistory movehistory = MoveHistory.Instance;

        public void Execute()
        {
            if (movehistory.Moves.Count == 0 || movehistory.Moves.Count == 1)
            {
                Console.WriteLine();
                Console.WriteLine("You cannot undo!");
                Console.WriteLine();
                return;
            }
            if (movehistory.index > 1)
            {
                Move lastitem = movehistory.Moves[movehistory.index - 1];
                Move secondlastitem = movehistory.Moves[movehistory.index - 2];
                //remove moves from move list and add to reverse move list
                movehistory.ReverseMoves.Add(lastitem);
                movehistory.Moves.Remove(lastitem);
                movehistory.ReverseMoves.Add(secondlastitem);
                movehistory.Moves.Remove(secondlastitem);
                MoveHistory.Instance.index--;
                MoveHistory.Instance.index--;
            }
        }

    }

    public class RedoCommand : ICommand
    {
        private MoveHistory movehistory = MoveHistory.Instance;
        public void Execute()
        {
            if (movehistory.ReverseMoves.Count == 0 || movehistory.ReverseMoves.Count == 1)
            {
                Console.WriteLine();
                Console.WriteLine("You cannot redo!");
                Console.WriteLine();
                return;
            }

            if (movehistory.ReverseMoves.Count > 1)
            {
                Move lastitem = movehistory.ReverseMoves[movehistory.ReverseMoves.Count - 1];
                Move secondlastitem = movehistory.ReverseMoves[movehistory.ReverseMoves.Count - 2];
                //remove moves from move list and add to reverse move list
                movehistory.Moves.Add(lastitem);
                movehistory.ReverseMoves.Remove(lastitem);
                movehistory.Moves.Add(secondlastitem);
                movehistory.ReverseMoves.Remove(secondlastitem);
                MoveHistory.Instance.index++;
                MoveHistory.Instance.index++;
            }
        }
    }

    public class LoadCommand : ICommand
    {
        private string MoveHistoryFile { get; set; }
        private Game game;
        
        public LoadCommand(Game game)
        {
            this.game = game;
        }

        private bool AddMove(Move move)
        {
            MoveHistory.Instance.Moves.Add(move);
            return true;
        }

        private void LoadFromFile()
        {
            MoveHistoryFile = "movehistory.csv";

            if (!File.Exists("movehistory.csv"))
            {
                Console.WriteLine();
                Console.WriteLine("No saved game found! You need to save a game before loading. ");
                Console.WriteLine();
            } else { 

                using (StreamReader sr = new StreamReader(MoveHistoryFile))
                { 
                    //Read the headline of csv file
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] tokens = line.Split(',');

                        if (tokens[2] == "P1")
                        {
                            game.Player1 = new HumanPlayer("P1");
                            AddMove(new Move(int.Parse(tokens[0]), int.Parse(tokens[1]), game.Player1));
                            game.CurrentPlayer = game.Player1;
                        } else 
                        {
                            if (tokens[3] == "Human") {
                                game.Player2 = new HumanPlayer("P2");
                            }
                            else
                            {
                                game.Player2 = new ComputerPlayer("P2");
                            }     
                            AddMove(new Move(int.Parse(tokens[0]), int.Parse(tokens[1]), game.Player2));
                            game.CurrentPlayer = game.Player2;
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Game successfully loaded!");
                Console.WriteLine();
            }

        }

        public void Execute()
        {
            MoveHistory.Instance.ReverseMoves.Clear();
            MoveHistory.Instance.Moves.Clear();
            LoadFromFile();
        }
    }


    public class SaveCommand : ICommand
    {
        private string MoveHistoryFile { get; set; }
        private MoveHistory movehistory = MoveHistory.Instance;

        public void SaveToFile()
        {
            MoveHistoryFile = "movehistory.csv";

            // if cannot find file, create new file 
            // then continue save 
            if (!File.Exists("movehistory.csv"))
            {
                FileStream stream = File.Create("movehistory.csv");
                stream.Close();
            }

            using (StreamWriter sw = new StreamWriter(MoveHistoryFile))
            {
                //Write the headline of csv file
                sw.WriteLine("X,Y,Player,PlayerType");
                foreach (Move move in MoveHistory.Instance.Moves)
                {
                    sw.WriteLine(move.ToCSVString());
                }
            }
        }

        public void Execute()
        {
            if (movehistory.Moves.Count >= 2)
            {
                SaveToFile();
                Console.WriteLine();
                Console.WriteLine("Game saved!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You need at least two moves to save! Place one more move to save.");
                Console.WriteLine();
            }
        }
    }

    public class ExitCommand : ICommand
    {
        public void Execute(){
            Console.WriteLine();
            Console.WriteLine("See you again!");
            Environment.Exit(0);
        }
    }
}


