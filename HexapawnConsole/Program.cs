using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HexapawnLogic;

namespace HexapawnConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HexaConsole play = new HexaConsole(4, 4);
            //play.RunQLearning(Logic.Piece.PLAYER1, Logic.Piece.PLAYER2);

            play.testRunQ(false, false);
            //Console.Read();
        }



    }
}
