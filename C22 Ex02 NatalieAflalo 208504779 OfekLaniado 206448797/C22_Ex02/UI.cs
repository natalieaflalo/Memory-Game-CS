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

        public static void PlayGame()
        {
            int numOfRows = 0;
            int numOfColumns = 0;
            Player firstPlayer;
            Player secondPlayer;

            Console.WriteLine("Welcome to Memory Game! {0}Enter your name:", Environment.NewLine);
            string firstPlayerName = Console.ReadLine();
            bool isPlayingAgainstComputer = playingAgainstComputer();

            if (isPlayingAgainstComputer)
            {
                firstPlayer = new Player(firstPlayerName);
                secondPlayer = new Player("Computer");
            }
            else
            {
                firstPlayer = new Player(firstPlayerName);
                Console.WriteLine("Enter second player name:");
                string secondPlayerName = Console.ReadLine();
                secondPlayer = new Player(secondPlayerName);
            }

            boardSizeCheck(ref numOfRows, ref numOfColumns);
            game = new MemoryGameBoard(numOfRows, numOfColumns);
            bool isFirstPlayerTurn = true;
            bool finishGame = false;

            do
            {
                if(isFirstPlayerTurn)
                {
                    playerTurn(firstPlayer.GetName(), ref finishGame);
                }
                else
                {
                    if(isPlayingAgainstComputer)
                    {
                        LogicForUI.ComputerTurn();
                    }
                    else
                    {
                        playerTurn(secondPlayer.GetName(), ref finishGame);
                    }
                }
            }
            while (!finishGame);
            //ask for block ID
            //check isValidBlockID and flip it
            //ask again and check again
        }

        private static bool playingAgainstComputer()
        {
            string computerOrPlayer;
            bool isPlayingAgainsComputer = true;
            do
            {
                Console.WriteLine("Would you like to play against the computer or another player?{0}Enter C for Computer or P for Player");
                computerOrPlayer = Console.ReadLine();
                if (computerOrPlayer == "P")
                {
                    isPlayingAgainsComputer = false;
                }
                else if (computerOrPlayer == "C")
                {
                    isPlayingAgainsComputer = true;
                }
                else
                {
                    Console.WriteLine("Input was not P or C.{0}", Environment.NewLine);
                }
            }
            while (computerOrPlayer != "P" || computerOrPlayer != "C");
            return isPlayingAgainsComputer;
        }

        private static void boardSizeCheck(ref int o_NumOfRows, ref int o_NumOfColumns)
        {
            bool isValidBoard = false;
            eValidationOption validationCode = eValidationOption.Undefined;
            string boardSizeString;

            do
            {
                if(validationCode != eValidationOption.Undefined)
                {
                    printBoardSizeValidationMessage(validationCode);
                }
                Console.WriteLine("The board game size must be between 4X4 and 6X6.{0}Enter board size in (rows)X(columns) format (example: 4X5):");
                boardSizeString = Console.ReadLine();
                if(boardSizeString.Length !=3 || boardSizeString[1] != 'X')
                {
                    validationCode = eValidationOption.NotInFormat;
                }
                else
                {
                    isValidBoard = LogicForUI.isLegalSizeOfMatrix(boardSizeString, boardSizeString, ref o_NumOfRows, ref o_NumOfColumns, out validationCode);
                }
            }
            while (!isValidBoard);
        }

        private static void printBoardSizeValidationMessage(eValidationOption validationCode)
        {
            switch(validationCode)
            {
                case eValidationOption.NotANumber:
                    Console.WriteLine("The Input is not a number.{0}", Environment.NewLine);
                    break;
                case eValidationOption.OddNumber:
                    Console.WriteLine("The Input is not an even size.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BoardTooBig:
                    Console.WriteLine("The Input is larger than 6X6.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BoardTooSmall:
                    Console.WriteLine("The Input is smaller than 4X4.{0}", Environment.NewLine);
                    break;
                case eValidationOption.NotInFormat:
                    Console.WriteLine("The Input is not in (rows)X(columns) format.{0}", Environment.NewLine);
                    break;
            }
        }

        private static void playerTurn(string i_PlayerName, int i_NumOfRows, int i_NumOfColumns, ref bool o_FinishGame)
        {
            string playerInput;
            bool isValidTurn = false;

            Console.WriteLine("{0} Turn!{1}If you want to finish the game press q or Q{1}If you want to play, enter block ID in (Column Upper Case Letter)(Row Number) format (Example- A2):", i_PlayerName, Environment.NewLine);
            playerInput = Console.ReadLine();
            if (playerInput != "Q" || playerInput != "q")
            {
                o_FinishGame = true;
            }
            else
            {
                isValidTurn = LogicForUI.isValidBlockID(playerInput, i_NumOfRows, i_NumOfColumns);
                if(isValidTurn)
                {
                    Console.WriteLine("Next player turn.");
                }
                else
                {

                }
            }
            //  ask for block ID
            //  check isValidBlockID and flip it
            //  ask again and check again
        }

        private static void printPlayerInputValidationMessage()
        {

        }

        private static bool isValidBlockID()
        {
            bool isBlockID = false;

            return isBlockID;
        }


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
