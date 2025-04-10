using System;
using IronPlus.Interfaces;

namespace IronPlus.Validation
{
    public class IsNameForBarbellWithinLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var name = value as string;

            if (name.Length > 15)
            {
                return false;
            }

            if (name.Length < 1)
            {
                return false;
            }

            return true;
        }
    }
}
