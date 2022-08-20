using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public static class LogicForUI
    {
        private static Player s_FirstPlayer;
        private static Player s_SecondPlayer;
        private static char[,] m_AIMatrix;
        private static int m_NumOfRows;
        private static int m_NumOfColumns;


        public static void CreatePlayers(string i_FirstName, string i_SecondName)
        {
            s_FirstPlayer = new Player(i_FirstName);
            s_SecondPlayer = new Player(i_SecondName);
        }

        public static void InitiateAIMatrix(int i_NumOfRows, int i_NumOfColumns)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfColumns = i_NumOfColumns;
            m_AIMatrix = new char[i_NumOfRows, i_NumOfColumns];
        }

        public static Player GetFirstPlayer()
        {
            return s_FirstPlayer;
        }

        public static Player GetSecondPlayer()
        {
            return s_SecondPlayer;
        }

        public static void UpdateAIMatrix(int i_BlockID, char i_ValueInMatrix)
        {
            m_AIMatrix[i_BlockID / 10, i_BlockID % 10] = i_ValueInMatrix;
        }

        public static bool FindAIMatch(ref int o_FirstRowIndex, ref int o_FirstColumnIndex, ref int o_SecondRowIndex, ref int o_SecondColumnIndex)
        {
            int[] countDuplicates = new int[m_NumOfRows * m_NumOfColumns / 2];

            for(int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColumns; j++)
                {
                    if(m_AIMatrix[i, j] != '\0')
                    {
                        countDuplicates[m_AIMatrix[i, j] - 'A']++;
                    }
                }
            }

            for(int k = 0; k < m_NumOfRows * m_NumOfColumns / 2; k++)
            {
                if(countDuplicates[k]==2)
                {
                    o_FirstColumnIndex
                }
            }
        }

        public static bool isLegalSizeOfMatrix(char i_CharRows, char i_CharColumns, ref int o_NumberOfRows, ref int o_NumberOfColumns, out eValidationOption o_ValidationCode)
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

        public static bool ComputerTurn(ref MemoryGameBoard i_GameBoard)
        {
            Random randomIndexNumber = new Random();
            int randomRow;
            int randomColumn;
            List<int> flippedBlockID = new List<int>();
            int numOfFlips = 0;
            int numOfRows = i_GameBoard.GetNumberOfRows();
            int numOfColumns = i_GameBoard.GetNumberOfColumns();

            do
            {
                randomRow = randomIndexNumber.Next(1, numOfRows);
                randomColumn = randomIndexNumber.Next(1, numOfColumns);
                if(IsAnUnflippedBlock(ref i_GameBoard, randomRow * 10 + randomColumn))
                {
                    flippedBlockID.Add(randomRow * 10 + randomColumn);
                    i_GameBoard.FlipOrUnflipBlock(flippedBlockID[numOfFlips], true);
                    UI.PrintMatrix(numOfRows, numOfColumns);
                    System.Threading.Thread.Sleep(2000);
                    numOfFlips++;
                }
            }
            while (numOfFlips < 2);

            if(!IsGoodPair(i_GameBoard, flippedBlockID[0], flippedBlockID[1]))
            {
                i_GameBoard.FlipOrUnflipBlock(flippedBlockID[0], false);
                i_GameBoard.FlipOrUnflipBlock(flippedBlockID[1], false);
                UI.PrintMatrix(numOfRows, numOfColumns);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsGoodPair(MemoryGameBoard i_GameBoard, int i_FirstBlockID, int i_SecondBlockID)
        {
            return i_GameBoard.GetMatrixGameBoard()[i_FirstBlockID / 10, i_FirstBlockID % 10] == i_GameBoard.GetMatrixGameBoard()[i_SecondBlockID / 10, i_SecondBlockID % 10];
        }

        public static bool IsAnUnflippedBlock(ref MemoryGameBoard i_GameBoard, int i_BlockID)
        {
            return !i_GameBoard.GetMatrixFlippedBlocks()[i_BlockID / 10, i_BlockID % 10];
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
