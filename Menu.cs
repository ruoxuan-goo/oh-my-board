using System;
using System.Collections.Generic;
using System.Text;

namespace OhMyBoard
{
    public class Menu
    {
        public int Option {get;set;}

        public int GetOption()
        {
            Console.Write("Your choice: ");
            try
            {
                Option = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input! Please try again.");
                Console.WriteLine();
            }
            return Option;
        }

        public void DisplayGameMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to OhMyBoard! - Brisbane's #1 platform for playing board games.");
            Console.WriteLine();
            Console.WriteLine("Choose a game...");
            Console.WriteLine("1. Gomoku");
            Console.WriteLine("2. Connect Four (Coming Soon)");
            Console.WriteLine("3. Chess (Coming Soon)");
            Console.WriteLine();
            GetOption();
        }

        public void DisplayLoadGameMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to...");
            Console.WriteLine("1. Start a new game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine();
            GetOption();
        }

        public void DisplayModeMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a mode...");
            Console.WriteLine("1. Human vs Human");
            Console.WriteLine("2. Human vs Computer");
            Console.WriteLine();
            GetOption();
        }
    }
}
