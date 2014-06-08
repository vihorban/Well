using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LocalizableAttribute;
using PropertyTools.DataAnnotations;
using Well.Objects;
using Well.Properties;

namespace Well
{
    public class OptionsViewModel : Observable
    {
        private const int NumOfBackSuits = 19;
        private const int NumOfEmptyCards = 8;
        private const int NumOfZeroCards = 11;
        private const int NumOfCardStyles = 6;
        private const string BasePath = "pack://application:,,,";

        private Settings Settings
        {
            get { return Settings.Default; }
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("BackgroundColor")]
        public Color BackgroundColor
        {
            get { return Settings.BackgroundColor; }
            set
            {
                Settings.BackgroundColor = value;
                NotifyPropertyChanged("BackgroundColor");
            }
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("SelectColor")]
        public Color SelectColor
        {
            get { return Settings.SelectColor; }
            set
            {
                Settings.SelectColor = value;
                NotifyPropertyChanged("SelectColor");
            }
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("CasualBorderColor")]
        public Color CasualBorderColor
        {
            get { return Settings.CasualBorderColor; }
            set
            {
                Settings.CasualBorderColor = value;
                NotifyPropertyChanged("CasualBorderColor");
            }
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("DeckLightningColor")]
        public Color DeckLightningColor
        {
            get { return Settings.DeckLightningColor; }
            set
            {
                Settings.DeckLightningColor = value;
                NotifyPropertyChanged("DeckLightningColor");
            }
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("WrongColor")]
        public Color WrongColor
        {
            get { return Settings.WrongColor; }
            set
            {
                Settings.WrongColor = value;
                NotifyPropertyChanged("WrongColor");
            }
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("SuccessColor")]
        public Color SuccessColor
        {
            get { return Settings.SuccessColor; }
            set
            {
                Settings.SuccessColor = value;
                NotifyPropertyChanged("SuccessColor");
            }
        }

        [Slidable(0, Game.MaxDifficulty)]
        [LocalizableCategory("Difficulty|")]
        [LocalizableDisplayName("Difficulty")]
        [LocalizableDescription("DifficultyDescrition")]
        public int Difficulty
        {
            get { return Settings.Difficulty; }
            set
            {
                Settings.Difficulty = value;
                if (Settings.Difficulty < 0)
                    Settings.Difficulty = 0;
                if (Settings.Difficulty > Game.MaxDifficulty)
                    Settings.Difficulty = Game.MaxDifficulty;
                NotifyPropertyChanged("Difficulty");
            }
        }

        [LocalizableCategory("Difficulty|")]
        [LocalizableDisplayName("NumberOfCancellations")]
        public Cancellation NumberOfCancellations
        {
            get { return Settings.NumberOfCancellations; }
            set
            {
                Settings.NumberOfCancellations = value;
                NotifyPropertyChanged("NumberOfCancellations");
            }
        }

        [LocalizableCategory("Language|")]
        [LocalizableDisplayName("Language")]
        [LocalizableDescription("LanguageChange")]
        public Languages Language
        {
            get { return Settings.Language; }
            set
            {
                Settings.Language = value;
                var cultureInfo = new CultureInfo(LanguageCode.GetCode(value));
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                NotifyPropertyChanged("Language");
            }
        }

        [LocalizableCategory("Design|Menu view")]
        [LocalizableDisplayName("Compact menu")]
        public bool CompactMenu
        {
            get { return Settings.CompactMenu; }
            set
            {
                Settings.CompactMenu = value;
                NotifyPropertyChanged("CompactMenu");
            }
        }

        [Browsable(false)]
        public List<string> CardStyleItemsDescription
        {
            get
            {
                var items = new List<string>();
                for (int i = 0; i < NumOfCardStyles; i++)
                    items.Add(new CardStyleListItem(i).ToString());
                return items;
            }
        }

        [LocalizableCategory("CardStyle|")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("CardStyleItemsDescription")]
        public string CardStyle
        {
            get { return CardStyleItemsDescription[Settings.CardStyleSelectedNumber]; }
            set
            {
                for (int i = 0; i < NumOfCardStyles; i++)
                {
                    if (CardStyleItemsDescription[i] == value)
                    {
                        Settings.CardStyleSelectedNumber = i;
                        NotifyPropertyChanged("CardStyle");
                        NotifyPropertyChanged("CardStyleImagePreview");
                    }
                }
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("CardStyle|")]
        [LocalizableDisplayName("Preview")]
        public ImageSource CardStyleImagePreview
        {
            get
            {
                string folder = "/cards" + Settings.CardStyleSelectedNumber + "/";
                return ImageMerger.Merge(folder);
            }
        }

        [Browsable(false)]
        public List<string> BackSuitItemsDescription
        {
            get
            {
                var items = new List<string>();
                for (int i = 0; i < NumOfBackSuits; i++)
                    items.Add(new BackSuitListItem(i).ToString());
                return items;
            }
        }

        [LocalizableCategory("Back suit|Back suit")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("BackSuitItemsDescription")]
        public string BackSuit
        {
            get { return BackSuitItemsDescription[Settings.BackSuitSelectedNumber]; }
            set
            {
                for (int i = 0; i < NumOfBackSuits; i++)
                {
                    if (BackSuitItemsDescription[i] == value)
                    {
                        Settings.BackSuitSelectedNumber = i;
                        NotifyPropertyChanged("BackSuit");
                        NotifyPropertyChanged("BackSuitImagePreview");
                    }
                }
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("Back suit|Back suit")]
        [LocalizableDisplayName("Preview")]
        public ImageSource BackSuitImagePreview
        {
            get
            {
                string path = "/" + Card.BackCard().Path("");
                var uri = new Uri(BasePath + path);
                return new BitmapImage(uri);
            }
        }

        [Browsable(false)]
        public List<string> ZeroCardItemsDescription
        {
            get
            {
                var items = new List<string>();
                for (int i = 0; i < NumOfZeroCards; i++)
                    items.Add(new ZeroCardListItem(i).ToString());
                return items;
            }
        }

        [LocalizableCategory("Empty deck|Empty deck")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("ZeroCardItemsDescription")]
        public string ZeroCard
        {
            get { return ZeroCardItemsDescription[Settings.ZeroCardSelectedNumber]; }
            set
            {
                for (int i = 0; i < NumOfZeroCards; i++)
                {
                    if (ZeroCardItemsDescription[i] == value)
                    {
                        Settings.ZeroCardSelectedNumber = i;
                        NotifyPropertyChanged("ZeroCard");
                        NotifyPropertyChanged("ZeroCardImagePreview");
                    }
                }
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("Empty deck|Empty deck")]
        [LocalizableDisplayName("Preview")]
        public ImageSource ZeroCardImagePreview
        {
            get
            {
                string path = "/" + Card.ZeroCard().Path("");
                var uri = new Uri(BasePath + path);
                return new BitmapImage(uri);
            }
        }

        [Browsable(false)]
        public List<string> EmptyCardItemsDescription
        {
            get
            {
                var items = new List<string>();
                for (int i = 0; i < NumOfEmptyCards; i++)
                    items.Add(new EmptyCardListItem(i).ToString());
                return items;
            }
        }

        [LocalizableCategory("Empty card|Empty card")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("EmptyCardItemsDescription")]
        public string EmptyCard
        {
            get { return EmptyCardItemsDescription[Settings.EmptyCardSelectedNumber]; }
            set
            {
                for (int i = 0; i < NumOfEmptyCards; i++)
                {
                    if (EmptyCardItemsDescription[i] == value)
                    {
                        Settings.EmptyCardSelectedNumber = i;
                        NotifyPropertyChanged("EmptyCard");
                        NotifyPropertyChanged("EmptyCardImagePreview");
                    }
                }
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("Empty card|Empty card")]
        [LocalizableDisplayName("Preview")]
        public ImageSource EmptyCardImagePreview
        {
            get
            {
                string path = "/" + Card.EmptyCard().Path("");
                var uri = new Uri(BasePath + path);
                return new BitmapImage(uri);
            }
        }

        public void ResetDefault()
        {
            Settings.Reset();
        }

        public void Save()
        {
            Settings.Save();
        }
    }
}