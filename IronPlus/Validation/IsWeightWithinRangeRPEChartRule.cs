using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsWeightWithinRangeRPEChartRule<T> : IValidationRule<T>
    {
        public IsWeightWithinRangeRPEChartRule()
        {
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var weight = value as double?;

            if (weight > 10000)
            {
                return false;
            }

            if (weight <= 0)
            {
                return false;
            }

            return true;

        }
    }
}
