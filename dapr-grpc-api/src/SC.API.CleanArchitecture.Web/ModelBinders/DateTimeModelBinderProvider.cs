using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace SC.API.CleanArchitecture.API.ModelBinders
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsComplexType && context.Metadata.ModelType == typeof(DateTime)) {
                return new BinderTypeModelBinder(typeof(DateTimeModelBinder));
            }

            return null;
        }
    }
}