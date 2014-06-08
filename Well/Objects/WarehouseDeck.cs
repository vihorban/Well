namespace Well.Objects
{
    public class WarehouseDeck : Deck
    {
        public WarehouseDeck()
        {
        }

        public WarehouseDeck(string name)
            : base(name)
        {
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard().Value + 1 && newCard.Suit == TopCard().Suit)
                return true;
            if (((TopCard().Value == 13 && newCard.Suit == TopCard().Suit) || TopCard().Value == 0) &&
                newCard.Value == 1)
                return true;
            return false;
        }
    }
}