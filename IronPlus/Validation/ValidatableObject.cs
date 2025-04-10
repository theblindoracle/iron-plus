using System;
using System.Collections.Generic;
using System.Linq;
using IronPlus.Interfaces;
using IronPlus.ViewModels.Base;

namespace IronPlus.Validation
{
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        readonly List<IValidationRule<T>> validations;

        public List<IValidationRule<T>> Validations => validations;

        List<string> errors;
        public List<string> Errors
        {
            get
            {
                return errors;
            }
            set
            {
                SetProperty(ref errors, value);
            }
        }

        private T value;
        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                SetProperty(ref this.value, value);
            }
        }

        bool isValid;
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                SetProperty(ref isValid, value);
            }
        }

        public ValidatableObject()
        {
            isValid = true;
            errors = new List<string>();
            validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
