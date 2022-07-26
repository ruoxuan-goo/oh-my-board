using System;

namespace OhMyBoard
{
    //TEMPLATE DESIGN
    public abstract class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer;
        
        private Menu menu = new Menu();
        private bool isEnd;
        public bool IsEnd
        {
            get { return isEnd; }   // get method
            set { isEnd = value; }  // set method
        }

        public Menu Menu 
        {
            get { return menu; }   // get method
            set { menu = value; }  // set method
        }
        

        //template
        public void PlayGame()
        {
            InitializeGame();
            PromptMove();
            EndGame();
        }
        protected abstract void InitializeGame();
        protected abstract void CreatePlayer(); //create player and pieces will be assigned
        protected abstract void InitializeBoard(); //create board and set board
        protected abstract void HumanPlaceMove();
        protected abstract void ComputerPlaceMove();
        protected abstract bool CheckInvalid(); //check if move is invalid
        protected abstract void CheckWin();//check if win

        protected void SwitchPlayer()
        {
            if (CurrentPlayer.GetPlayerName() == "P1")
            {
                CurrentPlayer = Player2;
            }
            else
            {
                CurrentPlayer = Player1;
            }
        }

        //prompt user to enter move when game is not end
        protected void PromptMove()
        {
            do
            {
                if (CurrentPlayer.PlayerType == "Human")
                {
                    HumanPlaceMove();
                }
                else
                {
                    ComputerPlaceMove();
                }
            } while (!IsEnd);
        }

        //display winner and end game
        protected void EndGame()
        {
            if (IsEnd == true)
            {
                SwitchPlayer();
                Console.WriteLine();
                Console.WriteLine("Congratulations " + CurrentPlayer.PlayerName + " ! You won!");
                Console.WriteLine();
                ICommand exit = new ExitCommand();
                exit.Execute();

            }
        }
    }

    public class GomokuGame : Game
    {
        GameFactory gomokuFactory = new GomokuFactory();
        Board gomokuBoard;

        Rule gomokuRule = new GomakuRule();

        //temporary store user input
        private int placeX;
        private int placeY;

        protected override void InitializeGame()
        {
            bool isValid = true;

            // while menu option
            while (isValid)
            {
                Menu.DisplayLoadGameMenu();
                switch (Menu.Option)
                {
                    case 1: //start new game 
                        CreatePlayer();
                        InitializeBoard();
                        gomokuBoard.PrintBoard();
                        isValid = false;
                        break;
                    case 2: //load game
                        ICommand load = new LoadCommand(this);
                        load.Execute();

                        SwitchPlayer();
                        InitializeBoard();
                        gomokuBoard.UpdateBoard(MoveHistory.Instance);
                        gomokuBoard.PrintBoard();

                        isValid = false;
                        break;
                }
            }
        }

        protected override void CreatePlayer()
        {
            bool isValid = true;
            Player1 = new HumanPlayer("P1");

            // while menu option
            while (isValid)
            {
                Menu.DisplayModeMenu();
                switch (Menu.Option)
                {
                    case 1: //human vs human
                        Player2 = new HumanPlayer("P2");
                        isValid = false;
                        break;
                    case 2: //human vs computer
                        Player2 = new ComputerPlayer("P2");
                        isValid = false;
                        break;
                }
            }

            CurrentPlayer = Player1;
        }

        protected override void InitializeBoard()
        {
            gomokuBoard = gomokuFactory.CreateBoard();
            gomokuBoard.SetBoard();
        }

        protected override bool CheckInvalid()
        {
            bool invalid = false;

            //check if move is invalid 
            if (MoveHistory.Instance.Moves.Count != 0)
            {
                foreach (Move move in MoveHistory.Instance.Moves)
                {
                    if (placeX == move.X && placeY == move.Y)
                    {
                        invalid = true;
                        break;
                    }
                }
            }
            return invalid;
        }

        protected override void ComputerPlaceMove()
        {
            //generate random numbers till move is valid
            do
            {
                Random rnd = new Random();
                placeX = rnd.Next(1, 16);
                placeY = rnd.Next(1, 16);
            }
            while (CheckInvalid() == true);

            ICommand movecommand = new MoveCommand(placeX, placeY, CurrentPlayer);
            movecommand.Execute();

            gomokuBoard.SetBoard();
            gomokuBoard.UpdateBoard(MoveHistory.Instance);
            gomokuBoard.PrintBoard();

            SwitchPlayer();
            CheckWin();
        }

        protected override void HumanPlaceMove()
        {
            Console.WriteLine();
            gomokuFactory.CreateHelpSystem().DisplayHelpSystem();
            //display player's turn
            Console.WriteLine(CurrentPlayer.PlayerName + " - It's your turn.");
            Console.Write("Enter X and Y Position: ");
            string inputPos = Console.ReadLine();

            try
            {
                if (inputPos == "A")
                {
                    ICommand undo = new UndoCommand();
                    undo.Execute();

                    gomokuBoard.SetBoard();
                    gomokuBoard.UpdateBoard(MoveHistory.Instance);
                    gomokuBoard.PrintBoard();
                }
                else if (inputPos == "B")
                {
                    ICommand redo = new RedoCommand();
                    redo.Execute();

                    gomokuBoard.SetBoard();
                    gomokuBoard.UpdateBoard(MoveHistory.Instance);
                    gomokuBoard.PrintBoard();

                }
                else if (inputPos == "C")
                {
                    ICommand save = new SaveCommand();
                    save.Execute();

                    gomokuBoard.SetBoard();
                    gomokuBoard.UpdateBoard(MoveHistory.Instance);
                    gomokuBoard.PrintBoard();

                }
                else if (inputPos == "D")
                {
                    ICommand load = new LoadCommand(this);
                    load.Execute();
                    SwitchPlayer();

                    gomokuBoard.SetBoard();
                    gomokuBoard.UpdateBoard(MoveHistory.Instance);
                    gomokuBoard.PrintBoard();
                }
                else if (inputPos == "E")
                {
                    ICommand exit = new ExitCommand();
                    exit.Execute();
                }
                else
                {
                    string[] inputSplit = inputPos.Split();
                    placeX = Int32.Parse(inputSplit[0]);
                    placeY = Int32.Parse(inputSplit[1]);

                    if (CheckInvalid() == false)
                    {
                        ICommand move = new MoveCommand(placeX, placeY, CurrentPlayer);
                        move.Execute();
                        SwitchPlayer();

                        gomokuBoard.SetBoard();
                        gomokuBoard.UpdateBoard(MoveHistory.Instance);
                        gomokuBoard.PrintBoard();

                        CheckWin();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Move invalid! Please enter again.");
                        Console.WriteLine();
                    }
                }

            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input! Please try again. ");
            }
        }

        protected override void CheckWin()
        {
            if (gomokuRule.CheckWinStrategy(gomokuBoard, placeX, placeY) == true)
            {
                IsEnd = true;
            }
            else {
                IsEnd = false;
            }
        }

    }
}
