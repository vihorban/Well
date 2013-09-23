using System.ComponentModel;
using System.Globalization;
using Well.Properties;
using System.Threading;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Linq;
using System.Text;
using LocalizableAttribute;
using Well.Objects;
using PropertyTools.DataAnnotations;

namespace Well
{
    public class OptionsViewModel : Observable
    {
        private Settings Settings
        {
            get
            {
                return Settings.Default;
            }
        }

        private const int numOfBackSuits = 19;
        private const int numOfEmptyCards = 8;
        private const int numOfZeroCards = 11;
        private const int numOfCardStyles = 6;

        public void ResetDefault()
        {
            Settings.Reset();
        }

        [LocalizableCategory("Design|Color")]
        [LocalizableDisplayName("BackgroundColor")]
        public System.Windows.Media.Color BackgroundColor
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
        public System.Windows.Media.Color SelectColor
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
        public System.Windows.Media.Color CasualBorderColor
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
        public System.Windows.Media.Color DeckLightningColor
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
        public System.Windows.Media.Color WrongColor
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
        public System.Windows.Media.Color SuccessColor
        {
            get { return Settings.SuccessColor; }
            set
            {
                Settings.SuccessColor = value;
                NotifyPropertyChanged("SuccessColor");
            }
        }

        [Slidable(0, 60)]
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
                if (Settings.Difficulty > 60)
                    Settings.Difficulty = 60;
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
                CultureInfo cultureInfo = new CultureInfo(LanguageCode.getCode(value));
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                NotifyPropertyChanged("Language");
            }
        }

        [LocalizableCategory("Design|Menu view")]
        [LocalizableDisplayName("Compact menu")]
        public bool CompactMenu
        {
            get
            {
                return Settings.CompactMenu;
            }
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
                List<string> items = new List<string>();
                for (int i = 0; i < numOfCardStyles; i++)
                    items.Add(new CardStyleListItem(i).ToString());
                return items;
            }
            set
            {
            }
        }

        [LocalizableCategory("CardStyle|")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("CardStyleItemsDescription")]
        public string CardStyle
        {
            get
            {
                return CardStyleItemsDescription[Settings.CardStyleSelectedNumber];
            }
            set
            {
                for (int i = 0; i < numOfCardStyles; i++)
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
                string folder = "/cards" + Settings.CardStyleSelectedNumber.ToString() + "/";
                return ImageMerger.merge(folder);
            }
            set
            {
            }
        }

        [Browsable(false)]
        public List<string> BackSuitItemsDescription
        {
            get
            {
                List<string> items = new List<string>();
                for (int i = 0; i < numOfBackSuits; i++)
                    items.Add(new BackSuitListItem(i).ToString());
                return items;
            }
            set
            {
            }
        }

        [LocalizableCategory("Back suit|Back suit")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("BackSuitItemsDescription")]
        public string BackSuit
        {
            get
            {
                return BackSuitItemsDescription[Settings.BackSuitSelectedNumber];
            }
            set
            {
                for (int i = 0; i < numOfBackSuits; i++)
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
                string path = "/" + Card.BackCard().path("");
                Uri uri = new Uri("pack://application:,,," + path);
                return new BitmapImage(uri);
            }
            set
            {
            }
        }

        [Browsable(false)]
        public List<string> ZeroCardItemsDescription
        {
            get
            {
                List<string> items = new List<string>();
                for (int i = 0; i < numOfZeroCards; i++)
                    items.Add(new ZeroCardListItem(i).ToString());
                return items;
            }
            set
            {
            }
        }

        [LocalizableCategory("Empty deck|Empty deck")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("ZeroCardItemsDescription")]
        public string ZeroCard
        {
            get
            {
                return ZeroCardItemsDescription[Settings.ZeroCardSelectedNumber];
            }
            set
            {
                for (int i = 0; i < numOfZeroCards; i++)
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
                string path = "/" + Card.ZeroCard().path("");
                Uri uri = new Uri("pack://application:,,," + path);
                return new BitmapImage(uri);
            }
            set
            {
            }
        }

        [Browsable(false)]
        public List<string> EmptyCardItemsDescription
        {
            get
            {
                List<string> items = new List<string>();
                for (int i = 0; i < numOfEmptyCards; i++)
                    items.Add(new EmptyCardListItem(i).ToString());
                return items;
            }
            set
            {
            }
        }

        [LocalizableCategory("Empty card|Empty card")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("EmptyCardItemsDescription")]
        public string EmptyCard
        {
            get
            {
                return EmptyCardItemsDescription[Settings.EmptyCardSelectedNumber];
            }
            set
            {
                for (int i = 0; i < numOfEmptyCards; i++)
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
                string path = "/" + Card.EmptyCard().path("");
                Uri uri = new Uri("pack://application:,,," + path);
                return new BitmapImage(uri);
            }
            set
            {
            }
        }

        public void Save()
        {
            Settings.Save();
        }
    }
}
