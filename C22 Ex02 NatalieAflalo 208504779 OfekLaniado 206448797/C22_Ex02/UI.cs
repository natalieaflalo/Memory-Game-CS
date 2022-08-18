using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public static class UI
    {
        private static MemoryGameBoard game;
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

            game = new MemoryGameBoard(numOfRows, numOfColumns);
            string playerInput;

            //ask if playing with computer or player
            //ask for name\s
            do
            {
                playerInput = Console.ReadLine();
                playerTurn(playerInput);

            }
            while (playerInput != "Q" || playerInput != "q");
            //ask for block ID
            //check isValidBlockID and flip it
            //ask again and check again
        }

        private static void playerTurn(string i_PlayerInput)
        {
            bool isValidTurn = false;
            if(i_PlayerInput != "Q" || i_PlayerInput != "q")
            {
                Console.WriteLine("Game finished.");
            }
            else
            {
                isValidTurn = LogicForUI.isValidBlockID(i_PlayerInput);
                if(isValidTurn)
                {
                    Console.WriteLine("Next player turn.");
                }
            }
            //  ask for block ID
            //  check isValidBlockID and flip it
            //  ask again and check again
            

        }

        private static void computerTurn()
        {
            // randomly choose valid block and check that it's not chosen already
        }


        //private static bool isValidBlockID()


        /// <summary>
        /// private static void QuitGame() 
        ///{
        ///if (userGuessInput.Equals("Q") || userGuessInput.Equals("q"))
        ///{
        ///game.QuitGame();
        ///break;
        ///}
        ///}
        /// </summary>


    }
}
