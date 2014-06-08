using System.Collections.Generic;

namespace Well.Objects
{
    public class Deck
    {
        public List<Card> Items;
        public string Name;

        public Deck()
        {
            Items = new List<Card>();
            Name = "Unknown";
        }

        public Deck(string name)
        {
            Items = new List<Card>();
            Name = name;
        }

        public bool IsEmpty()
        {
            return Items.Count == 0;
        }

        public Card TopCard()
        {
            return IsEmpty() ? Card.EmptyCard() : Items[Items.Count - 1];
        }

        public virtual bool CanPutOnTop(Card newCard)
        {
            return false;
        }

        public void Add(Card newCard)
        {
            newCard.DeckName = Name;
            Items.Add(newCard);
        }

        public void Remove()
        {
            if (!IsEmpty())
            {
                Items.RemoveAt(Items.Count - 1);
            }
        }

        public void MoveTo(Deck deck)
        {
            if (TopCard().Value > 0)
            {
                deck.Add(TopCard());
                Remove();
            }
        }

        public virtual bool TryMove(Deck deck)
        {
            if (deck.CanPutOnTop(TopCard()))
            {
                MoveTo(deck);
                return true;
            }
            return false;
        }

        public int Count()
        {
            return Items.Count;
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}