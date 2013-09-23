using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class BackDeck : Deck
    {
        public BackDeck()
            : base()
        {
        }

        public BackDeck(string Name)
            : base(Name)
        {
        }

        public Card viewCard()
        {
            if (isEmpty())
                return Card.ZeroCard();
            else
                return Card.BackCard();
        }
    }
}
