using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Test
{
    static class Utils
    {
        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static IDictionary<string, object> ToDictionary(this object value)
        {
            IDictionary<string, object> expando = new Dictionary<string, object>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                expando.Add(property.Name, property.GetValue(value));
            }
            return expando;
        }

        public static void Throws<T>(Action func) where T : Exception
        {
            var exceptionThrown = false;
            try
            {
                func.Invoke();
            }
            catch (T)
            {
                exceptionThrown = true;
            }

            if (!exceptionThrown)
            {
                throw new Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException(
                    string.Format("An exception of type {0} was expected, but not thrown", typeof(T))
                    );
            }
        }
    }
}
