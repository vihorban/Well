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
        public bool IsSomethingSelected;
        private OptionsViewModel _options;
        private Deck _selected;
        private int _topCount;

        public Game()
        {
            _options = new OptionsViewModel();
            _options.Language = _options.Language;
            _availableSuits = new List<SuitEnum>(_suits);
            _collection = new DeckCollection(_availableSuits);
            _generalDeck = new List<Card>();
            _selected = new Deck();
            _steps = new List<Step>();
        }

        public Step LastStep
        {
            get { return _steps[_steps.Count - 1]; }
        }

        public void InitializeGame()
        {
            Clear();
            IsGameOver = false;
            IsSomethingSelected = false;
            _topCount = DeckCollection.TopCount;
            _availableSuits.AddRange(_suits);
            GenerateGeneralDeck();
        }

        public void GenerateGeneralDeck()
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

        public void Clear()
        {
            foreach (Deck deck in Collection)
            {
                deck.Clear();
            }
            _generalDeck.Clear();
            _selected.Clear();
            _steps.Clear();
            _availableSuits.Clear();
        }

        public void Select(Deck deck)
        {
            _selected = deck;
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

        public void CollectBackDeck()
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

        public bool IsTopDecksEmpty()
        {
            return Collection.TopDecks.All(s => s.IsEmpty());
        }

        public void DeleteOldSteps()
        {
            if (_steps.Count > (int) _options.NumberOfCancellations - 1)
                DeleteLastStep();
        }

        public void DeleteLastStep()
        {
            _steps.Remove(LastStep);
        }

        public void AddNewStep()
        {
            _steps.Add(new Step());
        }

        public void SaveMovement(string from, string to)
        {
            LastStep.Add(from, to);
        }

        public bool IsGameWon()
        {
            const int neededValue = CardValue.King*NumOfGeneratedDecks;
            return Collection.ResultDecks.All(deck => deck.Count == neededValue);
        }

        public Deck FindByName(string deckName)
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

        #region Binding Variables

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

        #endregion

        #region Notify Helpers

        public void NotifyCardsChanged()
        {
            NotifyPropertyChanged("Collection");
            NotifyPropertyChanged("IsCancelEnabled");
        }

        #endregion
    }
}