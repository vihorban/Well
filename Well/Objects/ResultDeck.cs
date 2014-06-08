using System.Collections.Generic;

namespace Well.Objects
{
    public class ResultDeck : Deck
    {
        public List<SuitEnum> AvailableSuits;

        public ResultDeck()
        {
            SuitEnum[] suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
            AvailableSuits = new List<SuitEnum>(suits);
        }

        public ResultDeck(string name)
            : base(name)
        {
            SuitEnum[] suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
            AvailableSuits = new List<SuitEnum>(suits);
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard().Value - 1 && newCard.Suit == TopCard().Suit)
                return true;
            if (newCard.Value == 13 && TopCard().Value == 1 && newCard.Suit == TopCard().Suit)
                return true;
            var result = SuitEnum.Any;
            foreach (SuitEnum s in AvailableSuits)
                if (s == newCard.Suit)
                    result = s;
            if (newCard.Value == 13 && TopCard().Value == 0 && result != SuitEnum.Any)
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