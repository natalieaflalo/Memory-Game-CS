using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public static class UI
    {
        private static MemoryGameBoard s_Game;
        private static bool s_IsFirstGame = true;
        private static bool s_IsPlayingAgainstComputer;

        public static void PlayGame()
        {
            int numOfRows = 0;
            int numOfColumns = 0;

            if(s_IsFirstGame)
            {
                Console.WriteLine("Welcome to Memory Game! {0}Enter your name:", Environment.NewLine);
                string firstPlayerName = Console.ReadLine();
                s_IsPlayingAgainstComputer = playingAgainstComputer();

                if (s_IsPlayingAgainstComputer)
                {
                    LogicForUI.CreatePlayers(firstPlayerName, "Computer");
                }
                else
                {
                    Console.WriteLine("Enter second player name:");
                    string secondPlayerName = Console.ReadLine();

                    LogicForUI.CreatePlayers(firstPlayerName, secondPlayerName);
                }
            }

            boardSizeCheck(ref numOfRows, ref numOfColumns);
            s_Game = new MemoryGameBoard(numOfRows, numOfColumns);
            bool isFirstPlayerTurn = true;
            bool finishGame = false;

            do
            {
                if(isFirstPlayerTurn)
                {
                    if(playerTurn(LogicForUI.GetFirstPlayer().GetName(), numOfRows, numOfColumns, ref finishGame))
                    {
                        LogicForUI.GetFirstPlayer().UpdateScore();
                    }
                    else
                    {
                        isFirstPlayerTurn = false;
                    }
                }
                else
                {
                    if(s_IsPlayingAgainstComputer)
                    {
                        LogicForUI.ComputerTurn();
                    }
                    else
                    {
                        if(playerTurn(LogicForUI.GetSecondPlayer().GetName(), numOfRows, numOfColumns, ref finishGame))
                        {
                            LogicForUI.GetSecondPlayer().UpdateScore();
                        }
                        else
                        {
                            isFirstPlayerTurn = true;
                        }
                    }
                }
            }
            while (!finishGame);
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
                    printValidationMessage(validationCode);
                }
                Console.WriteLine("The board game size must be between 4X4 and 6X6.{0}Enter board size in (rows)X(columns) format (example: 4X5):");
                boardSizeString = Console.ReadLine();
                if(boardSizeString.Length !=3 || boardSizeString[1] != 'X')
                {
                    validationCode = eValidationOption.NotInSizeFormat;
                }
                else
                {
                    isValidBoard = LogicForUI.isLegalSizeOfMatrix(boardSizeString[0], boardSizeString[2], ref o_NumOfRows, ref o_NumOfColumns, out validationCode);
                }
            }
            while (!isValidBoard);
        }

        private static void printValidationMessage(eValidationOption validationCode)
        {
            switch(validationCode)
            {
                case eValidationOption.NotANumber:
                    Console.WriteLine("The input is not a number.{0}", Environment.NewLine);
                    break;
                case eValidationOption.OddNumber:
                    Console.WriteLine("The Ininputput is not an even size.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BoardTooBig:
                    Console.WriteLine("The input is larger than 6X6.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BoardTooSmall:
                    Console.WriteLine("The input is smaller than 4X4.{0}", Environment.NewLine);
                    break;
                case eValidationOption.NotInSizeFormat:
                    Console.WriteLine("The input is not in (rows)X(columns) format.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BadColumnLetter:
                    Console.WriteLine("The input for the column is not an upper case letter column on the board.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BadRowNumber:
                    Console.WriteLine("The input for the row is not a row number on the board.{0}", Environment.NewLine);
                    break;
                case eValidationOption.NotInBlockFormat:
                    Console.WriteLine("The input is not in (Column Upper Case Letter)(Row Number) format.{0}", Environment.NewLine);
                    break;
                case eValidationOption.BlockAlreadyFlipped:
                    Console.WriteLine("The block is already flipped, Choose a non flipped block.{0}", Environment.NewLine);
                    break;
            }
        }

        private static bool playerTurn(string i_PlayerName, int i_NumOfRows, int i_NumOfColumns, ref bool o_FinishGame)
        {
            string playerInput;
            bool isFinishTurn = false;
            int flipTurnNumber = 1;
            List<int> playedBlockID = new List<int>();
            eValidationOption validationCode = eValidationOption.Undefined;

            do
            {
                Console.WriteLine("{0} Turn!{1}Please choose card number {2} to flip.{1}If you want to finish the game enter q or Q{1}If you want to keep playing, enter block ID in (Column Upper Case Letter)(Row Number) format (Example- A2):", i_PlayerName, Environment.NewLine, flipTurnNumber);
                playerInput = Console.ReadLine();
                if (playerInput != "Q" || playerInput != "q")
                {
                    o_FinishGame = true;
                }
                else
                {
                    if (isValidBlockID(playerInput, i_NumOfRows, i_NumOfColumns, out validationCode))
                    {
                        playedBlockID.Add(convertValidBlockIDToInt(playerInput));
                        s_Game.FlipOrUnflipBlock(playedBlockID[flipTurnNumber-1], true);
                        if (flipTurnNumber == 2)
                        {
                            isFinishTurn = true;
                            if (LogicForUI.IsGoodPair(s_Game, playedBlockID[0], playedBlockID[1]))
                            {
                                if(s_Game.GetIsAllBlocksFlipped())
                                {

                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            flipTurnNumber++;
                        }
                    }
                    else
                    {
                        printValidationMessage(validationCode);
                    }
                }
            }
            while (!o_FinishGame || !isFinishTurn);

            return false;
        }

        private static bool isValidBlockID(string i_PlayerInput, int i_NumOfRows, int i_NumOfColumns, out eValidationOption o_ValidationCode)
        {
            bool isValidBlock = false;
            int blockID;

            if (i_PlayerInput.Length == 2)
            {
                if (i_PlayerInput[0] < 'A' || i_PlayerInput[0] > 'A' + i_NumOfColumns)
                {
                    if (i_PlayerInput[1] < '1' || i_PlayerInput[1] > '1' + i_NumOfRows)
                    {
                        blockID = convertValidBlockIDToInt(i_PlayerInput);
                        if (LogicForUI.IsAnUnflippedBlock(s_Game, blockID))
                        {
                            isValidBlock = true;
                            o_ValidationCode = eValidationOption.Valid;
                        }
                        else
                        {
                            o_ValidationCode = eValidationOption.BlockAlreadyFlipped;
                        }
                    }
                    else
                    {
                        o_ValidationCode = eValidationOption.BadRowNumber;
                    }
                }
                else
                {
                    o_ValidationCode = eValidationOption.BadColumnLetter;
                }
            }
            else
            {
                o_ValidationCode = eValidationOption.NotInBlockFormat;
            }

            return isValidBlock;
        }

        private static int convertValidBlockIDToInt(string i_StringBlockID)
        {
            int blockIDNumber;
            string strToConvert;

            strToConvert = string.Format("{0}{1}", (i_StringBlockID[0] - 'A').ToString(), i_StringBlockID[2]);
            if (int.TryParse(i_StringBlockID, out blockIDNumber))
            {
                return blockIDNumber;
            }

            return 0;
        }

        private static void doneGame()
        {
            Console.WriteLine("Game Over!{0}The result is: {1} If you want to finish the game enter q or Q:", Environment.NewLine, LogicForUI.GetGameResult());
            string playerInput = Console.ReadLine();
        }
    }
}
