using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsWeightForBarbellWithinRangePoundsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var weight = value as int?;

            if (weight > 95)
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
