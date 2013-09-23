using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ImageSplitter.Objects;

namespace ImageSplitter.Algorithms
{
    public class DeckGenerator
    {
        public static List<Card> generateFromAceToKing()
        {
            List<Card> resultDeck= new List<Card>();
            SuitEnum[] suits = { SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades, SuitEnum.Diamonds };
            foreach (SuitEnum s in suits)
            {
                for (int i = 1; i < 14; ++i)
                {
                    resultDeck.Add(new Card { value = i, suit = s });
                }
            }
            return resultDeck;     
        }

        public static List<Card> generateFromAceToTwo()
        {
            List<Card> resultDeck = new List<Card>();
            SuitEnum[] suits = { SuitEnum.Spades, SuitEnum.Diamonds, SuitEnum.Clubs, SuitEnum.Hearts };
            foreach (SuitEnum s in suits)
            {
                resultDeck.Add(new Card { value = 1, suit = s });
                for (int i = 13; i > 1; --i)
                {
                    resultDeck.Add(new Card { value = i, suit = s });
                }
            }
            return resultDeck;
        }

        public static List<Card> generateFromTwoToAce()
        {
            List<Card> resultDeck = new List<Card>();
            SuitEnum[] suits = { SuitEnum.Diamonds, SuitEnum.Clubs, SuitEnum.Hearts, SuitEnum.Spades };
            foreach (SuitEnum s in suits)
            {
                for (int i = 2; i < 14; ++i)
                {
                    resultDeck.Add(new Card { value = i, suit = s });
                }
                resultDeck.Add(new Card { value = 1, suit = s });
            }
            return resultDeck;
        }

        public static List<Card> generateMicrosoftDeck()
        {
            List<Card> resultDeck = new List<Card>();
            SuitEnum[] suits1 = { SuitEnum.Hearts, SuitEnum.Diamonds,SuitEnum.Clubs, SuitEnum.Spades  };
            SuitEnum[] suits2 = { SuitEnum.Clubs, SuitEnum.Diamonds, SuitEnum.Spades, SuitEnum.Hearts };
            List<Card> helpDeck = new List<Card>();
            foreach (SuitEnum s in suits2)
            {
                for (int i = 11; i < 14; i++)
                {
                    helpDeck.Add(new Card { value = i, suit = s });
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (i < 5)
                {
                    foreach (SuitEnum s in suits1)
                    {
                        resultDeck.Add(new Card { value = i * 2 + 1, suit = s });
                        resultDeck.Add(new Card { value = i * 2 + 2, suit = s });
                    }
                }
                resultDeck.Add(helpDeck[i * 2]);
                resultDeck.Add(helpDeck[i * 2 + 1]);
            }
            return resultDeck;
        }
    }
}
