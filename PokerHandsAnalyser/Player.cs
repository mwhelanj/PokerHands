using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandsAnalyser
{
    public class Player
    {
        public Player() 
        {
            Score = 0;
        }

        public Hand Hand { get; set; }

        public int Score { get; set; }

    }
}
