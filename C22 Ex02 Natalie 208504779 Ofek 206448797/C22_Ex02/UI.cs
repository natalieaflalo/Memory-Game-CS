using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public static class UI
    {
        public static void StartGame()
        {
            int numOfRows = 0;
            int numOfColumns = 0;
            Console.WriteLine("Enter number of rows:{0}", Environment.NewLine);
            string stringNumOfRows = Console.ReadLine();
            Console.WriteLine("Enter number of columns:{0}", Environment.NewLine);
            string stringNumOfColumns = Console.ReadLine();

            while ((!isLegalInput(stringNumOfRows, ref numOfRows)) || (!isLegalInput(stringNumOfColumns, ref numOfColumns)))
            {
                Console.WriteLine("Input is not a legal. Please try again");
                Console.WriteLine("Enter number of rows:{0}", Environment.NewLine);
                stringNumOfRows = Console.ReadLine();
                Console.WriteLine("Enter number of columns:{0}", Environment.NewLine);
                stringNumOfColumns = Console.ReadLine();
            }
            MemoryGameBoard game = new MemoryGameBoard(numOfRows, numOfColumns);
        }

        private static bool isLegalInput(string i_StringNumber, ref int io_ValidNumber)
        {
            bool isLegal = false;

            if (int.TryParse(i_StringNumber, out io_ValidNumber) && io_ValidNumber >= 0)
            {
                isLegal = true;
            }

            
            return isLegal;
        }

        /*private static void QuitGame() 
        {
            if (userGuessInput.Equals("Q") || userGuessInput.Equals("q"))
            {
                game.QuitGame();
                break;
            }
        }
        */
    }
}
