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
        private const string BasePath = "pack://application:,,,";
        private readonly BackSuitListItem _backSuitListItem;
        private readonly CardStyleListItem _cardStyleListItem;
        private readonly EmptyCardListItem _emptyCardListItem;
        private readonly ZeroCardListItem _zeroCardListItem;

        public OptionsViewModel()
        {
            _backSuitListItem = new BackSuitListItem();
            _cardStyleListItem = new CardStyleListItem();
            _zeroCardListItem = new ZeroCardListItem();
            _emptyCardListItem = new EmptyCardListItem();
        }

        private Settings Settings
        {
            get { return Settings.Default; }
        }

        [LocalizableCategory("DesignColor")]
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

        [LocalizableCategory("DesignColor")]
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

        [LocalizableCategory("DesignColor")]
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

        [LocalizableCategory("DesignColor")]
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

        [LocalizableCategory("DesignColor")]
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

        [LocalizableCategory("DesignColor")]
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
        [LocalizableCategory("DifficultyMenu")]
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

        [LocalizableCategory("DifficultyMenu")]
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

        [LocalizableCategory("LanguageMenu")]
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

        [LocalizableCategory("DesignMenu")]
        [LocalizableDisplayName("Compact Menu")]
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
            get { return _cardStyleListItem.GetList(); }
        }

        [LocalizableCategory("CardStyle|")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("CardStyleItemsDescription")]
        public string CardStyle
        {
            get { return CardStyleItemsDescription[Settings.CardStyleSelectedNumber]; }
            set
            {
                Settings.CardStyleSelectedNumber = _cardStyleListItem.GetKey(value);
                NotifyPropertyChanged("CardStyle");
                NotifyPropertyChanged("CardStyleImagePreview");
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
            get { return _backSuitListItem.GetList(); }
        }

        [LocalizableCategory("Back Suit")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("BackSuitItemsDescription")]
        public string BackSuit
        {
            get { return BackSuitItemsDescription[Settings.BackSuitSelectedNumber]; }
            set
            {
                Settings.BackSuitSelectedNumber = _backSuitListItem.GetKey(value);
                NotifyPropertyChanged("BackSuit");
                NotifyPropertyChanged("BackSuitImagePreview");
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("Back Suit")]
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
            get { return _zeroCardListItem.GetList(); }
        }

        [LocalizableCategory("EmptyDeckMenu")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("ZeroCardItemsDescription")]
        public string ZeroCard
        {
            get { return ZeroCardItemsDescription[Settings.ZeroCardSelectedNumber]; }
            set
            {
                Settings.ZeroCardSelectedNumber = _zeroCardListItem.GetKey(value);
                NotifyPropertyChanged("ZeroCard");
                NotifyPropertyChanged("ZeroCardImagePreview");
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("EmptyDeckMenu")]
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
            get { return _emptyCardListItem.GetList(); }
        }

        [LocalizableCategory("EmptyCardMenu")]
        [LocalizableDisplayName("Choose")]
        [ItemsSourceProperty("EmptyCardItemsDescription")]
        public string EmptyCard
        {
            get { return EmptyCardItemsDescription[Settings.EmptyCardSelectedNumber]; }
            set
            {
                Settings.EmptyCardSelectedNumber = _emptyCardListItem.GetKey(value);
                NotifyPropertyChanged("EmptyCard");
                NotifyPropertyChanged("EmptyCardImagePreview");
            }
        }

        [Height(double.NaN, 0, 140)]
        [LocalizableCategory("EmptyCardMenu")]
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