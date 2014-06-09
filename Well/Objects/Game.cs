using System;
using System.Collections.Generic;
using System.Linq;

namespace Well.Objects
{
    public class Game : Observable
    {
        public const int NumOfGeneratedDecks = 2;
        public const int BorderCount = 10;
        public const int MaxDifficulty = 60;
        private readonly List<SuitEnum> _availableSuits;
        private readonly DeckCollection _collection;
        private readonly List<Card> _generalDeck;
        private readonly List<Step> _steps;
        private readonly SuitEnum[] _suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
        public bool IsGameOver;
        private bool _isSomethingSelected;
        private OptionsViewModel _options;
        private int _score;
        private Deck _selected;
        private int _topCount;

        public Game()
        {
            _options = new OptionsViewModel();
            _options.Language = _options.Language;
            _availableSuits = new List<SuitEnum>(_suits);
            _collection = new DeckCollection(_availableSuits);
            _generalDeck = new List<Card>();
            _steps = new List<Step>();
        }

        public OptionsViewModel Options
        {
            get { return _options; }
            set
            {
                _options = value;
                NotifyPropertyChanged("Options");
            }
        }

        public DeckCollection Collection
        {
            get { return _collection; }
        }

        public bool IsCancelEnabled
        {
            get { return _steps.Count > 0 && !IsGameOver; }
        }

        public bool IsSomethingSelected
        {
            get { return _isSomethingSelected; }
            set
            {
                _isSomethingSelected = value;
                if (_isSomethingSelected == false)
                {
                    _selected = null;
                }
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                NotifyPropertyChanged("Score");
            }
        }

        private Step LastStep
        {
            get { return _steps[_steps.Count - 1]; }
        }

        private void InitializeGame()
        {
            Clear();
            IsGameOver = false;
            IsSomethingSelected = false;
            _topCount = DeckCollection.TopCount;
            _availableSuits.AddRange(_suits);
            GenerateGeneralDeck();
            Score = 0;
        }

        private void GenerateGeneralDeck()
        {
            for (int k = 0; k < NumOfGeneratedDecks; k++)
            {
                for (int i = CardValue.Ace; i <= CardValue.King; ++i)
                {
                    foreach (SuitEnum s in _suits)
                    {
                        _generalDeck.Add(new Card {Value = i, Suit = s, DeckName = Deck.Any});
                    }
                }
            }
        }

        private void Clear()
        {
            foreach (Deck deck in Collection)
            {
                deck.Clear();
            }
            _generalDeck.Clear();
            _steps.Clear();
            _availableSuits.Clear();
        }

        public void Select(Deck deck)
        {
            _isSomethingSelected = true;
            _selected = deck;
        }

        public void Save(string fileName)
        {
            var game = new SavedGame
            {
                AvailableSuits = _availableSuits,
                Collection = Collection,
                IsGameOver = IsGameOver,
                TopCount = _topCount,
                Steps = _steps,
                Score = Score
            };
            XmlUtilities<SavedGame>.Serialize(game, fileName);
        }

        public void Load(string fileName)
        {
            SavedGame game = XmlUtilities<SavedGame>.Deserialize(fileName);
            Resore(game);
        }

        public void Resore(SavedGame game)
        {
            Clear();
            IsGameOver = game.IsGameOver;
            IsSomethingSelected = false;
            Score = game.Score;
            _topCount = game.TopCount;
            _availableSuits.AddRange(game.AvailableSuits);
            _collection.Copy(game.Collection, _availableSuits);
            _steps.AddRange(game.Steps);
            NotifyCardsChanged();
        }

        private void MoveFromGeneral(Deck to, Random random, bool useDifficulty = true)
        {
            int left = _generalDeck.Count;
            if (useDifficulty)
            {
                left += _options.Difficulty - MaxDifficulty;
            }
            int index = random.Next(0, left);
            Card addCard = _generalDeck[index];
            to.Add(addCard);
            _generalDeck.RemoveAt(index);
        }

        public void NewGame()
        {
            InitializeGame();
            var random = new Random();
            for (int i = 0; i < DeckCollection.MiddleCount; ++i)
            {
                for (int j = 0; j < BorderCount; j++)
                {
                    MoveFromGeneral(Collection.MiddleChestDecks[i], random);
                }
            }
            for (int i = 0; i < DeckCollection.BorderCount; ++i)
            {
                MoveFromGeneral(Collection.BorderChestDecks[i], random);
            }
            while (_generalDeck.Count > 0)
            {
                MoveFromGeneral(Collection.BackDeck, random, false);
            }
            NotifyCardsChanged();
        }

        public void ReleaseBackDeck()
        {
            DeleteOldSteps();
            AddNewStep();
            if (Collection.BackDeck.IsEmpty())
            {
                CollectBackDeck();
                _topCount--;
                LastStep.TopCountDecrased = true;
                Score += ScoreCounter.BackDeckOpened;
                if (_topCount == 0)
                    IsGameOver = true;
            }
            for (int i = 0; i < _topCount; i++)
            {
                if (!Collection.BackDeck.IsEmpty())
                {
                    Collection.BackDeck.MoveTo(Collection.TopDecks[i]);
                    SaveMovement(Collection.BackDeck.Name, Collection.TopDecks[i].Name);
                }
            }
            NotifyCardsChanged();
        }

        public bool TryMove(Deck to)
        {
            if (_selected.TryMove(to))
            {
                DeleteOldSteps();
                AddNewStep();
                ModifyScore(_selected, to);
                SaveMovement(_selected.Name, to.Name);
                if (to.Type == DeckType.Result)
                {
                    var deck = (ResultDeck) to;
                    if (deck.DisabledSuit != null)
                    {
                        LastStep.DisabledSuit = deck.DisabledSuit;
                        deck.DisabledSuit = null;
                    }
                }
                NotifyCardsChanged();
                return true;
            }
            return false;
        }

        public void ModifyScore(Deck from, Deck to)
        {
            int change = ScoreCounter.CountChange(from, to);
            Score += change;
            LastStep.ScoreIncreased = change;
        }

        private void CollectBackDeck()
        {
            while (!IsTopDecksEmpty())
            {
                for (int i = DeckCollection.TopCount - 1; i >= 0; i--)
                {
                    if (!Collection.TopDecks[i].IsEmpty())
                    {
                        Collection.TopDecks[i].MoveTo(Collection.BackDeck);
                        SaveMovement(Collection.TopDecks[i].Name, Collection.BackDeck.Name);
                    }
                }
            }
        }

        private bool IsTopDecksEmpty()
        {
            return Collection.TopDecks.All(s => s.IsEmpty());
        }

        private void DeleteOldSteps()
        {
            if (_steps.Count > (int) _options.NumberOfCancellations - 1)
                DeleteLastStep();
        }

        private void DeleteLastStep()
        {
            _steps.Remove(LastStep);
        }

        private void AddNewStep()
        {
            _steps.Add(new Step());
        }

        private void SaveMovement(string from, string to)
        {
            LastStep.Add(from, to);
        }

        public bool IsGameWon()
        {
            const int neededValue = CardValue.King*NumOfGeneratedDecks;
            return Collection.ResultDecks.All(deck => deck.Count == neededValue);
        }

        private Deck FindByName(string deckName)
        {
            return Collection[deckName];
        }

        public void RestoreLastStep()
        {
            if (_steps.Count > 0)
            {
                if (LastStep.DisabledSuit != null)
                {
                    _availableSuits.Add(LastStep.DisabledSuit.Value);
                }
                if (LastStep.TopCountDecrased)
                {
                    _topCount++;
                }
                Score -= LastStep.ScoreIncreased;
                for (int i = LastStep.Movements.Count - 1; i >= 0; i--)
                {
                    string from = LastStep.Movements[i].From;
                    string to = LastStep.Movements[i].To;
                    Deck deckFrom = FindByName(from);
                    Deck deckTo = FindByName(to);
                    deckTo.MoveTo(deckFrom);
                }
                DeleteLastStep();
                NotifyCardsChanged();
            }
        }

        #region Notify Helpers

        public void NotifyCardsChanged()
        {
            NotifyPropertyChanged("Collection");
            NotifyPropertyChanged("IsCancelEnabled");
        }

        #endregion
    }
}