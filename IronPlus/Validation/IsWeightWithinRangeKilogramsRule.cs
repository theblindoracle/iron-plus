using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsWeightWithinRangeKilogramsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var weight = value as double?;

            if (weight > 501)
            {
                return false;
            }

            if (weight < 20)
            {
                return false;
            }

            return true;

        }
    }
}
