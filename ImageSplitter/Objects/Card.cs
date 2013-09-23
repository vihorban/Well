using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageSplitter.Objects
{
    public enum SuitEnum { Clubs, Diamonds, Hearts, Spades}

    public class Card
    {
        public int value;
        public SuitEnum suit;

        public string path()
        {
            return "cards//" + value.ToString() + suit.ToString();
        }
    }

}
