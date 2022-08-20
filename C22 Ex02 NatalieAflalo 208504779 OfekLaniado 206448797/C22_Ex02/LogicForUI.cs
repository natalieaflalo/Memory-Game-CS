using System;
using System.Collections.Generic;
using System.Text;

namespace C22_Ex02
{
    public static class LogicForUI
    {
        private static Player s_FirstPlayer;
        private static Player s_SecondPlayer;
        private static Dictionary<int, char> s_AIMemoryDict;

        public static void CreatePlayers(string i_FirstName, string i_SecondName)
        {
            s_FirstPlayer = new Player(i_FirstName);
            s_SecondPlayer = new Player(i_SecondName);
        }

        public static void InitiateAIDictionary()
        {
            s_AIMemoryDict = new Dictionary<int, char>();
        }

        public static Player GetFirstPlayer()
        {
            return s_FirstPlayer;
        }

        public static Player GetSecondPlayer()
        {
            return s_SecondPlayer;
        }

        public static void UpdateAIDictionary(int i_BlockID, char i_ValueInMatrix)
        {
            if(!s_AIMemoryDict.ContainsKey(i_BlockID))
            {
                s_AIMemoryDict.Add(i_BlockID, i_ValueInMatrix);
            }
        }

        public static void ClearFlippedPairFromAIMatrix(int i_FirstBlockID, int i_SecondBlockID)
        {
            if(s_AIMemoryDict.ContainsKey(i_FirstBlockID))
            {
                s_AIMemoryDict.Remove(i_FirstBlockID);
            }

            if(s_AIMemoryDict.ContainsKey(i_SecondBlockID))
            {
                s_AIMemoryDict.Remove(i_SecondBlockID);
            }
        }

        public static bool IsFindAIPair(out int o_FirstBlockID, out int o_SecondBlockID)
        {
            foreach (KeyValuePair<int, char> firstBlockFromMemory in s_AIMemoryDict)
            {
                foreach (KeyValuePair<int, char> secondBlockFromMemory in s_AIMemoryDict)
                {
                    if (!firstBlockFromMemory.Key.Equals(secondBlockFromMemory.Key))
                    {
                        if (firstBlockFromMemory.Value == secondBlockFromMemory.Value)
                        {
                            o_FirstBlockID = firstBlockFromMemory.Key;
                            o_SecondBlockID = secondBlockFromMemory.Key;

                            return true;
                        }
                    }
                }
            }

            o_FirstBlockID = -1;
            o_SecondBlockID = -1;

            return false;
        }

        public static bool IsLegalSizeOfMatrix(char i_CharRows, char i_CharColumns, ref int o_NumberOfRows, ref int o_NumberOfColumns, out eValidationOption o_ValidationCode)
        {
            bool isLegal = false;

            if (int.TryParse(i_CharRows.ToString(), out o_NumberOfRows) && int.TryParse(i_CharColumns.ToString(), out o_NumberOfColumns))
            {
                if(o_NumberOfRows <= 6 && o_NumberOfColumns <= 6)
                {
                    if(o_NumberOfRows >= 4 && o_NumberOfColumns >= 4)
                    {
                        if((o_NumberOfRows * o_NumberOfColumns) % 2 == 0)
                        {
                            isLegal = true;
                            o_ValidationCode = eValidationOption.Valid;
                        }
                        else
                        {
                            o_ValidationCode = eValidationOption.OddNumber;
                        }
                    }
                    else
                    {
                        o_ValidationCode = eValidationOption.BoardTooSmall;
                    }
                }
                else
                {
                    o_ValidationCode = eValidationOption.BoardTooBig;
                }
            }
            else
            {
                o_ValidationCode = eValidationOption.NotANumber;
            }

            return isLegal;
        }

        public static bool ComputerTurn(ref MemoryGameBoard io_GameBoard)
        {
            Random randomIndexNumber = new Random();
            int randomRow;
            int randomColumn;
            int firstAIPairBlockID;
            int secondAIPairBlockID;
            List<int> flippedBlockID = new List<int>();
            int numOfFlips = 0;
            int numOfRows = io_GameBoard.GetNumberOfRows();
            int numOfColumns = io_GameBoard.GetNumberOfColumns();

            do
            {
                if(!IsFindAIPair(out firstAIPairBlockID, out secondAIPairBlockID))
                {
                    randomRow = randomIndexNumber.Next(numOfRows);
                    randomColumn = randomIndexNumber.Next(numOfColumns);
                    if (IsAnUnflippedBlock(ref io_GameBoard, (randomRow * 10) + randomColumn))
                    {
                        flippedBlockID.Add((randomRow * 10) + randomColumn);
                        io_GameBoard.FlipOrUnflipBlock(flippedBlockID[numOfFlips], true);
                        UI.PrintMatrix(numOfRows, numOfColumns);
                        System.Threading.Thread.Sleep(2000);
                        UpdateAIDictionary(flippedBlockID[0], io_GameBoard.GetMatrixGameBoard()[randomRow, randomColumn]);
                        numOfFlips++;
                    }
                }
                else
                {
                    flippedBlockID.Clear();
                    flippedBlockID.Add(firstAIPairBlockID);
                    flippedBlockID.Add(secondAIPairBlockID);
                    System.Threading.Thread.Sleep(2000);
                    io_GameBoard.FlipOrUnflipBlock(flippedBlockID[0], true);
                    UI.PrintMatrix(numOfRows, numOfColumns);
                    System.Threading.Thread.Sleep(2000);
                    io_GameBoard.FlipOrUnflipBlock(flippedBlockID[1], true);
                    UI.PrintMatrix(numOfRows, numOfColumns);
                    System.Threading.Thread.Sleep(2000);
                    numOfFlips = 2;
                }
            }
            while (numOfFlips < 2);

            if(!IsGoodPair(io_GameBoard, flippedBlockID[0], flippedBlockID[1]))
            {
                io_GameBoard.FlipOrUnflipBlock(flippedBlockID[0], false);
                io_GameBoard.FlipOrUnflipBlock(flippedBlockID[1], false);
                UI.PrintMatrix(numOfRows, numOfColumns);

                return false;
            }
            else
            {
                ClearFlippedPairFromAIMatrix(flippedBlockID[0], flippedBlockID[1]);

                return true;
            }
        }

        public static bool IsGoodPair(MemoryGameBoard i_GameBoard, int i_FirstBlockID, int i_SecondBlockID)
        {
            return i_GameBoard.GetMatrixGameBoard()[i_FirstBlockID / 10, i_FirstBlockID % 10] == i_GameBoard.GetMatrixGameBoard()[i_SecondBlockID / 10, i_SecondBlockID % 10];
        }

        public static bool IsAnUnflippedBlock(ref MemoryGameBoard io_GameBoard, int i_BlockID)
        {
            return !io_GameBoard.GetMatrixFlippedBlocks()[i_BlockID / 10, i_BlockID % 10];
        }

        public static StringBuilder GetGameResult()
        {
            StringBuilder resultOutput = new StringBuilder();
            string firstPlayerName = s_FirstPlayer.GetName();
            string secondPlayerName = s_SecondPlayer.GetName();
            int firstPlayerScore = s_FirstPlayer.GetScore();
            int secondPlayerScore = s_SecondPlayer.GetScore();

            if (firstPlayerScore > secondPlayerScore)
            {
                resultOutput.Append(string.Format("{0} wins! {1}", firstPlayerName, Environment.NewLine));
            }
            else if (firstPlayerScore < secondPlayerScore)
            {
                resultOutput.Append(string.Format("{0} wins! {1}", secondPlayerName, Environment.NewLine));
            }
            else
            {
                resultOutput.Append(string.Format("Tie! {0}", Environment.NewLine));
            }

            resultOutput.Append(string.Format("The scores are: {0}- {1}, {2}- {3}{4}", firstPlayerName, firstPlayerScore, secondPlayerName, secondPlayerScore, Environment.NewLine));

            return resultOutput;
        }
    }
}
