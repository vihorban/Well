using System.Collections.Generic;

namespace Well.Objects
{
    public class Deck
    {
        public const string Any = "Any";
        public List<Card> Items;
        public string Name;
        public DeckType Type;

        public Deck(string name = Any, DeckType deckType = DeckType.None)
        {
            Items = new List<Card>();
            Name = name;
            Type = deckType;
        }

        public Card TopCard
        {
            get { return IsEmpty() ? Card.EmptyCard() : Items[Items.Count - 1]; }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsEmpty()
        {
            return Items.Count == 0;
        }

        public virtual Card DisplayCard()
        {
            return TopCard;
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
            if (TopCard.Value > 0)
            {
                deck.Add(TopCard);
                Remove();
            }
        }

        public virtual bool TryMove(Deck deck)
        {
            if (deck.CanPutOnTop(TopCard))
            {
                MoveTo(deck);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}