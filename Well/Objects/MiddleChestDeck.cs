namespace Well.Objects
{
    public class MiddleChestDeck : Deck
    {
        public const string Prefix = "M";

        public MiddleChestDeck(int name)
            : base(Prefix + name)
        {
        }
    }
}