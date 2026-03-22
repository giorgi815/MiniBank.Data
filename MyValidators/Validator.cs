using MyValidators.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MyValidators
{
    public static class Validator
    {
        public static void Validate(object obj)
        {
            Type objType = obj.GetType();
            var allProps = objType.GetProperties();
            foreach (var prop in allProps)
            {
                var value = prop.GetValue(obj);
                var validationAttributes = prop.GetCustomAttributes<MyValidationAttribute>();
                foreach (var item in validationAttributes) item.Validate(value, obj, prop.Name);
            }
        }
    }
}
