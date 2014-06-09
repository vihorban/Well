using System.Collections.Generic;

namespace Well.Objects
{
    public class DeckEnumerator
    {
        private Dictionary<string, Deck>.Enumerator _enumerator;

        public DeckEnumerator(DeckCollection coll)
        {
            _enumerator = coll.Decks.GetEnumerator();
        }

        public Deck Current
        {
            get { return _enumerator.Current.Value; }
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }
    }

    public class DeckCollection
    {
        public const int BorderCount = 4;
        public const int MiddleCount = 4;
        public const int ResultCount = 4;
        public const int TopCount = 5;

        public readonly Dictionary<string, Deck> Decks;
        public BackDeck BackDeck;
        public BorderChestDeck[] BorderChestDecks;
        public MiddleChestDeck[] MiddleChestDecks;
        public ResultDeck[] ResultDecks;
        public TopDeck[] TopDecks;
        public WarehouseDeck WarehouseDeck;

        public DeckCollection(List<SuitEnum> availableSuits)
        {
            Decks = new Dictionary<string, Deck>();
            BackDeck = new BackDeck();
            Decks.Add(BackDeck.Name, BackDeck);
            WarehouseDeck = new WarehouseDeck();
            Decks.Add(WarehouseDeck.Name, WarehouseDeck);
            BorderChestDecks = new BorderChestDeck[BorderCount];
            for (int i = 0; i < BorderCount; i++)
            {
                BorderChestDecks[i] = new BorderChestDeck(i);
                Decks.Add(BorderChestDecks[i].Name, BorderChestDecks[i]);
            }
            MiddleChestDecks = new MiddleChestDeck[MiddleCount];
            for (int i = 0; i < MiddleCount; i++)
            {
                MiddleChestDecks[i] = new MiddleChestDeck(i);
                Decks.Add(MiddleChestDecks[i].Name, MiddleChestDecks[i]);
            }
            ResultDecks = new ResultDeck[ResultCount];
            for (int i = 0; i < ResultCount; i++)
            {
                ResultDecks[i] = new ResultDeck(i, availableSuits);
                Decks.Add(ResultDecks[i].Name, ResultDecks[i]);
            }
            TopDecks = new TopDeck[TopCount];
            for (int i = 0; i < TopCount; i++)
            {
                TopDecks[i] = new TopDeck(i);
                Decks.Add(TopDecks[i].Name, TopDecks[i]);
            }
        }

        public Deck this[string key]
        {
            get { return Decks[key]; }
        }

        public DeckEnumerator GetEnumerator()
        {
            return new DeckEnumerator(this);
        }
    }
}