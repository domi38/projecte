using System;
using System.Web.Mvc;

namespace Projecte.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrjtDateTimePropertyAttribute : Attribute
    {
        public string Format { get; private set; }

        public PrjtDateTimePropertyAttribute(string format)
        {
            this.Format = format;
        }
    }

    public class PrjtDefaultModelBinder : DefaultModelBinder
    {
        protected override object GetPropertyValue(
               ControllerContext controllerContext,
               ModelBindingContext bindingContext,
               System.ComponentModel.PropertyDescriptor propertyDescriptor,
               IModelBinder propertyBinder)
        {
            if (((controllerContext.HttpContext).Request).ContentType.Contains("application/json"))
            {
                var propertyType = propertyDescriptor.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var provider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                    if (provider != null
                        && provider.RawValue != null
                        && Type.GetTypeCode(provider.RawValue.GetType()) == TypeCode.Int32)
                    {
                        var value = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(provider.AttemptedValue, bindingContext.ModelMetadata.ModelType);
                        return value;
                    }
                }
                else
                {
                    return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
                }
            }
            return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
        }
    }
}