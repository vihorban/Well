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
        private readonly DeckCollection _collection;
        private readonly SuitEnum[] _suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
        public List<Card> GeneralDeck;

        public bool IsGameOver;
        public bool IsSomethingSelected;

        public Deck Selected;

        public List<Step> Steps;
        public int TopCount;

        private OptionsViewModel _options;

        public Game()
        {
            _options = new OptionsViewModel();
            _options.Language = _options.Language;
            GeneralDeck = new List<Card>();
            _collection = new DeckCollection();
            Selected = new Deck();
            IsGameOver = false;
            IsSomethingSelected = false;
            Steps = new List<Step>();
            TopCount = DeckCollection.TopCount;
        }

        public Step LastStep()
        {
            return Steps[Steps.Count - 1];
        }

        public void GenerateGeneralDeck()
        {
            for (int k = 0; k < NumOfGeneratedDecks; k++)
            {
                for (int i = CardValue.Ace; i <= CardValue.King; ++i)
                {
                    foreach (SuitEnum s in _suits)
                    {
                        GeneralDeck.Add(new Card {Value = i, Suit = s, DeckName = Deck.Any});
                    }
                }
            }
        }

        public void Clear()
        {
            GeneralDeck.Clear();
            Selected.Clear();
            foreach (Deck deck in Collection)
            {
                deck.Clear();
            }
        }

        public void NewGame()
        {
            Clear();
            GenerateGeneralDeck();
            var random = new Random();
            for (int i = 0; i < DeckCollection.MiddleCount; ++i)
            {
                for (int j = 0; j < BorderCount; j++)
                {
                    int left = GeneralDeck.Count + _options.Difficulty - MaxDifficulty;
                    int index = random.Next(0, left);
                    Card addCard = GeneralDeck[index];
                    Collection.MiddleChestDecks[i].Add(addCard);
                    GeneralDeck.RemoveAt(index);
                }
            }
            for (int i = 0; i < DeckCollection.BorderCount; ++i)
            {
                int left = GeneralDeck.Count + _options.Difficulty - MaxDifficulty;
                int index = random.Next(0, left);
                Collection.BorderChestDecks[i].Add(GeneralDeck[index]);
                GeneralDeck.RemoveAt(index);
            }
            while (GeneralDeck.Count > 0)
            {
                int left = GeneralDeck.Count;
                int index = random.Next(0, left);
                Collection.BackDeck.Add(GeneralDeck[index]);
                GeneralDeck.RemoveAt(index);
            }
            IsGameOver = false;
            IsSomethingSelected = false;
            TopCount = DeckCollection.TopCount;
            var availableSuits = new List<SuitEnum>(_suits);
            foreach (ResultDeck s in Collection.ResultDecks)
            {
                s.AvailableSuits = availableSuits;
            }
            Steps.Clear();
            NotifyCountsChanged();
            NotifyTopCardsChanged();
        }

        public void ReleaseBackDeck()
        {
            CheckNumberOfSavedSteps();
            AddNewStep();
            if (Collection.BackDeck.IsEmpty())
            {
                CollectBackDeck();
                TopCount--;
                if (TopCount == 0)
                    IsGameOver = true;
            }
            for (int i = 0; i < TopCount; i++)
            {
                if (!Collection.BackDeck.IsEmpty())
                {
                    Collection.BackDeck.MoveTo(Collection.TopDecks[i]);
                    SaveMovement(Collection.BackDeck.Name, Collection.TopDecks[i].Name);
                }
            }
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

        public void ChangeAvailability(string nameDeck)
        {
            ResultDeck result = null;
            foreach (ResultDeck k in Collection.ResultDecks)
            {
                if (k.Name == nameDeck)
                    result = k;
            }
            if (result == null) return;
            foreach (ResultDeck k in Collection.ResultDecks)
            {
                k.AvailableSuits = result.AvailableSuits;
            }
        }

        public void CheckNumberOfSavedSteps()
        {
            if (Steps.Count > (int) _options.NumberOfCancellations - 1)
                DeleteLastStep();
        }

        public void DeleteLastStep()
        {
            Steps.RemoveAt(Steps.Count - 1);
        }

        public void AddNewStep()
        {
            Steps.Add(new Step());
        }

        public void SaveMovement(string from, string to)
        {
            Steps[Steps.Count - 1].Add(from, to);
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
            if (Steps.Count > 0)
            {
                Step lStep = LastStep();
                bool topCountChangeChecked = false;
                for (int i = lStep.Movements.Count - 1; i >= 0; i--)
                {
                    string from = lStep.Movements[i].From;
                    string to = lStep.Movements[i].To;
                    Deck deckFrom = FindByName(from);
                    Deck deckTo = FindByName(to);
                    if (to[0] == ResultDeck.Prefix[0] && deckTo.Count == 1)
                    {
                        SuitEnum restoredSuit = deckTo.TopCard.Suit;
                        foreach (ResultDeck s in Collection.ResultDecks)
                        {
                            s.AvailableSuits.Add(restoredSuit);
                        }
                    }
                    if (!topCountChangeChecked && to == BackDeck.BaseName)
                    {
                        TopCount++;
                        topCountChangeChecked = true;
                    }
                    deckTo.MoveTo(deckFrom);
                }
                DeleteLastStep();
                NotifyCountsChanged();
                NotifyTopCardsChanged();
            }
        }

        #region Private Members

        #endregion

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
            get { return Steps.Count > 0 && !IsGameOver; }
        }

        #endregion

        #region Notify Helpers

        public void NotifyCountsChanged()
        {
            NotifyPropertyChanged("Collection");
        }

        public void NotifyTopCardsChanged()
        {
            NotifyPropertyChanged("Collection");
            NotifyPropertyChanged("IsCancelEnabled");
        }

        #endregion
    }
}