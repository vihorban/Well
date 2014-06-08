using System.Collections.Generic;

namespace Well.Objects
{
    public class ResultDeck : Deck
    {
        public List<SuitEnum> AvailableSuits;
        public const string Prefix = "R";

        public ResultDeck(int name)
            : base(Prefix+name)
        {
            SuitEnum[] suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
            AvailableSuits = new List<SuitEnum>(suits);
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard.Value - 1 && newCard.Suit == TopCard.Suit)
                return true;
            if (newCard.Value == CardValue.King && TopCard.Value == CardValue.Ace && newCard.Suit == TopCard.Suit)
                return true;
            var result = SuitEnum.Any;
            foreach (SuitEnum s in AvailableSuits)
                if (s == newCard.Suit)
                    result = s;
            if (newCard.Value == CardValue.King && TopCard.Value == CardValue.Empty && result != SuitEnum.Any)
            {
                AvailableSuits.Remove(result);
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