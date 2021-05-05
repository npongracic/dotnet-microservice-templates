using System;
using System.ComponentModel.DataAnnotations;

namespace SC.API.CleanArchitecture.API.Validators
{
    public sealed class DependRequiredAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;
        private readonly bool _inverse;

        public DependRequiredAttribute(string otherProperty, bool inverse = false)
        {
            _otherProperty = otherProperty;
            _inverse = inverse;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance.GetType().GetProperty(_otherProperty).PropertyType != typeof(bool))
                throw new InvalidOperationException("Other property must have type equals to bool");

            if ((bool)validationContext.ObjectInstance.GetType().GetProperty(_otherProperty).GetValue(validationContext.ObjectInstance) && value == null) {
                var message = string.Format(ErrorMessage, validationContext.DisplayName);

                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}