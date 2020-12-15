using System;
using System.Globalization;
using System.Web.Mvc;

namespace WebApp.Helpers
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            return valueProviderResult == null
                ? base.BindModel(controllerContext, bindingContext)
                : Convert.ToDecimal((string.IsNullOrEmpty(valueProviderResult.AttemptedValue) ? "0" : valueProviderResult.AttemptedValue), CultureInfo.GetCultureInfo("en-US"));
        }
    }
}