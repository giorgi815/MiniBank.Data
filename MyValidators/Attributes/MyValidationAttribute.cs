using System;
using System.Collections.Generic;
using System.Text;

namespace MyValidators.Attributes
{
    public abstract class MyValidationAttribute : Attribute
    {
        public abstract void Validate(object value, object instance, string propertyName);
    }
}
