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
        private int m_AmountOfPairsFound = 0;

        public Player(string i_Name)
        {
            m_PlayerName = i_Name;
        }

        public string GetName()
        {
            return m_PlayerName;
        }

        public int GetScore()
        {
            return m_AmountOfPairsFound;
        }

        public void UpdateScore()
        {
            m_AmountOfPairsFound++;
        }
    }
}
