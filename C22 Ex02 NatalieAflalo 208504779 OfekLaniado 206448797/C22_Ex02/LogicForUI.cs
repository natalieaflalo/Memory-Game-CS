using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public class LogicForUI
    {
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
            if(i_GameBoard.GetMatrixGameBoard()[i_FirstBlockID/10,i_FirstBlockID%10] == i_GameBoard.GetMatrixGameBoard()[i_SecondBlockID / 10, i_SecondBlockID % 10])
            {
                return true;
            }

            return false;
        }

        public static bool IsAnUnflippedBlock(MemoryGameBoard i_GameBoard, int i_BlockID)
        {
            if(i_GameBoard.GetMatrixFlippedBlocks()[i_BlockID/10,i_BlockID%10])
            {
                return false;
            }
            return true;
        }
    }
}
