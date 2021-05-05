using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.ModelBinders
{
    public class InvariantDecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None) {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue.Replace(',', '.');

                if (decimal.TryParse(valueAsString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var result)) {
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }

            return Task.CompletedTask;
        }
    }
}
