using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LocalizableAttribute;

namespace Well.Objects
{
    public class CardValue
    {
        public const int Back = -2;
        public const int None = -1;
        public const int Empty = 0;
        public const int King = 13;
        public const int Ace = 1;
    }

    public enum SuitEnum
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
        Any
    }

    public enum DeckType
    {
        None,
        Result,
        Top,
        Middle,
        Border,
        Warehouse,
        Back
    }

    public enum Languages
    {
        [LocalizableDescription("Ukrainian")] Ukrainian = 1,
        [LocalizableDescription("Russian")] Russian = 2,
        [LocalizableDescription("English")] English = 3,
    }

    public enum Cancellation
    {
        [LocalizableDescription("OneCancellationEnable")] OneCancellationEnable = 1,
        [LocalizableDescription("TwoCancellationsEnable")] TwoCancellationsEnable = 2,
        [LocalizableDescription("InfinityCancellationsEnable")] InfinityCancellationsEnable = Int32.MaxValue,
    }

    public abstract class ListItem
    {
        protected Dictionary<int, string> Mapping;

        public int GetKey(string value)
        {
            return Mapping.First(x => Localizable(x.Value) == value).Key;
        }

        private string Localizable(string key)
        {
            return Application.Current.TryFindResource(key).ToString();
        }

        public List<string> GetList()
        {
            return Mapping.Select(kvp => Localizable(kvp.Value)).ToList();
        }
    }

    public class CardStyleListItem : ListItem
    {
        public CardStyleListItem()
        {
            Mapping = new Dictionary<int, string>
            {
                {0, "Casulal"},
                {1, "Modern"},
                {2, "MassEffect"},
                {3, "Snap2Objects"},
                {4, "Emotions"},
                {5, "Microsoft"}
            };
        }
    }

    public class BackSuitListItem : ListItem
    {
        public BackSuitListItem()
        {
            Mapping = new Dictionary<int, string>
            {
                {0, "Raster"},
                {1, "Lake"},
                {2, "Bird"},
                {3, "Field"},
                {4, "Tree"},
                {5, "Casual"},
                {6, "Space"},
                {7, "Earth"},
                {8, "Night City"},
                {9, "Quay"},
                {10, "Sunset"},
                {11, "Wing Mass Effect"},
                {12, "Star Mass Effect"},
                {13, "N7 Mass Effect"},
                {14, "Symbol Mass Effect"},
                {15, "Classic Emotions"},
                {16, "Classic Microsoft"},
                {17, "Hearts Microsoft"},
                {18, "Seasons Microsoft"},
            };
        }
    }


    public class EmptyCardListItem : ListItem
    {
        public EmptyCardListItem()
        {
            Mapping = new Dictionary<int, string>
            {
                {0, "Green"},
                {1, "Yellow"},
                {2, "Red"},
                {3, "Blue"},
                {4, "White"},
                {5, "Pink"},
                {6, "Gray"},
                {7, "Old"}
            };
        }
    }

    public class ZeroCardListItem : ListItem
    {
        public ZeroCardListItem()
        {
            Mapping = new Dictionary<int, string>
            {
                {0, "Green With Circle"},
                {1, "Green Usual"},
                {2, "Yellow With Circle"},
                {3, "Yellow Usual"},
                {4, "Create Me"},
                {5, "Gift Card"},
                {6, "Book"},
                {7, "Touch"},
                {8, "Thank You"},
                {9, "Guide"},
                {10, "Joke"}
            };
        }
    }
}