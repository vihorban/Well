using System.Collections.Generic;

namespace Well.Objects
{
    public class DeckEnumerator
    {
        private Dictionary<string, Deck>.Enumerator _enumerator;

        public DeckEnumerator(DeckCollection coll)
        {
            _enumerator = coll.GetDictonaryEnumerator();
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

        private readonly Dictionary<string, Deck> _decks;
        public BackDeck BackDeck;
        public BorderChestDeck[] BorderChestDecks;
        public MiddleChestDeck[] MiddleChestDecks;
        public ResultDeck[] ResultDecks;
        public TopDeck[] TopDecks;
        public WarehouseDeck WarehouseDeck;

        public DeckCollection() : this(new List<SuitEnum>())
        {
        }

        public DeckCollection(List<SuitEnum> availableSuits)
        {
            _decks = new Dictionary<string, Deck>();
            BackDeck = new BackDeck();
            _decks.Add(BackDeck.Name, BackDeck);
            WarehouseDeck = new WarehouseDeck();
            _decks.Add(WarehouseDeck.Name, WarehouseDeck);
            BorderChestDecks = new BorderChestDeck[BorderCount];
            for (int i = 0; i < BorderCount; i++)
            {
                BorderChestDecks[i] = new BorderChestDeck(i);
                _decks.Add(BorderChestDecks[i].Name, BorderChestDecks[i]);
            }
            MiddleChestDecks = new MiddleChestDeck[MiddleCount];
            for (int i = 0; i < MiddleCount; i++)
            {
                MiddleChestDecks[i] = new MiddleChestDeck(i);
                _decks.Add(MiddleChestDecks[i].Name, MiddleChestDecks[i]);
            }
            ResultDecks = new ResultDeck[ResultCount];
            for (int i = 0; i < ResultCount; i++)
            {
                ResultDecks[i] = new ResultDeck(i, availableSuits);
                _decks.Add(ResultDecks[i].Name, ResultDecks[i]);
            }
            TopDecks = new TopDeck[TopCount];
            for (int i = 0; i < TopCount; i++)
            {
                TopDecks[i] = new TopDeck(i);
                _decks.Add(TopDecks[i].Name, TopDecks[i]);
            }
        }

        public Deck this[string key]
        {
            get { return _decks[key]; }
        }

        public void Copy(DeckCollection collection, List<SuitEnum> availableSuits)
        {
            _decks.Clear();
            BackDeck = collection.BackDeck;
            _decks.Add(BackDeck.Name, BackDeck);
            WarehouseDeck = collection.WarehouseDeck;
            _decks.Add(WarehouseDeck.Name, WarehouseDeck);
            BorderChestDecks = collection.BorderChestDecks;
            for (int i = 0; i < BorderCount; i++)
            {
                _decks.Add(BorderChestDecks[i].Name, BorderChestDecks[i]);
            }
            MiddleChestDecks = collection.MiddleChestDecks;
            for (int i = 0; i < MiddleCount; i++)
            {
                _decks.Add(MiddleChestDecks[i].Name, MiddleChestDecks[i]);
            }
            ResultDecks = collection.ResultDecks;
            for (int i = 0; i < ResultCount; i++)
            {
                ResultDecks[i].AvailableSuits = availableSuits;
                _decks.Add(ResultDecks[i].Name, ResultDecks[i]);
            }
            TopDecks = collection.TopDecks;
            for (int i = 0; i < TopCount; i++)
            {
                _decks.Add(TopDecks[i].Name, TopDecks[i]);
            }
        }

        public Dictionary<string, Deck>.Enumerator GetDictonaryEnumerator()
        {
            return _decks.GetEnumerator();
        }

        public DeckEnumerator GetEnumerator()
        {
            return new DeckEnumerator(this);
        }
    }
}