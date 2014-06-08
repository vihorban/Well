using Well.Properties;

namespace Well.Objects
{
    public class Card
    {
        public const string ExtensionPng = ".png";
        public string DeckName;
        public SuitEnum Suit;
        public int Value;

        public string Path(string folder)
        {
            string result = folder;
            if (Value > 0)
            {
                result += Value + Suit.ToString() + ExtensionPng;
            }
            else
                switch (Value)
                {
                    case -1:
                        result = "commonCards/None" + Settings.Default.ZeroCardSelectedNumber + ExtensionPng;
                        break;
                    case -2:
                        result = "commonCards/Back" + Settings.Default.BackSuitSelectedNumber + ExtensionPng;
                        break;
                    default:
                        result = "commonCards/Empty" + Settings.Default.EmptyCardSelectedNumber + ExtensionPng;
                        break;
                }
            return result;
        }

        public static Card EmptyCard()
        {
            return new Card {Value = 0, Suit = SuitEnum.Any, DeckName = "Any"};
        }

        public static Card ZeroCard()
        {
            return new Card {Value = -1, Suit = SuitEnum.Any, DeckName = "Any"};
        }

        public static Card BackCard()
        {
            return new Card {Value = -2, Suit = SuitEnum.Any, DeckName = "Any"};
        }
    }
}