using System;
using System.Collections.Generic;
using Well.Properties;

namespace Well.Objects
{
    public class Game : Observable
    {
        public BackDeck BackDeck;
        public BorderChestDeck[] BorderChestDecks;
        public List<Card> GeneralDeck;
        public bool IsGameOver;
        public bool IsSomethingSelected;
        public MiddleChestDeck[] MiddleChestDecks;
        public int NumOfGeneratedDecks;
        public ResultDeck[] ResultDecks;
        public Deck Selected;

        public List<Step> Steps;
        public int TopCount;
        public TopDeck[] TopDecks;
        public WarehouseDeck WarehouseDeck;

        private OptionsViewModel _options;

        public Game()
        {
            _options = new OptionsViewModel();
            _options.Language = _options.Language;
            GeneralDeck = new List<Card>();
            BackDeck = new BackDeck("Back");
            BorderChestDecks = new BorderChestDeck[4];
            for (int i = 0; i < 4; i++)
            {
                BorderChestDecks[i] = new BorderChestDeck("B" + i);
            }
            MiddleChestDecks = new MiddleChestDeck[4];
            for (int i = 0; i < 4; i++)
            {
                MiddleChestDecks[i] = new MiddleChestDeck("M" + i);
            }
            ResultDecks = new ResultDeck[4];
            for (int i = 0; i < 4; i++)
            {
                ResultDecks[i] = new ResultDeck("R" + i);
            }
            TopDecks = new TopDeck[5];
            for (int i = 0; i < 5; i++)
            {
                TopDecks[i] = new TopDeck("T" + i);
            }
            WarehouseDeck = new WarehouseDeck("W0");
            Selected = new Deck();
            TopCount = 5;
            NumOfGeneratedDecks = 2;
            IsGameOver = false;
            IsSomethingSelected = false;
            Steps = new List<Step>();
        }

        private string folder
        {
            get { return "cards" + Settings.Default.CardStyleSelectedNumber + "\\"; }
        }

        public Step LastStep()
        {
            return Steps[Steps.Count - 1];
        }

        public void GenerateGeneralDeck()
        {
            SuitEnum[] suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
            for (int k = 0; k < NumOfGeneratedDecks; k++)
            {
                for (int i = 1; i < 14; ++i)
                {
                    foreach (SuitEnum s in suits)
                    {
                        GeneralDeck.Add(new Card {Value = i, Suit = s, DeckName = "Unknown"});
                    }
                }
            }
        }

        public void Clear()
        {
            GeneralDeck.Clear();
            BackDeck.Clear();
            WarehouseDeck.Clear();
            Selected.Clear();
            foreach (BorderChestDeck s in BorderChestDecks)
                s.Clear();
            foreach (MiddleChestDeck s in MiddleChestDecks)
                s.Clear();
            foreach (ResultDeck s in ResultDecks)
                s.Clear();
            foreach (TopDeck s in TopDecks)
                s.Clear();
        }

        public void NewGame()
        {
            Clear();
            GenerateGeneralDeck();
            var random = new Random();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 10; j++)
                {
                    int left = GeneralDeck.Count + _options.Difficulty - 60;
                    int index = random.Next(0, left);
                    Card addCard = GeneralDeck[index];
                    MiddleChestDecks[i].Add(addCard);
                    GeneralDeck.RemoveAt(index);
                }
            }
            for (int i = 0; i < 4; ++i)
            {
                int left = GeneralDeck.Count + _options.Difficulty - 60;
                int index = random.Next(0, left);
                BorderChestDecks[i].Add(GeneralDeck[index]);
                GeneralDeck.RemoveAt(index);
            }
            while (GeneralDeck.Count > 0)
            {
                int left = GeneralDeck.Count;
                int index = random.Next(0, left);
                BackDeck.Add(GeneralDeck[index]);
                GeneralDeck.RemoveAt(index);
            }
            TopCount = 5;
            IsGameOver = false;
            IsSomethingSelected = false;
            SuitEnum[] suits = {SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds};
            var availableSuits = new List<SuitEnum>(suits);
            foreach (ResultDeck s in ResultDecks)
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
            if (BackDeck.IsEmpty())
            {
                CollectBackDeck();
                TopCount--;
                if (TopCount == 0)
                    IsGameOver = true;
            }
            for (int i = 0; i < TopCount; i++)
            {
                if (!BackDeck.IsEmpty())
                {
                    BackDeck.MoveTo(TopDecks[i]);
                    SaveMovement(BackDeck.Name, TopDecks[i].Name);
                }
            }
        }

        public void CollectBackDeck()
        {
            while (!IsTopDecksEmpty())
            {
                for (int i = 4; i >= 0; i--)
                {
                    if (!TopDecks[i].IsEmpty())
                    {
                        TopDecks[i].MoveTo(BackDeck);
                        SaveMovement(TopDecks[i].Name, BackDeck.Name);
                    }
                }
            }
        }

        public bool IsTopDecksEmpty()
        {
            foreach (TopDeck s in TopDecks)
                if (!s.IsEmpty())
                    return false;
            return true;
        }

        public void ChangeAvailability(string nameDeck)
        {
            var result = new ResultDeck();
            foreach (ResultDeck k in ResultDecks)
            {
                if (k.Name == nameDeck)
                    result = k;
            }
            foreach (ResultDeck k in ResultDecks)
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
            bool isOk = true;
            for (int i = 0; i < 4 && isOk; i++)
                if (ResultDecks[i].Count() != 26)
                    isOk = false;
            return isOk;
        }

        public Deck FindByName(string deckName)
        {
            if (WarehouseDeck.Name == deckName)
                return WarehouseDeck;
            foreach (BorderChestDeck s in BorderChestDecks)
            {
                if (s.Name == deckName)
                    return s;
            }
            foreach (MiddleChestDeck s in MiddleChestDecks)
            {
                if (s.Name == deckName)
                    return s;
            }
            foreach (ResultDeck s in ResultDecks)
            {
                if (s.Name == deckName)
                    return s;
            }
            foreach (TopDeck s in TopDecks)
            {
                if (s.Name == deckName)
                    return s;
            }
            return BackDeck;
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
                    if (to[0] == 'R' && deckTo.Count() == 1)
                    {
                        SuitEnum restoredSuit = deckTo.TopCard().Suit;
                        foreach (ResultDeck s in ResultDecks)
                        {
                            s.AvailableSuits.Add(restoredSuit);
                        }
                    }
                    if (!topCountChangeChecked && to[0] == 'B' && to[1] == 'a')
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

        public int LeftBorderNum
        {
            get { return BorderChestDecks[0].Count(); }
        }

        public int TopBorderNum
        {
            get { return BorderChestDecks[1].Count(); }
        }

        public int RightBorderNum
        {
            get { return BorderChestDecks[2].Count(); }
        }

        public int BottomBorderNum
        {
            get { return BorderChestDecks[3].Count(); }
        }

        public int LeftMiddleNum
        {
            get { return MiddleChestDecks[0].Count(); }
        }

        public int TopMiddleNum
        {
            get { return MiddleChestDecks[1].Count(); }
        }

        public int RightMiddleNum
        {
            get { return MiddleChestDecks[2].Count(); }
        }

        public int BottomMiddleNum
        {
            get { return MiddleChestDecks[3].Count(); }
        }

        public string BackImageSource
        {
            get { return BackDeck.ViewCard().Path(folder); }
        }

        public string Top1ImageSource
        {
            get { return TopDecks[0].TopCard().Path(folder); }
        }

        public string Top2ImageSource
        {
            get { return TopDecks[1].TopCard().Path(folder); }
        }

        public string Top3ImageSource
        {
            get { return TopDecks[2].TopCard().Path(folder); }
        }

        public string Top4ImageSource
        {
            get { return TopDecks[3].TopCard().Path(folder); }
        }

        public string Top5ImageSource
        {
            get { return TopDecks[4].TopCard().Path(folder); }
        }

        public string WarehouseImageSource
        {
            get { return WarehouseDeck.TopCard().Path(folder); }
        }

        public string LeftBorderImageSource
        {
            get { return BorderChestDecks[0].TopCard().Path(folder); }
        }

        public string TopBorderImageSource
        {
            get { return BorderChestDecks[1].TopCard().Path(folder); }
        }

        public string RightBorderImageSource
        {
            get { return BorderChestDecks[2].TopCard().Path(folder); }
        }

        public string BottomBorderImageSource
        {
            get { return BorderChestDecks[3].TopCard().Path(folder); }
        }

        public string LeftMiddleImageSource
        {
            get { return MiddleChestDecks[0].TopCard().Path(folder); }
        }

        public string TopMiddleImageSource
        {
            get { return MiddleChestDecks[1].TopCard().Path(folder); }
        }

        public string RightMiddleImageSource
        {
            get { return MiddleChestDecks[2].TopCard().Path(folder); }
        }

        public string BottomMiddleImageSource
        {
            get { return MiddleChestDecks[3].TopCard().Path(folder); }
        }

        public string LeftTopResultImageSource
        {
            get { return ResultDecks[0].TopCard().Path(folder); }
        }

        public string RightTopResultImageSource
        {
            get { return ResultDecks[1].TopCard().Path(folder); }
        }

        public string RightBottomResultImageSource
        {
            get { return ResultDecks[2].TopCard().Path(folder); }
        }

        public string LeftBottomResultImageSource
        {
            get { return ResultDecks[3].TopCard().Path(folder); }
        }

        public bool IsCancelEnabled
        {
            get { return Steps.Count > 0 && !IsGameOver; }
        }

        #endregion

        #region Notify Helpers

        public void NotifyCountsChanged()
        {
            NotifyPropertyChanged("LeftBorderNum");
            NotifyPropertyChanged("TopBorderNum");
            NotifyPropertyChanged("RightBorderNum");
            NotifyPropertyChanged("BottomBorderNum");
            NotifyPropertyChanged("LeftMiddleNum");
            NotifyPropertyChanged("TopMiddleNum");
            NotifyPropertyChanged("RightMiddleNum");
            NotifyPropertyChanged("BottomMiddleNum");
        }

        public void NotifyTopCardsChanged()
        {
            NotifyPropertyChanged("BackImageSource");
            NotifyPropertyChanged("Top1ImageSource");
            NotifyPropertyChanged("Top2ImageSource");
            NotifyPropertyChanged("Top3ImageSource");
            NotifyPropertyChanged("Top4ImageSource");
            NotifyPropertyChanged("Top5ImageSource");
            NotifyPropertyChanged("WarehouseImageSource");
            NotifyPropertyChanged("LeftBorderImageSource");
            NotifyPropertyChanged("TopBorderImageSource");
            NotifyPropertyChanged("RightBorderImageSource");
            NotifyPropertyChanged("BottomBorderImageSource");
            NotifyPropertyChanged("LeftMiddleImageSource");
            NotifyPropertyChanged("TopMiddleImageSource");
            NotifyPropertyChanged("RightMiddleImageSource");
            NotifyPropertyChanged("BottomMiddleImageSource");
            NotifyPropertyChanged("LeftTopResultImageSource");
            NotifyPropertyChanged("RightTopResultImageSource");
            NotifyPropertyChanged("RightBottomResultImageSource");
            NotifyPropertyChanged("LeftBottomResultImageSource");
            NotifyPropertyChanged("IsCancelEnabled");
        }

        #endregion
    }
}