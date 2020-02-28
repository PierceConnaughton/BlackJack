using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public enum CardNumber
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 11
    }

    public enum CardSuit
    {
        Hearts = 1,
        Clubs = 2,
        Spades = 3,
        Diamond = 4
    }
    class Card
    {
        CardNumber cardNumber;
        CardSuit cardSuit;

        public CardNumber cardNumberVar
        {
            get
            {
                return this.cardNumber;
            }
            set
            {
                this.cardNumber = value;
            }
        }

        public CardNumber cardSuitVar
        {
            get
            {
                return this.cardSuitVar;
            }
            set
            {
                this.cardSuitVar = value;
            }
        }

        public Card()
        {
            cardSuit = 0;
            cardNumber = 0;
        }

        public override string ToString()
        {
            return string.Format(cardSuit.ToString());
        }
    }
}
