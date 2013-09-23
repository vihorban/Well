using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Well.Objects
{
    public class Movement
    {
        public string From;
        public string To;
    }

    public class Step
    {
        public List<Movement> movements;
        public Step()
        {
            movements = new List<Movement>();
        }
        public void add(string from, string to)
        {
            movements.Add(new Movement { From = from, To = to });
        }
    }
}
