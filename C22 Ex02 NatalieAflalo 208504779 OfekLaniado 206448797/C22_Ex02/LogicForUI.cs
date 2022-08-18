using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public class LogicForUI
    {
        public static bool isLegalSizeOfMatrix(string i_CharRows, string i_CharColumns, ref int o_NumberOfRows, ref int o_NumberOfColumns, out eValidationOption o_ValidationCode)
        {
            bool isLegal = false;

            if (int.TryParse(i_CharRows, out o_NumberOfRows) && int.TryParse(i_CharColumns, out o_NumberOfColumns))
            {
                if(o_NumberOfRows * o_NumberOfColumns >= 16)
                {
                    if(o_NumberOfRows * o_NumberOfColumns <= 36)
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
                        o_ValidationCode = eValidationOption.BoardTooBig;
                    }
                }
                else
                {
                    o_ValidationCode = eValidationOption.OddNumber;
                }
            }
            else
            {
                o_ValidationCode = eValidationOption.NotANumber;
            }

            return isLegal;
        }

        public static bool isValidBlockID(string i_PlayerInput, int i_NumOfRows, int i_NumOfColumns)
        {
            bool isValidBlock = false;

            if(i_PlayerInput.Length==2)
            {
                if(i_PlayerInput[0] < 'A' || i_PlayerInput[0] > 'A' + i_NumOfColumns)
                {
                    if(i_PlayerInput[1] < '1' || i_PlayerInput[1] > '1' + i_NumOfRows)
                    {
                        isValidBlock = true;
                    }
                }
            }

            return isValidBlock;
        }

        public static void ComputerTurn()
        {
            // randomly choose valid block and check that it's not chosen already
        }
    }
}
