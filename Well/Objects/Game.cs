using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class Game : Observable
    {
        public List<Card> generalDeck;

        public BackDeck backDeck;
        public BorderChestDeck[] borderChestDecks;
        public MiddleChestDeck[] middleChestDecks;
        public ResultDeck[] resultDecks;
        public TopDeck[] topDecks;
        public WarehouseDeck warehouseDeck;
        public Deck selected;

        public bool isSomethingSelected;
        public int topCount;
        public int numOfGeneratedDecks;
        public bool isGameOver;

        public List<Step> steps;

        private OptionsViewModel options;

        private string folder
        {
            get
            {
                return "cards"+ Properties.Settings.Default.CardStyleSelectedNumber.ToString() + "\\";
            }
        }

        public Game()
        {
            options = new OptionsViewModel();
            options.Language = options.Language;
            generalDeck = new List<Card>();
            backDeck = new BackDeck("Back");
            borderChestDecks = new BorderChestDeck[4];
            for (int i = 0; i < 4; i++)
            {
                borderChestDecks[i] = new BorderChestDeck("B" + i.ToString());
            }
            middleChestDecks = new MiddleChestDeck[4];
            for (int i = 0; i < 4; i++)
            {
                middleChestDecks[i] = new MiddleChestDeck("M" + i.ToString());
            }
            resultDecks = new ResultDeck[4];
            for (int i = 0; i < 4; i++)
            {
                resultDecks[i] = new ResultDeck("R" + i.ToString());
            }
            topDecks = new TopDeck[5];
            for (int i = 0; i < 5; i++)
            {
                topDecks[i] = new TopDeck("T" + i.ToString());
            }
            warehouseDeck = new WarehouseDeck("W0");
            selected = new Deck();
            topCount = 5;
            numOfGeneratedDecks = 2;
            isGameOver = false;
            isSomethingSelected = false;
            steps = new List<Step>();
        }

        public Step lastStep()
        {
            return steps[steps.Count - 1];
        }
        
        public void generateGeneralDeck()
        {
            SuitEnum[] suits = { SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds };
            for (int k = 0; k < numOfGeneratedDecks; k++)
            {

                for (int i = 1; i < 14; ++i)
                {
                    foreach (SuitEnum s in suits)
                    {
                        generalDeck.Add(new Card { value = i, suit = s, deckName = "Unknown" });
                    }
                }
                    
            }

        }

        public void Clear()
        {
            generalDeck.Clear();
            backDeck.Clear();
            warehouseDeck.Clear();
            selected.Clear();
            foreach (var s in borderChestDecks)
                s.Clear();
            foreach (var s in middleChestDecks)
                s.Clear();
            foreach (var s in resultDecks)
                s.Clear();
            foreach (var s in topDecks)
                s.Clear();
        }

        public void newGame()
        {
            Clear();
            generateGeneralDeck();
            Random random = new Random();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 10; j++)
                {
                    int left = generalDeck.Count + options.Difficulty - 60;
                    int index = random.Next(0, left);
                    Card addCard = generalDeck[index];
                    middleChestDecks[i].add(addCard);
                    generalDeck.RemoveAt(index);
                }
            }
            for (int i = 0; i < 4; ++i)
            {
                int left = generalDeck.Count + options.Difficulty - 60;
                int index = random.Next(0, left);
                borderChestDecks[i].add(generalDeck[index]);
                generalDeck.RemoveAt(index);
            }
            while (generalDeck.Count > 0)
            {
                int left = generalDeck.Count;
                int index = random.Next(0, left);
                backDeck.add(generalDeck[index]);
                generalDeck.RemoveAt(index);
            }
            topCount = 5;
            isGameOver = false;
            isSomethingSelected = false;
            SuitEnum[] suits = { SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds };
            List<SuitEnum> availableSuits = new List<SuitEnum>(suits);
            foreach (var s in resultDecks)
            {
                s.availableSuits = availableSuits;
            }
            steps.Clear();
            NotifyCountsChanged();
            NotifyTopCardsChanged();
        }

        public void releaseBackDeck()
        {
            checkNumberOfSavedSteps();
            addNewStep();
            if (backDeck.isEmpty())
            {
                collectBackDeck();
                topCount--;
                if(topCount==0)
                    isGameOver = true;
            }
            for (int i = 0; i < topCount; i++)
            {
                if (!backDeck.isEmpty())
                {
                    backDeck.moveTo(topDecks[i]);
                    saveMovement(backDeck.name, topDecks[i].name);
                }
            }
        }

        public void collectBackDeck()
        {
            while (!isTopDecksEmpty())
            {
                for (int i = 4; i >= 0; i--)
                {
                    if (!topDecks[i].isEmpty())
                    {
                        topDecks[i].moveTo(backDeck);
                        saveMovement(topDecks[i].name, backDeck.name);
                    }
                }
            }
        }

        public bool isTopDecksEmpty()
        {
            foreach (var s in topDecks)
                if (!s.isEmpty())
                    return false;
            return true;
        }

        public void changeAvailability(string nameDeck)
        {
            ResultDeck result = new ResultDeck();
            foreach (var k in resultDecks)
            {
                if (k.name == nameDeck)
                    result = k;
            }
            foreach (var k in resultDecks)
            {
                k.availableSuits = result.availableSuits;
            }
        }

        public void checkNumberOfSavedSteps()
        {
            if (steps.Count > (int)options.NumberOfCancellations - 1)
                deleteLastStep();
        }

        public void deleteLastStep()
        {
            steps.RemoveAt(steps.Count - 1);
        }

        public void addNewStep()
        {
            steps.Add(new Step());
        }

        public void saveMovement(string from, string to)
        {
            steps[steps.Count - 1].add(from, to);
        }

        public bool isGameWon()
        {
            bool isOk = true;
            for (int i = 0; i < 4 && isOk; i++)
                if (resultDecks[i].count() != 26)
                    isOk = false;
            return isOk;
        }

        public Deck findByName(string deckName)
        {
            if (warehouseDeck.name == deckName)
                return warehouseDeck;
            foreach (var s in borderChestDecks)
            {
                if (s.name == deckName)
                    return s;
            }
            foreach (var s in middleChestDecks)
            {
                if (s.name == deckName)
                    return s;
            }
            foreach (var s in resultDecks)
            {
                if (s.name == deckName)
                    return s;
            }
            foreach (var s in topDecks)
            {
                if (s.name == deckName)
                    return s;
            }
            return backDeck;
        }

        public void restoreLastStep()
        {
            if (steps.Count > 0)
            {
                Step lStep = lastStep();
                bool topCountChangeChecked = false;
                for (int i = lStep.movements.Count - 1; i >= 0; i--)
                {
                    string from = lStep.movements[i].From;
                    string to = lStep.movements[i].To;
                    Deck deckFrom = findByName(from);
                    Deck deckTo = findByName(to);
                    if (to[0] == 'R' && deckTo.count() == 1)
                    {
                        SuitEnum restoredSuit = deckTo.topCard().suit;
                        foreach (var s in resultDecks)
                        {
                            s.availableSuits.Add(restoredSuit);
                        }
                    }
                    if (!topCountChangeChecked && to[0] == 'B' && to[1] == 'a')
                    {
                        topCount++;
                        topCountChangeChecked = true;
                    }
                    deckTo.moveTo(deckFrom);
                }
                deleteLastStep();
                NotifyCountsChanged();
                NotifyTopCardsChanged();
            }
        }

        #region Private Members

        #endregion

        #region Binding Variables

        public OptionsViewModel Options
        {
            get
            {
                return options;
            }
            set
            {
                options = value;
                NotifyPropertyChanged("Options");
            }
        }

        public int LeftBorderNum
        {
            get 
            {
                return borderChestDecks[0].count();
            }
            set
            {
            }
        }

        public int TopBorderNum
        {
            get
            {
                return borderChestDecks[1].count();
            }
            set
            {
            }
        }

        public int RightBorderNum
        {
            get
            {
                return borderChestDecks[2].count();
            }
            set
            {
            }
        }

        public int BottomBorderNum
        {
            get
            {
                return borderChestDecks[3].count();
            }
            set
            {
            }
        }

        public int LeftMiddleNum
        {
            get
            {
                return middleChestDecks[0].count();
            }
            set
            {
            }
        }

        public int TopMiddleNum
        {
            get
            {
                return middleChestDecks[1].count();
            }
            set
            {
            }
        }

        public int RightMiddleNum
        {
            get
            {
                return middleChestDecks[2].count();
            }
            set
            {
            }
        }

        public int BottomMiddleNum
        {
            get
            {
                return middleChestDecks[3].count();
            }
            set
            {
            }
        }

        public string BackImageSource
        {
            get
            {
                return backDeck.viewCard().path(folder);
            }
            set
            {
            }
        }

        public string Top1ImageSource
        {
            get
            {
                return topDecks[0].topCard().path(folder);
            }
            set
            {
            }
        }

        public string Top2ImageSource
        {
            get
            {
                return topDecks[1].topCard().path(folder);
            }
            set
            {
            }
        }

        public string Top3ImageSource
        {
            get
            {
                return topDecks[2].topCard().path(folder);
            }
            set
            {
            }
        }

        public string Top4ImageSource
        {
            get
            {
                return topDecks[3].topCard().path(folder);
            }
            set
            {
            }
        }

        public string Top5ImageSource
        {
            get
            {
                return topDecks[4].topCard().path(folder);
            }
            set
            {
            }
        }

        public string WarehouseImageSource
        {
            get
            {
                return warehouseDeck.topCard().path(folder);
            }
            set
            {
            }
        }

        public string LeftBorderImageSource
        {
            get
            {
                return borderChestDecks[0].topCard().path(folder);
            }
            set
            {
            }
        }

        public string TopBorderImageSource
        {
            get
            {
                return borderChestDecks[1].topCard().path(folder);
            }
            set
            {
            }
        }

        public string RightBorderImageSource
        {
            get
            {
                return borderChestDecks[2].topCard().path(folder);
            }
            set
            {
            }
        }

        public string BottomBorderImageSource
        {
            get
            {
                return borderChestDecks[3].topCard().path(folder);
            }
            set
            {
            }
        }

        public string LeftMiddleImageSource
        {
            get
            {
                return middleChestDecks[0].topCard().path(folder);
            }
            set
            {
            }
        }

        public string TopMiddleImageSource
        {
            get
            {
                return middleChestDecks[1].topCard().path(folder);
            }
            set
            {
            }
        }

        public string RightMiddleImageSource
        {
            get
            {
                return middleChestDecks[2].topCard().path(folder);
            }
            set
            {
            }
        }

        public string BottomMiddleImageSource
        {
            get
            {
                return middleChestDecks[3].topCard().path(folder);
            }
            set
            {
            }
        }

        public string LeftTopResultImageSource
        {
            get
            {
                return resultDecks[0].topCard().path(folder);
            }
            set
            {
            }
        }

        public string RightTopResultImageSource
        {
            get
            {
                return resultDecks[1].topCard().path(folder);
            }
            set
            {
            }
        }

        public string RightBottomResultImageSource
        {
            get
            {
                return resultDecks[2].topCard().path(folder);
            }
            set
            {
            }
        }

        public string LeftBottomResultImageSource
        {
            get
            {
                return resultDecks[3].topCard().path(folder);
            }
            set
            {
            }
        }

        public bool IsCancelEnabled
        {
            get
            {
                return steps.Count > 0 && !isGameOver;
            }
            set
            {
            }
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
