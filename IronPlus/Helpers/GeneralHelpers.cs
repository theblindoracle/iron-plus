using System;
namespace IronPlus.Helpers
{
    public static class GeneralHelpers
    {
        public static double RoundValueToNearest(double value, double roundTo)
        {
            return roundTo == 0 ? value : Math.Round(value / roundTo) * roundTo;
        }

        public static double RoundValueDownToNearest(double value, double roundTo)
        {
            return roundTo == 0 ? value : Math.Floor(value / roundTo) * roundTo;
        }

        public static double RoundValueUpToNearest(double value, double roundTo)
        {
            return roundTo == 0 ? value : Math.Ceiling(value / roundTo) * roundTo;
        }
    }
}
