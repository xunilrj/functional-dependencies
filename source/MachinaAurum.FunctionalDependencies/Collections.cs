using System;
using System.Collections.Generic;

namespace MachinaAurum.FunctionalDependencies
{
    public class CollectionFunctions<T>
    {
        public Action<T> Add { get; private set; }
        public Action<T> Remove { get; private set; }

        public CollectionFunctions(ICollection<T> collection)
        {
            Add = new Action<T>(collection.Add);
            Remove = new Action<T>((x) => collection.Remove(x));
        }
    }

    public static class CollectionExtensions
    {
        public static CollectionFunctions<T> GetFunctions<T>(this ICollection<T> collection)
        {
            return new CollectionFunctions<T>(collection);
        }
    }
}
