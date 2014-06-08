namespace Well.Objects
{
    public class BorderChestDeck : Deck
    {
        public const string Prefix = "B";

        public BorderChestDeck()
        {
        }

        public BorderChestDeck(int name)
            : base(Prefix + name)
        {
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard.Value + 1 && newCard.Suit == TopCard.Suit)
                return true;
            if (newCard.Value == CardValue.Ace && TopCard.Value == CardValue.King && newCard.Suit == TopCard.Suit)
                return true;
            return TopCard.Value == CardValue.Empty && newCard.DeckName[0] == MiddleChestDeck.Prefix[0] &&
                   newCard.DeckName[1] == Name[1];
        }
    }
}