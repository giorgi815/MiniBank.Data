using System;
using System.Collections.Generic;
using System.Text;

namespace MyValidators.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class MyStringLengthAttribute : MyValidationAttribute
    {
        private int stringLength;

        public MyStringLengthAttribute(int length)
        {
            stringLength = length;
        }

        public override void Validate(object value, object instance, string propertyName)
        {
            if (propertyName.Length != stringLength)
            {
                throw new ArgumentException();
            }
        }

    }
}
