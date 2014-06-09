using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PropertyTools.Wpf;
using Well.Objects;
using Well.Properties;

namespace Well
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public ResourceDictionary Dict;
        public Game MyGame;
        public Border SelectedBorder;

        public MainWindow()
        {
            InitializeComponent();
            Dict = new ResourceDictionary();
            MyGame = new Game();
            DataContext = MyGame;
            MyGame.NewGame();
            SetLanguageDictionary();
        }

        private void SetLanguageDictionary()
        {
            MyGame.Options.Language = Settings.Default.Language;
            switch (Thread.CurrentThread.CurrentUICulture.ToString())
            {
                case "uk-UA":
                    Dict.Source = new Uri("..\\Resources\\LocalizationUA.xaml", UriKind.Relative);
                    break;
                case "ru-RU":
                    Dict.Source = new Uri("..\\Resources\\LocalizationRU.xaml", UriKind.Relative);
                    break;
                default:
                    Dict.Source = new Uri("..\\Resources\\LocalizationEN.xaml", UriKind.Relative);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(Dict);
        }

        public void CheckGameOver()
        {
            if (MyGame.IsGameOver)
                MessageBox.Show(FindResource("GameOverMessage").ToString());
        }

        public void CheckWin()
        {
            if (MyGame.IsGameWon())
                MessageBox.Show(FindResource("GameWinMessage").ToString());
        }

        private void imageBack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var border = (Border) ((Image) sender).Parent;
            if (MyGame.IsSomethingSelected)
            {
                MakeAction(border, false);
            }
            else
            {
                MakeBackDeckLighted(border);
                MyGame.ReleaseBackDeck();
                CheckGameOver();
            }
        }


        public void MakeAction(Border border, bool isSuccess)
        {
            if (isSuccess)
            {
                MakeSuccessSelection(border);
            }
            else
            {
                MakeWrongSelection(border);
            }
            MakeUsualBorder(SelectedBorder);
            MyGame.IsSomethingSelected = false;
        }

        public void MakeWrongSelection(Border border)
        {
            border.BorderBrush = new SolidColorBrush(MyGame.Options.WrongColor);
            var t = new Thread(() =>
            {
                Thread.Sleep(100);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.CasualBorderColor)));
                Thread.Sleep(100);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.WrongColor)));
                Thread.Sleep(100);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.CasualBorderColor)));
                Thread.Sleep(100);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.WrongColor)));
                Thread.Sleep(100);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.CasualBorderColor)));
            }) {IsBackground = true};
            t.Start();
        }

        public void MakeBackDeckLighted(Border border)
        {
            border.BorderBrush = new SolidColorBrush(MyGame.Options.DeckLightningColor);
            var t = new Thread(() =>
            {
                Thread.Sleep(500);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.CasualBorderColor)));
            }) {IsBackground = true};
            t.Start();
        }

        public void MakeSuccessSelection(Border border)
        {
            border.BorderBrush = new SolidColorBrush(MyGame.Options.SuccessColor);
            var t = new Thread(() =>
            {
                Thread.Sleep(250);
                border.Dispatcher.BeginInvoke(
                    new Action(() => border.BorderBrush = new SolidColorBrush(MyGame.Options.CasualBorderColor)));
            }) {IsBackground = true};
            t.Start();
        }

        public void MakeUsualBorder(Border border)
        {
            border.BorderBrush = new SolidColorBrush(MyGame.Options.CasualBorderColor);
        }

        public void MakeSelected(Border border)
        {
            border.BorderBrush = new SolidColorBrush(MyGame.Options.SelectColor);
        }

        private void MakeStep(Border border, Deck deck)
        {
            if (MyGame.IsSomethingSelected)
            {
                MakeAction(border, MyGame.TryMove(deck));
            }
            else
            {
                MyGame.IsSomethingSelected = true;
                MyGame.Select(deck);
                SelectedBorder = border;
                MakeSelected(SelectedBorder);
            }
            CheckWin();
        }

        private void image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var currentImage = (Image) sender;
            Binding binding = BindingOperations.GetBinding(currentImage, Image.SourceProperty);
            if (binding == null) return;
            Deck currentDeck = MyGame.Collection[binding.ConverterParameter.ToString()];
            var currentBorder = (Border) currentImage.Parent;
            MakeStep(currentBorder, currentDeck);
        }

        private void menuItemNewGame_Click(object sender, RoutedEventArgs e)
        {
            MyGame.NewGame();
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuItemCancelStep_Click(object sender, RoutedEventArgs e)
        {
            if (MyGame.IsCancelEnabled)
            {
                MyGame.RestoreLastStep();
            }
        }

        private void menuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new AboutDialog(this)
            {
                Title = FindResource("AboutProgram").ToString(),
                UpdateStatus = FindResource("UpdateStatus").ToString(),
                Comments = FindResource("Comments").ToString(),
                Image = new BitmapImage(new Uri(@"/about.png", UriKind.Relative))
            };
            dlg.ShowDialog();
        }

        private void menuItemPreferences_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PropertyDialog {Owner = this};
            OptionsViewModel options = MyGame.Options;

            dlg.DataContext = options;
            dlg.Title = FindResource("Preferences").ToString();
            bool? showDialog = dlg.ShowDialog();
            if (showDialog == null || !showDialog.Value) return;
            options.Save();
            MyGame.Options = options;
            MyGame.NotifyCardsChanged();
            SetLanguageDictionary();
        }

        private void menuItemResetDefault_Click(object sender, RoutedEventArgs e)
        {
            MyGame.Options.ResetDefault();
            MyGame.Options = MyGame.Options;
            MyGame.Options.Language = MyGame.Options.Language;
            SetLanguageDictionary();
        }
    }
}