using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public class MemoryGameBoard
    {
        private int m_NumOfColumns;
        private int m_NumOfRows;
        private char[,] m_MatrixGameBoard;
        private int[] m_RandomLettersCounter;

        public MemoryGameBoard()
        {
            m_MatrixGameBoard = new char[m_NumOfColumns,m_NumOfRows];
            m_RandomLettersCounter = new int[m_NumOfColumns * m_NumOfRows / 2];
            createRandomMatrix();
        }

        private void createRandomMatrix()
        {
            int counterOfCorrectValuesInMatrix = 0;
            Random rndCharForMatrix = new Random();
            char randomChar;

            for (int currentColumn = 0; currentColumn < m_NumOfColumns ; currentColumn++)
            {
                for (int currentRow = 0; currentRow < m_NumOfRows ; currentRow++)
                {
                    while (m_MatrixGameBoard[currentColumn, currentRow] == '\0')
                    {
                        randomChar = (char)rndCharForMatrix.Next(65, 65 + (m_NumOfColumns * m_NumOfRows / 2));

                        if (m_RandomLettersCounter[(int)randomChar] < 2)
                        {
                            m_RandomLettersCounter[(int)randomChar]++;
                            m_MatrixGameBoard[currentColumn, currentRow] = randomChar;
                            counterOfCorrectValuesInMatrix++;
                        }
                    }
                }
            }
        }
    }
}
