using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Player
    {
       public string PlayerName { get; set; }

        public int Wins { get; set; }
        
        public int Losses { get; set; }

        public int Draws { get; set; }

        public Player()
        {

        }

        public Player(string playerName, int wins, int losses, int draws)
        {
            PlayerName = playerName;
            Wins = wins;
            Losses = losses;
            Draws = draws;
        }

        public override string ToString()
        {
            
            return String.Format("{0,-35}{1,-14}{2,-14}{3}", PlayerName, Wins,Losses,Draws);
        }

    }
}
