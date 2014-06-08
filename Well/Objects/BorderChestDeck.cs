namespace Well.Objects
{
    public class BorderChestDeck : Deck
    {
        public BorderChestDeck()
        {
        }

        public BorderChestDeck(string name)
            : base(name)
        {
        }

        public override bool CanPutOnTop(Card newCard)
        {
            if (newCard.Value == TopCard().Value + 1 && newCard.Suit == TopCard().Suit)
                return true;
            if (newCard.Value == 1 && TopCard().Value == 13 && newCard.Suit == TopCard().Suit)
                return true;
            return TopCard().Value == 0 && newCard.DeckName[0] == 'M' && newCard.DeckName[1] == Name[1];
        }
    }
}