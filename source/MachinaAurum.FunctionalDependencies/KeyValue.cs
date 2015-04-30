using System;
using System.Collections.Generic;

namespace MachinaAurum.FunctionalDependencies
{
    public class KeyValueFunctions<TKey, TValue>
    {
        public Func<TKey, TValue> Seek { get; private set; }

        public KeyValueFunctions(IDictionary<TKey, TValue> dictionary)
        {
            Seek = new Func<TKey, TValue>(x =>
            {
                TValue value = default(TValue);
                dictionary.TryGetValue(x, out value);
                return value;
            });
        }
    }

    public static class KeyValueExtensions
    {
        public static KeyValueFunctions<TKey, TValue> GetFunctions<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new KeyValueFunctions<TKey, TValue>(dictionary);
        }
    }
}
