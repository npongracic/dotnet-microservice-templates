using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace SC.API.CleanArchitecture.API.Validators
{
    public sealed class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string _extensions;
        private readonly string _otherProperty;

        public AllowedFileExtensionsAttribute(string extensions, string otherProperty = null)
        {
            _extensions = extensions;
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

            if (!_extensions.Contains(Path.GetExtension(file.FileName).ToLower())) {
                var message = FormatErrorMessage(_extensions);

                return new ValidationResult(message);
            }

            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name.Replace(";", ",").Replace(".", " "));
        }
    }
}