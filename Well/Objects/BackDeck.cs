namespace Well.Objects
{
    public class BackDeck : Deck
    {
        public BackDeck()
        {
        }

        public BackDeck(string Name)
            : base(Name)
        {
        }

        public Card ViewCard()
        {
            return IsEmpty() ? Card.ZeroCard() : Card.BackCard();
        }
    }
}