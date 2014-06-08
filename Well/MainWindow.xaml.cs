using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Well.Objects;
using System.Threading;
using System.Globalization;
using PropertyTools.Wpf;

namespace Well
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dict = new ResourceDictionary();
            myGame = new Game();
            this.DataContext = myGame;
            myGame.NewGame();
            this.SetLanguageDictionary();
        }

        public Game myGame;
        public Border selectedBorder;
        public ResourceDictionary dict;

        private void SetLanguageDictionary()
        {
            myGame.Options.Language = Properties.Settings.Default.Language;
            switch (Thread.CurrentThread.CurrentUICulture.ToString())
            {
                case "uk-UA":
                    dict.Source = new Uri("..\\Resources\\LocalizationUA.xaml", UriKind.Relative);
                    break;
                case "ru-RU":
                    dict.Source = new Uri("..\\Resources\\LocalizationRU.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\LocalizationEN.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        public void checkGameOver()
        {
            if (myGame.IsGameOver)
                MessageBox.Show(FindResource("GameOverMessage").ToString());
        }

        public void checkWin()
        {
            if (myGame.IsGameWon())
                MessageBox.Show(FindResource("GameWinMessage").ToString());
        }

        private void imageBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (myGame.IsSomethingSelected)
            {
                makeWrongSelection(borderImageBack);
                makeUsualBorder(selectedBorder);
                myGame.IsSomethingSelected = false;
            }
            else
            {
                makeBackDeckLighted(borderImageBack);
                myGame.ReleaseBackDeck();
                myGame.NotifyTopCardsChanged();
                checkGameOver();
            }
        }

        public void makeWrongSelection(Border border)
        {
            border.BorderBrush = new SolidColorBrush(myGame.Options.WrongColor);
            Thread t = new Thread(() =>
            {
                Thread.Sleep(100);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.CasualBorderColor)));
                Thread.Sleep(100);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.WrongColor)));
                Thread.Sleep(100);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.CasualBorderColor)));
                Thread.Sleep(100);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.WrongColor)));
                Thread.Sleep(100);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.CasualBorderColor)));
            });
            t.IsBackground = true;
            t.Start();
        }

        public void makeBackDeckLighted(Border border)
        {
            border.BorderBrush = new SolidColorBrush(myGame.Options.DeckLightningColor);
            Thread t = new Thread(() =>
            {
                Thread.Sleep(500);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.CasualBorderColor)));
            });
            t.IsBackground = true;
            t.Start();
        }

        public void makeSuccess(Border border)
        {
            border.BorderBrush = new SolidColorBrush(myGame.Options.SuccessColor);
            Thread t = new Thread(() =>
            {
                Thread.Sleep(250);
                borderImageBack.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(myGame.Options.CasualBorderColor)));
            });
            t.IsBackground = true;
            t.Start();
        }

        public void makeUsualBorder(Border border)
        {
            border.BorderBrush = new SolidColorBrush(myGame.Options.CasualBorderColor);
        }

        public void makeSelected(Border border)
        {
            border.BorderBrush =  new SolidColorBrush(myGame.Options.SelectColor);
        }

        private void imageWarehouse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageWarehouse, myGame.WarehouseDeck);
        }

        private void makeStep(Border border, Deck deck)
        {
            if (myGame.IsSomethingSelected)
            {
                if (myGame.Selected.TryMove(deck))
                {
                    if (deck.Name[0] == 'R' && deck.Count() == 1)
                        myGame.ChangeAvailability(deck.Name);
                    myGame.CheckNumberOfSavedSteps();
                    myGame.AddNewStep();
                    myGame.SaveMovement(myGame.Selected.Name, deck.Name);
                    myGame.NotifyTopCardsChanged();
                    myGame.NotifyCountsChanged();
                    makeUsualBorder(selectedBorder);
                    makeSuccess(border);
                }
                else
                {
                    makeUsualBorder(selectedBorder);
                    makeWrongSelection(border);
                }
                myGame.IsSomethingSelected = false;
            }
            else
            {
                myGame.IsSomethingSelected = true;
                myGame.Selected = deck;
                selectedBorder= border;
                makeSelected(selectedBorder);
            }
            checkWin();
        }

        private void imageTop1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTop1, myGame.TopDecks[0]);
        }

        private void imageTop2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTop2, myGame.TopDecks[1]);
        }

        private void imageTop3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTop3, myGame.TopDecks[2]);
        }

        private void imageTop4_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTop4, myGame.TopDecks[3]);
        }

        private void imageTop5_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTop5, myGame.TopDecks[4]);
        }

        private void imageTopBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTopBorder, myGame.BorderChestDecks[1]);
        }

        private void imageTopMiddle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageTopMiddle, myGame.MiddleChestDecks[1]);
        }

        private void imageLeftTopResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageLeftTopResult, myGame.ResultDecks[0]);
        }

        private void imageRightTopResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageRightTopResult, myGame.ResultDecks[1]);
        }

        private void imageLeftBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageLeftBorder, myGame.BorderChestDecks[0]);
        }

        private void imageLeftMiddle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageLeftMiddle, myGame.MiddleChestDecks[0]);
        }

        private void imageRightMiddle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageRightMiddle, myGame.MiddleChestDecks[2]);
        }

        private void imageRightBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageRightBorder, myGame.BorderChestDecks[2]);
        }

        private void imageBottomMiddle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageBottomMiddle, myGame.MiddleChestDecks[3]);
        }

        private void imageLeftBottomResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageLeftBottomResult, myGame.ResultDecks[3]);
        }

        private void imageRightBottomResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageRightBottomResult, myGame.ResultDecks[2]);
        }

        private void imageBottomBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeStep(borderImageBottomBorder, myGame.BorderChestDecks[3]);
        }

        private void menuItemNewGame_Click(object sender, RoutedEventArgs e)
        {
            myGame.NewGame();
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuItemCancelStep_Click(object sender, RoutedEventArgs e)
        {
            if (myGame.IsCancelEnabled)
            {
                myGame.RestoreLastStep();
            }
        }

        private void menuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AboutDialog(this);
            dlg.Title = FindResource("AboutProgram").ToString();
            dlg.UpdateStatus = FindResource("UpdateStatus").ToString(); ;
            dlg.Comments = FindResource("Comments").ToString(); ;
            dlg.Image = new BitmapImage(new Uri(@"/about.png", UriKind.Relative));
            dlg.ShowDialog();
        }

        private void menuItemPreferences_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PropertyDialog() { Owner = this };
            var options = myGame.Options;

            dlg.DataContext = options;
            dlg.Title = FindResource("Preferences").ToString();
            if (dlg.ShowDialog().Value)
            {
                options.Save();
                myGame.Options = options;
                myGame.NotifyTopCardsChanged();
                SetLanguageDictionary();
            }           
        }

        private void menuItemResetDefault_Click(object sender, RoutedEventArgs e)
        {
            myGame.Options.ResetDefault();
            myGame.Options = myGame.Options;
            myGame.Options.Language = myGame.Options.Language;
            SetLanguageDictionary();
        }
    }
}
