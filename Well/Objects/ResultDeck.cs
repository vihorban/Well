using System.Collections.Generic;

namespace Well.Objects
{
    public class ResultDeck : Deck
    {
        public const string Prefix = "R";
        public List<SuitEnum> AvailableSuits;
        public SuitEnum? DisabledSuit;

        public ResultDeck() : this(0, new List<SuitEnum>())
        {
        }

        public ResultDeck(int name, List<SuitEnum> availableSuits)
            : base(Prefix + name, DeckType.Result)
        {
            AvailableSuits = availableSuits;
            DisabledSuit = null;
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard.Value - 1 && newCard.Suit == TopCard.Suit)
                return true;
            if (newCard.Value == CardValue.King && TopCard.Value == CardValue.Ace && newCard.Suit == TopCard.Suit)
                return true;
            if (newCard.Value == CardValue.King && TopCard.Value == CardValue.Empty &&
                AvailableSuits.Contains(newCard.Suit))
            {
                DisabledSuit = newCard.Suit;
                AvailableSuits.Remove(newCard.Suit);
                return true;
            }
            return false;
        }

        public override bool TryMove(Deck deck)
        {
            return false;
        }
    }
}