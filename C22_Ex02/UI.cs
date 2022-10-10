using System;
using System.Collections.Generic;
using System.Text;

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
                    LogicForUI.InitiateAIDictionary();
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
            PrintMatrix(numOfRows, numOfColumns);
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
                        if (!LogicForUI.ComputerTurn(ref s_Game))
                        {
                            isFirstPlayerTurn = true;
                        }
                        else
                        {
                            LogicForUI.GetSecondPlayer().UpdateScore();
                        }
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

                if(s_Game.GetIsAllBlocksFlipped())
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    doneGame(ref finishGame);
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
                Console.WriteLine("Would you like to play against the computer or another player?{0}Enter C for Computer or P for Player", Environment.NewLine);
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
            while (computerOrPlayer != "P" && computerOrPlayer != "C");

            return isPlayingAgainsComputer;
        }

        private static void boardSizeCheck(ref int io_NumOfRows, ref int io_NumOfColumns)
        {
            bool isValidBoard = false;
            eValidationOption validationCode = eValidationOption.Undefined;
            string boardSizeString;

            do
            {
                if(validationCode != eValidationOption.Undefined)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    printValidationMessage(validationCode);
                }

                Console.WriteLine("The board game size must be between 4X4 and 6X6.{0}Enter board size in (rows)X(columns) format (example: 4X5):", Environment.NewLine);
                boardSizeString = Console.ReadLine();
                if (boardSizeString.Length != 3 || boardSizeString[1] != 'X')
                {
                    validationCode = eValidationOption.NotInSizeFormat;
                }
                else
                {
                    isValidBoard = LogicForUI.IsLegalSizeOfMatrix(boardSizeString[0], boardSizeString[2], ref io_NumOfRows, ref io_NumOfColumns, out validationCode);
                }
            }
            while (!isValidBoard);
        }

        private static void printValidationMessage(eValidationOption i_ValidationCode)
        {
            switch(i_ValidationCode)
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
            int flipTurnNumber = 0;
            List<int> playedBlockID = new List<int>();
            eValidationOption validationCode = eValidationOption.Undefined;

            do
            {
                Console.WriteLine("{0} Turn!{1}Please choose block number {2} to flip.{1}If you want to finish the game enter q or Q{1}If you want to keep playing, enter block ID in (Column Upper Case Letter)(Row Number) format (Example- A2):", i_PlayerName, Environment.NewLine, flipTurnNumber + 1);
                playerInput = Console.ReadLine();
                if (playerInput == "Q" || playerInput == "q")
                {
                    o_FinishGame = true;
                }
                else
                {
                    if (isValidBlockID(playerInput, i_NumOfRows, i_NumOfColumns, out validationCode))
                    {
                        playedBlockID.Add(convertValidBlockIDToInt(playerInput));
                        s_Game.FlipOrUnflipBlock(playedBlockID[flipTurnNumber], true);
                        flipTurnNumber++;
                        PrintMatrix(i_NumOfRows, i_NumOfColumns);
                        if (flipTurnNumber == 2)
                        {
                            if(!LogicForUI.IsGoodPair(s_Game, playedBlockID[0], playedBlockID[1]))
                            {
                                s_Game.FlipOrUnflipBlock(playedBlockID[0], false);
                                s_Game.FlipOrUnflipBlock(playedBlockID[1], false);
                                if(s_IsPlayingAgainstComputer)
                                {
                                    LogicForUI.UpdateAIDictionary(playedBlockID[0], s_Game.GetMatrixGameBoard()[playedBlockID[0] / 10, playedBlockID[0] % 10]);
                                    LogicForUI.UpdateAIDictionary(playedBlockID[1], s_Game.GetMatrixGameBoard()[playedBlockID[1] / 10, playedBlockID[1] % 10]);
                                }
                                
                                System.Threading.Thread.Sleep(2000);
                                PrintMatrix(i_NumOfRows, i_NumOfColumns);

                                return false;
                            }

                            if (s_IsPlayingAgainstComputer)
                            {
                                LogicForUI.ClearFlippedPairFromAIMatrix(playedBlockID[0], playedBlockID[1]);
                            }

                            return true;
                        }
                    }
                    else
                    {
                        PrintMatrix(i_NumOfRows, i_NumOfColumns);
                        printValidationMessage(validationCode);
                    }
                }
            }
            while (!o_FinishGame && flipTurnNumber < 3);

            return false;
        }

        private static bool isValidBlockID(string i_PlayerInput, int i_NumOfRows, int i_NumOfColumns, out eValidationOption o_ValidationCode)
        {
            bool isValidBlock = false;
            int blockID;

            if (i_PlayerInput.Length == 2)
            {
                if (i_PlayerInput[0] >= 'A' && i_PlayerInput[0] < 'A' + i_NumOfColumns)
                {
                    if (i_PlayerInput[1] >= '1' && i_PlayerInput[1] < '1' + i_NumOfRows)
                    {
                        blockID = convertValidBlockIDToInt(i_PlayerInput);
                        if (LogicForUI.IsAnUnflippedBlock(ref s_Game, blockID))
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

            strToConvert = string.Format("{0}{1}", i_StringBlockID[1], (i_StringBlockID[0] - 'A').ToString());
            if (int.TryParse(strToConvert, out blockIDNumber))
            {
                return blockIDNumber - 10;
            }

            return -1;
        }

        private static void doneGame(ref bool o_FinishGame)
        {
            Console.WriteLine("Game Over!{0}The result is: {1}If you want to finish the game enter q,Q,n or N. If you want to play a new game enter Y or y:", Environment.NewLine, LogicForUI.GetGameResult());
            string playerInput = Console.ReadLine();
            string validInputOptions = "YyNnQq";
            
            while(playerInput.Length != 1 || !validInputOptions.Contains(playerInput))
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("The input is not valid. Enter Y or y to start a new game, enter N or n to finish the game:");
                playerInput = Console.ReadLine();
            }

            if (playerInput == "Y" || playerInput == "y")
            {
                s_IsFirstGame = false;
                if (s_IsPlayingAgainstComputer)
                {
                    LogicForUI.InitiateAIDictionary();
                }

                PlayGame();
            }
            
            o_FinishGame = true;
        }

        public static void PrintMatrix(int i_NumberOfRows, int i_NumberOfColumns)
        {
            StringBuilder columnLettersRow = new StringBuilder(" ");
            int rowSeparetorLength = (i_NumberOfColumns * 4) + 1;
            string rowSeparetor = string.Format("  {0}", new string('=', rowSeparetorLength));

            Ex02.ConsoleUtils.Screen.Clear();

            for (int i = 0; i < i_NumberOfColumns; i++)
            {
                columnLettersRow.Append(string.Format("   {0}", (char)(i + 'A')));
            }

            Console.WriteLine(columnLettersRow.ToString());
            Console.WriteLine(rowSeparetor);

            for (int i = 0; i < i_NumberOfRows; i++)
            {
                string rowDigit = string.Format("{0} |", i + 1);

                Console.Write(rowDigit);

                for (int j = 0; j < i_NumberOfColumns; j++)
                {
                    bool isFlippedBlock = s_Game.GetMatrixFlippedBlocks()[i, j];
                    string blockToProint = string.Format(" {0} |", isFlippedBlock ? s_Game.GetMatrixGameBoard()[i, j] : ' ');

                    Console.Write(blockToProint);
                }

                Console.WriteLine();
                Console.WriteLine(rowSeparetor);
            }

            Console.WriteLine();
        }
    }
}
