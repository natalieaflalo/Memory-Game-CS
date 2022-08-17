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

            while ((!LogicValidationsForUI.isLegalSizeOfMatrix(stringNumOfRows, ref numOfRows)) || (!LogicValidationsForUI.isLegalSizeOfMatrix(stringNumOfColumns, ref numOfColumns)))
            {
                Console.WriteLine("Input is not a legal. Please try again");
                Console.WriteLine("Enter number of rows:{0}", Environment.NewLine);
                stringNumOfRows = Console.ReadLine();
                Console.WriteLine("Enter number of columns:{0}", Environment.NewLine);
                stringNumOfColumns = Console.ReadLine();
            }

            MemoryGameBoard game = new MemoryGameBoard(numOfRows, numOfColumns);

            //ask if playing with computer or player
            //ask for name\s
            //ask for block ID
            //check isValidBlockID and flip it
            //ask again and check again
        }

        //private static void playerTurn()
        //{
        //  ask for block ID
        //  check isValidBlockID and flip it
        //  ask again and check again

        //private static void computerTurn()
        // randomly choose valid block and 

        //private static bool isValidBlockID()

        private static void QuitGame() 
        {
            if (userGuessInput.Equals("Q") || userGuessInput.Equals("q"))
            {
                game.QuitGame();
                break;
            }
        }
        
    }
}
