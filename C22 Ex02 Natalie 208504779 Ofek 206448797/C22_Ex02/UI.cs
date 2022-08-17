using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C22_Ex02
{
    public static class UI
    {
        public static void StartGame()
        {

        }

        

        private static void QuitGame() 
        {
            if (userGuessInput.Equals("Q") || userGuessInput.Equals("q"))
            {
                game.QuitGame();
                break;
            }
        }

    }
}
