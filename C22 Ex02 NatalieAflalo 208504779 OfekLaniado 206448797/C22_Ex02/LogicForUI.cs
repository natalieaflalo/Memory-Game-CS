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

        public static void CreatePlayers(string i_FirstName, string i_SecondName)
        {
            s_FirstPlayer = new Player(i_FirstName);
            s_SecondPlayer = new Player(i_SecondName);
        }

        public static Player GetFirstPlayer()
        {
            return s_FirstPlayer;
        }

        public static Player GetSecondPlayer()
        {
            return s_SecondPlayer;
        }

        public static bool isLegalSizeOfMatrix(char i_CharRows, char i_CharColumns, ref int o_NumberOfRows, ref int o_NumberOfColumns, out eValidationOption o_ValidationCode)
        {
            bool isLegal = false;

            if (int.TryParse(i_CharRows.ToString(), out o_NumberOfRows) && int.TryParse(i_CharColumns.ToString(), out o_NumberOfColumns))
            {
                if(o_NumberOfRows > 6 || o_NumberOfColumns > 6)
                {
                    if(o_NumberOfRows < 4 || o_NumberOfColumns < 4)
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

        public static void ComputerTurn()
        {
            // randomly choose valid block and check that it's not chosen already
        }

        public static bool IsGoodPair(MemoryGameBoard i_GameBoard, int i_FirstBlockID, int i_SecondBlockID)
        {
            return i_GameBoard.GetMatrixGameBoard()[i_FirstBlockID / 10, i_FirstBlockID % 10] == i_GameBoard.GetMatrixGameBoard()[i_SecondBlockID / 10, i_SecondBlockID % 10];
        }

        public static bool IsAnUnflippedBlock(MemoryGameBoard i_GameBoard, int i_BlockID)
        {
            return !i_GameBoard.GetMatrixFlippedBlocks()[i_BlockID / 10, i_BlockID % 10];
        }

        public static string GetGameResult()
        {
            if(s_FirstPlayer.GetScore() > s_SecondPlayer.GetScore())
            {
                return string.Format("{0} wins", s_FirstPlayer.GetName());
            }
            if (s_FirstPlayer.GetScore() < s_SecondPlayer.GetScore())
            {
                return string.Format("{0} wins", s_SecondPlayer.GetName());
            }

            return "Tie";
        }
    }
}
