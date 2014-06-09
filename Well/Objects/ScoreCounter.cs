namespace Well.Objects
{
    public class ScoreCounter
    {
        public const int BackDeckOpened = -50;
        public const int FromBorderToResult = 25;
        public const int FromBorderToWarehouse = 5;
        public const int FromMiddleToBorder = 10;
        public const int FromMiddleToResult = 25;
        public const int FromMiddleToWarehouse = 10;
        public const int FromTopToWarehouse = 10;
        public const int FromTopToBorder = 15;
        public const int FromTopToResult = 25;
        public const int FromWarehouseToResult = 75;
        public const int FromWarehouseToBorder = -10;


        public static int CountChange(Deck from, Deck to)
        {
            if (from.Type == DeckType.Border)
            {
                switch (to.Type)
                {
                    case DeckType.Result:
                        return FromBorderToResult;
                    case DeckType.Warehouse:
                        return FromBorderToWarehouse;
                }
            }
            if (from.Type == DeckType.Middle)
            {
                switch (to.Type)
                {
                    case DeckType.Border:
                        return FromMiddleToBorder;
                    case DeckType.Result:
                        return FromMiddleToResult;
                    case DeckType.Warehouse:
                        return FromMiddleToWarehouse;
                }
            }
            if (from.Type == DeckType.Top)
            {
                switch (to.Type)
                {
                    case DeckType.Border:
                        return FromTopToBorder;
                    case DeckType.Result:
                        return FromTopToResult;
                    case DeckType.Warehouse:
                        return FromTopToWarehouse;
                }
            }
            if (from.Type == DeckType.Warehouse)
            {
                switch (to.Type)
                {
                    case DeckType.Border:
                        return FromWarehouseToBorder;
                    case DeckType.Result:
                        return FromWarehouseToResult;
                }
            }
            return 0;
        }
    }
}