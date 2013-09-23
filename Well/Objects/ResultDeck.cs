using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class ResultDeck : Deck
    {
        public List<SuitEnum> availableSuits;
        public ResultDeck()
            : base()
        {
            SuitEnum[] suits = { SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds };
            availableSuits = new List<SuitEnum>(suits);
        }

        public ResultDeck(string Name)
            : base(Name)
        {
            SuitEnum[] suits = { SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds };
            availableSuits = new List<SuitEnum>(suits);
        }

        public override bool canPutOnTop(Card newCard)
        {
            if (newCard.value == topCard().value - 1 && newCard.suit == topCard().suit)
                return true;
            if (newCard.value == 13 && topCard().value == 1 && newCard.suit == topCard().suit)
                return true;
            SuitEnum result = SuitEnum.Any;
            foreach (SuitEnum s in availableSuits)
                if (s == newCard.suit)
                    result = s;
            if (newCard.value == 13 && topCard().value == 0 && result != SuitEnum.Any)
            {
                availableSuits.Remove(result);
                return true;
            }
            return false;
        }

        public override bool tryMove(Deck deck)
        {
            return false;
        }
    }
}
