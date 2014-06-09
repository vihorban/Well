namespace Well.Objects
{
    public class WarehouseDeck : Deck
    {
        public const string Prefix = "W";

        public WarehouseDeck() : this(0)
        {
        }

        public WarehouseDeck(int name)
            : base(Prefix + name, DeckType.Warehouse)
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