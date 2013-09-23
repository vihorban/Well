using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using LocalizableAttribute;

namespace Well.Objects
{
    public enum SuitEnum { Clubs, Diamonds, Hearts, Spades, Any }

    public class CardStyleListItem
    {
        public int value;
        public CardStyleListItem(int newValue)
        {
            value = newValue;
        }
        public override string ToString()
        {
            switch (value)
            {
                case 0:
                    {
                        return Application.Current.FindResource("Casulal").ToString();
                    }
                case 1:
                    {
                        return Application.Current.FindResource("Modern").ToString();
                    }
                case 2:
                    {
                        return Application.Current.FindResource("MassEffect").ToString();
                    }
                case 3:
                    {
                        return Application.Current.FindResource("Snap2Objects").ToString();
                    }
                case 4:
                    {
                        return Application.Current.FindResource("Emotions").ToString();
                    }
                case 5:
                    {
                        return Application.Current.FindResource("Microsoft").ToString();
                    }
                default:
                    {
                        return Application.Current.FindResource("Error").ToString();
                    }
            }
        }
    }

    public enum Languages : int
    {
        [LocalizableDescription("Ukrainian")]
        Ukrainian = 1,
        [LocalizableDescription("Russian")]
        Russian = 2,
        [LocalizableDescription("English")]
        English = 3,
    }

    public enum Cancellation : int
    {
        [LocalizableDescription("OneCancellationEnable")]
        OneCancellationEnable = 1,
        [LocalizableDescription("TwoCancellationsEnable")]
        TwoCancellationsEnable = 2,
        [LocalizableDescription("InfinityCancellationsEnable")]
        InfinityCancellationsEnable = Int32.MaxValue,
    }

    public class BackSuitListItem
    {
        public int value;
        public BackSuitListItem(int newValue)
        {
            value = newValue;
        }
        public override string ToString()
        {
            switch (value)
            {
                case 0:
                    {
                        return Application.Current.FindResource("Raster").ToString();
                    }
                case 1:
                    {
                        return Application.Current.FindResource("Lake").ToString();
                    }
                case 2:
                    {
                        return Application.Current.FindResource("Bird").ToString();
                    }
                case 3:
                    {
                        return Application.Current.FindResource("Field").ToString();
                    }
                case 4:
                    {
                        return Application.Current.FindResource("Tree").ToString();
                    }
                case 5:
                    {
                        return Application.Current.FindResource("Casual").ToString();
                    }
                case 6:
                    {
                        return Application.Current.FindResource("Space").ToString();
                    }
                case 7:
                    {
                        return Application.Current.FindResource("Earth").ToString();
                    }
                case 8:
                    {
                        return Application.Current.FindResource("Night city").ToString();
                    }
                case 9:
                    {
                        return Application.Current.FindResource("Quay").ToString();
                    }
                case 10:
                    {
                        return Application.Current.FindResource("Sunset").ToString();
                    }
                case 11:
                    {
                        return Application.Current.FindResource("Wing @Mass Effect").ToString();
                    }
                case 12:
                    {
                        return Application.Current.FindResource("Star @Mass Effect").ToString();
                    }
                case 13:
                    {
                        return Application.Current.FindResource("N7 @Mass Effect").ToString();
                    }
                case 14:
                    {
                        return Application.Current.FindResource("Symbol @Mass Effect").ToString();
                    }
                case 15:
                    {
                        return Application.Current.FindResource("Classic @Emotions").ToString();
                    }
                case 16:
                    {
                        return Application.Current.FindResource("Classic @Microsoft").ToString();
                    }
                case 17:
                    {
                        return Application.Current.FindResource("Hearts @Microsoft").ToString();
                    }
                case 18:
                    {
                        return Application.Current.FindResource("Seasons @Microsoft").ToString();
                    }
                default:
                    {
                        return Application.Current.FindResource("Error").ToString();
                    }
            }
        }
    }

    public class EmptyCardListItem
    {
        public int value;
        public EmptyCardListItem(int newValue)
        {
            value = newValue;
        }
        public override string ToString()
        {
            switch (value)
            {
                case 0:
                    {
                        return Application.Current.FindResource("Green").ToString();
                    }
                case 1:
                    {
                        return Application.Current.FindResource("Yellow").ToString();
                    }
                case 2:
                    {
                        return Application.Current.FindResource("Red").ToString();
                    }
                case 3:
                    {
                        return Application.Current.FindResource("Blue").ToString();
                    }
                case 4:
                    {
                        return Application.Current.FindResource("White").ToString();
                    }
                case 5:
                    {
                        return Application.Current.FindResource("Pink").ToString();
                    }
                case 6:
                    {
                        return Application.Current.FindResource("Gray").ToString();
                    }
                case 7:
                    {
                        return Application.Current.FindResource("Old").ToString();
                    }
                default:
                    {
                        return Application.Current.FindResource("Error").ToString();
                    }
            }
        }
    }

    public class ZeroCardListItem
    {
        public int value;
        public ZeroCardListItem(int newValue)
        {
            value = newValue;
        }
        public override string ToString()
        {
            switch (value)
            {
                case 0:
                    {
                        return Application.Current.FindResource("Green with circle").ToString();
                    }
                case 1:
                    {
                        return Application.Current.FindResource("Green usual").ToString();
                    }
                case 2:
                    {
                        return Application.Current.FindResource("Yellow with circle").ToString();
                    }
                case 3:
                    {
                        return Application.Current.FindResource("Yellow usual").ToString();
                    }
                case 4:
                    {
                        return Application.Current.FindResource("Create me").ToString();
                    }
                case 5:
                    {
                        return Application.Current.FindResource("Gift card").ToString();
                    }
                case 6:
                    {
                        return Application.Current.FindResource("Book").ToString();
                    }
                case 7:
                    {
                        return Application.Current.FindResource("Touch").ToString();
                    }
                case 8:
                    {
                        return Application.Current.FindResource("Thank You").ToString();
                    }
                case 9:
                    {
                        return Application.Current.FindResource("Guide").ToString();
                    }
                case 10:
                    {
                        return Application.Current.FindResource("Joke").ToString();
                    }
                default:
                    {
                        return Application.Current.FindResource("Error").ToString();
                    }
            }
        }
    }
}
