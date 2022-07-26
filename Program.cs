namespace OhMyBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            bool isValid = true;

            while (isValid)
            {
                menu.DisplayGameMenu();
                if (menu.Option == 1)
                {
                    Game gomokuGame = new GomokuGame();
                    gomokuGame.PlayGame();
                    isValid = false;
                }
            }
        }
    }
}
