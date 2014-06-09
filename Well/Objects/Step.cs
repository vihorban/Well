using System.Collections.Generic;

namespace Well.Objects
{
    public class Movement
    {
        public string From;
        public string To;
    }

    public class Step
    {
        public List<Movement> Movements;
        public SuitEnum? DisabledSuit;
        public bool TopCountDecrased;
        public int ScoreIncreased;

        public Step()
        {
            Movements = new List<Movement>();
            DisabledSuit = null;
            TopCountDecrased = false;
            ScoreIncreased = 0;
        }

        public void Add(string from, string to)
        {
            Movements.Add(new Movement {From = from, To = to});
        }
    }
}