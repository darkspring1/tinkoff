using System;
using System.Collections.Generic;
using System.Linq;


namespace Test.Dal
{
    class DataContext
    {
        static readonly Dictionary<string, object> Data = new Dictionary<string, object>();

        static string GetKey(Type t)
        {
            return t.FullName;
        }

        public static IEnumerable<T> GetData<T>()
        {
            var key = GetKey(typeof(T));

            object result = null;

            if (Data.TryGetValue(key, out result))
            {
                return (IEnumerable<T>)result;
            }

            return Enumerable.Empty<T>();
        }

        public static void AddData<T>(IEnumerable<T> data)
        {
            var key = GetKey(typeof(T));
            Data.Add(key, data);
        }

        public static void Clear()
        {
            Data.Clear();
        }
    }
}
