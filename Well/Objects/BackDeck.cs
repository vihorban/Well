namespace Well.Objects
{
    public class BackDeck : Deck
    {
        public const string BaseName = "Back";

        public BackDeck()
            : base(BaseName, DeckType.Back)
        {
        }

        public override Card DisplayCard()
        {
            return IsEmpty() ? Card.ZeroCard() : Card.BackCard();
        }
    }
}