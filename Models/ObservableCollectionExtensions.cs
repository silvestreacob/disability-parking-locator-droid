using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using dpark.Models;

namespace dpark.Models
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var i in items)
            {
                collection.Add(i);
            }
        }

        public static void AddRange<T, K>(this ObservableCollection<Grouping<T, K>> collection, IEnumerable<T> items, string propertyName)
        {
            // If the specified propertyName does not exist on type T, throw an ArgumentException.
            if (typeof(T).GetRuntimeProperties().All(propertyInfo => propertyInfo.Name != propertyName))
            {
                throw new ArgumentException(String.Format("Type '{0}' does not have a property named '{1}'", typeof(T).Name, propertyName));
            }

            // Group the items in T by the different values of K.
            var groupings = items.GroupBy(t => t.GetType().GetRuntimeProperties().Single(propertyInfo => propertyInfo.Name == propertyName).GetValue(t, null));

            // Add new Grouping<T,K> items to the ObservableCollection<Grouping<T,K>> collection.
            collection.AddRange(groupings.Select(grouping => new Grouping<T, K>(grouping, (K)grouping.Key)));
        }
    }
}
