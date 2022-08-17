using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public class Player
    {
        private string m_PlayerName;
        private bool m_isComputer;
        private int m_AmountOfPairsFound = 0;

        public Player(bool i_IsComputer, string i_Name)
        {
            m_PlayerName = i_Name;
            m_isComputer = i_IsComputer;
        }

        public string GetName()
        {
            return m_PlayerName;
        }

        public int GetScore()
        {
            return m_AmountOfPairsFound;
        }
    }
}
