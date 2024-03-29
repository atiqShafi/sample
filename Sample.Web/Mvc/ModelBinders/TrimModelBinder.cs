using System.Web.Mvc;

namespace Sample.Web.Mvc.ModelBinders
{
    public class TrimModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == null || string.IsNullOrEmpty(valueResult.AttemptedValue))
                return null;
            return valueResult.AttemptedValue.Trim();
        }
    }
}