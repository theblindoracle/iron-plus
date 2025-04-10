using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsWeightWithinRangePoundsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var weight = value as double?;

            if (weight > 1104)
            {
                return false;
            }

            if (weight < 45)
            {
                return false;
            }

            return true;

        }
    }
}
