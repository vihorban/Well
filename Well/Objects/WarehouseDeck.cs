using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class WarehouseDeck : Deck
    {
        public WarehouseDeck()
            : base()
        {
        }

        public WarehouseDeck(string Name)
            : base(Name)
        {
        }

        public override bool canPutOnTop(Card newCard)
        {
            if (newCard.value == topCard().value + 1 && newCard.suit == topCard().suit)
                return true;
            if (((topCard().value == 13 && newCard.suit == topCard().suit) || topCard().value == 0) && newCard.value == 1)
                return true;
            return false;
        }
    }
}
