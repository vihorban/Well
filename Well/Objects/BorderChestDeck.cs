using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class BorderChestDeck : Deck
    {
        public BorderChestDeck()
            : base()
        {
        }
        public BorderChestDeck(string Name)
            : base(Name)
        {
        }
        public override bool canPutOnTop(Card newCard)
        {
            if (newCard.value == topCard().value + 1 && newCard.suit == topCard().suit)
                return true;
            if (newCard.value == 1 && topCard().value == 13 && newCard.suit == topCard().suit)
                return true;
            if (topCard().value == 0 && newCard.deckName[0] == 'M' && newCard.deckName[1] == name[1])
                return true;
            return false;
        }
    }
}
