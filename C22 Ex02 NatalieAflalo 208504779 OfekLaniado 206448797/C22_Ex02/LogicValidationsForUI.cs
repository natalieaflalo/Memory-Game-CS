using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public class LogicValidationsForUI
    {
        public static bool isLegalSizeOfMatrix(string i_StringNumber, ref int io_ValidNumber)
        {
            bool isLegal = false;

            if (int.TryParse(i_StringNumber, out io_ValidNumber) && io_ValidNumber >= 0)
            {
                isLegal = true;
            }


            return isLegal;
        }
    }
}
