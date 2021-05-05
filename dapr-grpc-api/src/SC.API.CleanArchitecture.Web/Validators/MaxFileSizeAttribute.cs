using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SC.API.CleanArchitecture.API.Validators
{
    public sealed class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly string _otherProperty;

        public MaxFileSizeAttribute(int maxFileSize, string otherProperty = null)
        {
            _maxFileSize = maxFileSize;
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file;

            if (_otherProperty != null) {
                if (validationContext.ObjectInstance.GetType().GetProperty(_otherProperty).PropertyType != typeof(IFormFile))
                    throw new InvalidOperationException("Other property must have type equals to HttpPostedFileBase");

                file = validationContext.ObjectInstance.GetType().GetProperty(_otherProperty).GetValue(validationContext.ObjectInstance) as IFormFile;
            }
            else {
                file = value as IFormFile;
            }

            if (file.Length > (_maxFileSize * 1024 * 1024)) {
                var message = FormatErrorMessage(_maxFileSize.ToString());

                return new ValidationResult(message);
            }

            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
    }
}
