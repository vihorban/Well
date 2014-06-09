using System.Collections.Generic;

namespace Well.Objects
{
    public class SavedGame
    {
        public List<SuitEnum> AvailableSuits;
        public DeckCollection Collection;
        public bool IsGameOver;
        public List<Step> Steps;
        public int TopCount;
    }
}