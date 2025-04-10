using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsRpeWithinRangeRpeChartRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var weight = value as double?;

            if (weight > 10)
            {
                return false;
            }

            if (weight < 5)
            {
                return false;
            }

            return true;
        }
    }
}
