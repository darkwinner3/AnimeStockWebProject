using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace AnimeStockWebProject.ModelBinders.DecimalModelBinder
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result != ValueProviderResult.None && !string.IsNullOrWhiteSpace(result.FirstValue))
            {
                decimal parsed = 0m;
                bool succeeded = false;

                try
                {
                    string decValue = result.FirstValue;
                    decValue = decValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decValue = decValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    parsed = Convert.ToDecimal(decValue);
                    succeeded = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if (succeeded)
                {
                    bindingContext.Result = ModelBindingResult.Success(parsed);
                }
            }

            return Task.CompletedTask;
        }
    }
}
