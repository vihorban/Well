namespace Well.Objects
{
    public class WarehouseDeck : Deck
    {
        public const string Prefix = "W";

        public WarehouseDeck(int name = 0)
            : base(Prefix + name)
        {
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard.Value + 1 && newCard.Suit == TopCard.Suit)
                return true;
            if (((TopCard.Value == CardValue.King && newCard.Suit == TopCard.Suit) || TopCard.Value == CardValue.Empty) &&
                newCard.Value == CardValue.Ace)
                return true;
            return false;
        }
    }
}