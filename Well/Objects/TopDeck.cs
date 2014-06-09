namespace Well.Objects
{
    public class TopDeck : Deck
    {
        public const string Prefix = "T";

        public TopDeck(int name)
            : base(Prefix + name, DeckType.Top)
        {
        }
    }
}