﻿using System.Collections.Generic;

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

        public Step()
        {
            Movements = new List<Movement>();
        }

        public void Add(string from, string to)
        {
            Movements.Add(new Movement {From = from, To = to});
        }
    }
}