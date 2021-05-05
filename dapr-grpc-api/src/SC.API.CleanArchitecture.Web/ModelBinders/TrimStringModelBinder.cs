using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.API.ModelBinders
{
    public class TrimStringModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None) {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value)) {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value.Trim(), value.Trim());
            bindingContext.Result = ModelBindingResult.Success(value.Trim());

            return Task.CompletedTask;
        }
    }
}