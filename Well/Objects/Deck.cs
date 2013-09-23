using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class Deck
    {
        public List<Card> items;
        public string name;

        public Deck()
        {
            items = new List<Card>();
            name = "Unknown";
        }

        public Deck(string Name)
        {
            items = new List<Card>();
            name = Name;
        }

        public bool isEmpty()
        {
            return items.Count == 0;
        }

        public Card topCard()
        {
            if (isEmpty())
                return Card.EmptyCard();
            else
                return items[items.Count - 1];
        }

        public virtual bool canPutOnTop(Card newCard)
        {
            return false;
        }

        public void add(Card newCard)
        {
            newCard.deckName = name;
            items.Add(newCard);
        }

        public void remove()
        {
            if (!isEmpty())
            {
                items.RemoveAt(items.Count - 1);
            }
        }

        public void moveTo(Deck deck)
        {
            if (topCard().value > 0)
            {
                deck.add(topCard());
                remove();
            }
        }

        public virtual bool tryMove(Deck deck)
        {
            if (deck.canPutOnTop(topCard()))
            {
                moveTo(deck);
                return true;
            }
            else return false;
        }

        public int count()
        {
            return items.Count;
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}
