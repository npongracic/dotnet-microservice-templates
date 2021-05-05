using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.ModelBinders
{
    public class NullableDateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None) {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (value == null) {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value, value);
                bindingContext.Result = ModelBindingResult.Success(value);
                return Task.CompletedTask;
            }

            if (DateTime.TryParse(value, valueProviderResult.Culture, DateTimeStyles.None, out var dateTime)) {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value.Trim(), value.Trim());
                bindingContext.Result = ModelBindingResult.Success(dateTime);
            }

            return Task.CompletedTask;
        }
    }
}