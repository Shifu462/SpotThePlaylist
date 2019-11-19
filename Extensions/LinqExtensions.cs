using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotThePlaylist.Web.Extensions
{
    public static class LinqExtensions
    {
        private static Random rnd = new Random();

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            var count = enumerable.Count();
            return enumerable.ElementAtOrDefault(rnd.Next(0, count));
        }

    }
}