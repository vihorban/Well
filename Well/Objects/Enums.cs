using System;
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

    public class CardStyleListItem
    {
        public int Value;

        public CardStyleListItem(int newValue)
        {
            Value = newValue;
        }

        public override string ToString()
        {
            switch (Value)
            {
                case 0:
                {
                    return Application.Current.TryFindResource("Casulal").ToString();
                }
                case 1:
                {
                    return Application.Current.TryFindResource("Modern").ToString();
                }
                case 2:
                {
                    return Application.Current.TryFindResource("MassEffect").ToString();
                }
                case 3:
                {
                    return Application.Current.TryFindResource("Snap2Objects").ToString();
                }
                case 4:
                {
                    return Application.Current.TryFindResource("Emotions").ToString();
                }
                case 5:
                {
                    return Application.Current.TryFindResource("Microsoft").ToString();
                }
                default:
                {
                    return Application.Current.TryFindResource("Error").ToString();
                }
            }
        }
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

    public class BackSuitListItem
    {
        public int Value;

        public BackSuitListItem(int newValue)
        {
            Value = newValue;
        }

        public override string ToString()
        {
            switch (Value)
            {
                case 0:
                {
                    return Application.Current.TryFindResource("Raster").ToString();
                }
                case 1:
                {
                    return Application.Current.TryFindResource("Lake").ToString();
                }
                case 2:
                {
                    return Application.Current.TryFindResource("Bird").ToString();
                }
                case 3:
                {
                    return Application.Current.TryFindResource("Field").ToString();
                }
                case 4:
                {
                    return Application.Current.TryFindResource("Tree").ToString();
                }
                case 5:
                {
                    return Application.Current.TryFindResource("Casual").ToString();
                }
                case 6:
                {
                    return Application.Current.TryFindResource("Space").ToString();
                }
                case 7:
                {
                    return Application.Current.TryFindResource("Earth").ToString();
                }
                case 8:
                {
                    return Application.Current.TryFindResource("Night city").ToString();
                }
                case 9:
                {
                    return Application.Current.TryFindResource("Quay").ToString();
                }
                case 10:
                {
                    return Application.Current.TryFindResource("Sunset").ToString();
                }
                case 11:
                {
                    return Application.Current.TryFindResource("Wing @Mass Effect").ToString();
                }
                case 12:
                {
                    return Application.Current.TryFindResource("Star @Mass Effect").ToString();
                }
                case 13:
                {
                    return Application.Current.TryFindResource("N7 @Mass Effect").ToString();
                }
                case 14:
                {
                    return Application.Current.TryFindResource("Symbol @Mass Effect").ToString();
                }
                case 15:
                {
                    return Application.Current.TryFindResource("Classic @Emotions").ToString();
                }
                case 16:
                {
                    return Application.Current.TryFindResource("Classic @Microsoft").ToString();
                }
                case 17:
                {
                    return Application.Current.TryFindResource("Hearts @Microsoft").ToString();
                }
                case 18:
                {
                    return Application.Current.TryFindResource("Seasons @Microsoft").ToString();
                }
                default:
                {
                    return Application.Current.TryFindResource("Error").ToString();
                }
            }
        }
    }

    public class EmptyCardListItem
    {
        public int Value;

        public EmptyCardListItem(int newValue)
        {
            Value = newValue;
        }

        public override string ToString()
        {
            switch (Value)
            {
                case 0:
                {
                    return Application.Current.TryFindResource("Green").ToString();
                }
                case 1:
                {
                    return Application.Current.TryFindResource("Yellow").ToString();
                }
                case 2:
                {
                    return Application.Current.TryFindResource("Red").ToString();
                }
                case 3:
                {
                    return Application.Current.TryFindResource("Blue").ToString();
                }
                case 4:
                {
                    return Application.Current.TryFindResource("White").ToString();
                }
                case 5:
                {
                    return Application.Current.TryFindResource("Pink").ToString();
                }
                case 6:
                {
                    return Application.Current.TryFindResource("Gray").ToString();
                }
                case 7:
                {
                    return Application.Current.TryFindResource("Old").ToString();
                }
                default:
                {
                    return Application.Current.TryFindResource("Error").ToString();
                }
            }
        }
    }

    public class ZeroCardListItem
    {
        public int Value;

        public ZeroCardListItem(int newValue)
        {
            Value = newValue;
        }

        public override string ToString()
        {
            switch (Value)
            {
                case 0:
                {
                    return Application.Current.TryFindResource("Green with circle").ToString();
                }
                case 1:
                {
                    return Application.Current.TryFindResource("Green usual").ToString();
                }
                case 2:
                {
                    return Application.Current.TryFindResource("Yellow with circle").ToString();
                }
                case 3:
                {
                    return Application.Current.TryFindResource("Yellow usual").ToString();
                }
                case 4:
                {
                    return Application.Current.TryFindResource("Create me").ToString();
                }
                case 5:
                {
                    return Application.Current.TryFindResource("Gift card").ToString();
                }
                case 6:
                {
                    return Application.Current.TryFindResource("Book").ToString();
                }
                case 7:
                {
                    return Application.Current.TryFindResource("Touch").ToString();
                }
                case 8:
                {
                    return Application.Current.TryFindResource("Thank You").ToString();
                }
                case 9:
                {
                    return Application.Current.TryFindResource("Guide").ToString();
                }
                case 10:
                {
                    return Application.Current.TryFindResource("Joke").ToString();
                }
                default:
                {
                    return Application.Current.TryFindResource("Error").ToString();
                }
            }
        }
    }
}