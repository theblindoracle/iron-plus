using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsWeightForBarbellWithinRangeKilogramsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var weight = value as int?;

            if (weight > 45)
            {
                return false;
            }

            if (weight < 2.5)
            {
                return false;
            }

            return true;
        }
    }
}
