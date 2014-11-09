using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SolidGui
{
    public static class Extensions
    {

        // Extension methods for IDictionary

        /// <summary>
        /// Extension method: If the value exists, returns it.
        /// If not, default is both set and returned.
        /// </summary>
        public static V GetSetDefault<K, V>
            (this IDictionary<K, V> dictionary, K key, V def)
        {
            return DefaultOrGetSetValue(dictionary, key, def, true);
        }

        /// <summary>
        /// Extension method: If the value exists, returns it.
        /// If not, default is returned.
        /// </summary>
        public static V GetOrDefault<K, V>
            (this IDictionary<K, V> dictionary, K key, V def)
        {
            return DefaultOrGetSetValue(dictionary, key, def, false);
        }

        private static V DefaultOrGetSetValue<K, V>
            (this IDictionary<K, V> dictionary, K key, V def, bool set)
        {
            V ret;
            if (!dictionary.TryGetValue(key, out ret) && set)
            {
                dictionary[key] = def;
                ret = def;
            }
            return ret;
        }

    }

    //Utils

    public static class EnumUtil
    {

        // http://stackoverflow.com/questions/11240236/enum-values-in-a-list
        public static IEnumerable<T> GetEnumValues<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");
            }
            return (T[])Enum.GetValues(typeof(T));
        }

        //
        public static IEnumerable<T> GetEnumValues2<T>()
        {
            var L = Enum.GetValues(typeof(T)).Cast<T>();
            return L;
        }

    }



}