using System;

namespace OhMyBoard
{
    public abstract class HelpSystem
    {
        public string Help { get; set; }
        public string MoveOptions { get; set; }
        public void DisplayHelpSystem()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Help System:");
            Console.WriteLine();
            Console.WriteLine(Help);
            Console.WriteLine(MoveOptions);
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
    }

    public class GomakuHelpSystem : HelpSystem
    {
        public GomakuHelpSystem(){
            Help = "P1: O   P2: X \nPlace Piece: enter X and Y coordinate, seperate with a space, then press ENTER on your keyboard." +
                "\n**  Example - Enter X and Y Position: 5 6  **\n \nOtherwise, enter the following options";
            MoveOptions = "A)Undo  B)Redo  C)Save Game  D)Load Game  E)Exit Game\n**  Example - Enter X and Y Position: C  ** will save your current game. ";
        }
    }
}
