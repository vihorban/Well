using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class Card
    {
        public int value;
        public SuitEnum suit;
        public string deckName;

        public string path(string folder)
        {
            string extension = ".png";
            string result = folder;
            if (value > 0)
            {
                result += value.ToString() + suit.ToString() + extension;
            }
            else if (value == -1)
                result = "commonCards/None" + Properties.Settings.Default.ZeroCardSelectedNumber.ToString() + extension;
            else if (value == -2)
                result = "commonCards/Back" + Properties.Settings.Default.BackSuitSelectedNumber.ToString() + extension;
            else
                result = "commonCards/Empty" + Properties.Settings.Default.EmptyCardSelectedNumber.ToString() + extension;
            return result;
        }

        public static Card EmptyCard()
        {
            return new Card() { value = 0, suit= SuitEnum.Any, deckName = "Any" };
        }

        public static Card ZeroCard()
        {
            return new Card() { value = -1, suit = SuitEnum.Any, deckName = "Any" };
        }

        public static Card BackCard()
        {
            return new Card() { value = -2, suit = SuitEnum.Any, deckName = "Any" };
        }
    }
}
