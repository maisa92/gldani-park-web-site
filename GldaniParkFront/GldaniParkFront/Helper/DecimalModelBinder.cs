using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GldaniParkFront.Helper
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerCntxt, ModelBindingContext bindingCntxt)
        {
            //Get the value from the context
            ValueProviderResult input = bindingCntxt.ValueProvider.GetValue(bindingCntxt.ModelName);

            ModelState modelState = new ModelState { Value = input };

            //Specify the numberstyle to parse the deciaml to convert $123.45 to 123.45
            object res = decimal.Parse(input.AttemptedValue,
                NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, new CultureInfo("en-US"));

            bindingCntxt.ModelState.Add(bindingCntxt.ModelName, modelState);
            return res;
        }
    }
}