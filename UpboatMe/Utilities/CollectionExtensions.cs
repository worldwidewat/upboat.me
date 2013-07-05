using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpboatMe.Utilities
{
    public static class CollectionExtensions
    {
        public static void Add<T>(this ICollection<T> collection, params T[] items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}