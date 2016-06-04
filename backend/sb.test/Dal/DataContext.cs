using System;
using System.Collections.Generic;
using System.Linq;

namespace sb.test.dal
{
    class DataContext
    {
        static Dictionary<string, object> _data = new Dictionary<string, object>();

        static string GetKey(Type t)
        {
            return t.FullName;
        }

        public static IEnumerable<T> GetData<T>()
        {
            var key = GetKey(typeof(T));

            object result = null;

            if (_data.TryGetValue(key, out result))
            {
                return (IEnumerable<T>)result;
            }

            return Enumerable.Empty<T>();
        }

        public static void AddData<T>(IEnumerable<T> data)
        {
            var key = GetKey(typeof(T));
            _data.Add(key, data);
        }

        public static void Clear()
        {
            _data.Clear();
        }
    }
}
