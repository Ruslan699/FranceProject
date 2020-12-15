using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;

namespace WebApp.Helpers
{
    public class DatetimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != null && 
                DateTime.TryParseExact(valueProviderResult.AttemptedValue, "dd-mm-yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            return BindModel(controllerContext, bindingContext);
        }
    }
}