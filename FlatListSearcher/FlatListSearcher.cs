using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatListSearcher
{
    public static class Searcher
    {
        static T Find<T>(this List<T> source, Predicate<T> findIndex, Func<T, bool> predicate)
            where T : ILevel
        {
            var currentIndex = source.FindIndex(findIndex);
            for (var i = currentIndex - 1; i >= 0; i--)
            {
                if (predicate(source[i]))
                    return source[i];
            }
            return default(T);
        }

        public static bool IsRoot<T>(this List<T> source, Predicate<T> findIndex, out T currentItem)
            where T : ILevel
        {
            var currentIndex = source.FindIndex(findIndex);
            currentItem = source[currentIndex];
            return currentItem.Level == 0;
        }

        public static T FindRoot<T>(this List<T> source, Predicate<T> findIndex)
            where T : ILevel
        {
            T currentItem;
            if (source.IsRoot(findIndex, out currentItem))
                return currentItem;

            return source.Find(findIndex, item => item.Level == 0);
        }

        public static T FindParent<T>(this List<T> source, Predicate<T> findIndex)
            where T : ILevel
        {
            T currentItem;
            if (source.IsRoot(findIndex, out currentItem))
                return default(T);

            return source.Find(findIndex, section => section.Level == currentItem.Level - 1);
        }

        public static List<T> FindFirstLevelChildren<T>(this List<T> source, Predicate<T> findIndex)
            where T : ILevel
        {
            var result = new List<T>();
            var index = source.FindIndex(findIndex);
            foreach (var item in source.Skip(index + 1)
                .TakeWhile(s => s.Level > source[index].Level)
                .Where(s => s.Level == source[index].Level + 1))
                result.Add(item);

            return result;
        }

        public static List<T> FindChildren<T>(this List<T> source, Predicate<T> findIndex)
            where T : ILevel
        {
            var result = new List<T>();
            var index = source.FindIndex(findIndex);

            foreach (var item in source
                .Skip(index + 1)
                .TakeWhile(s => s.Level > source[index].Level))
                result.Add(item);

            return result;
        }

        public static bool HasChildren<T>(List<T> source, Predicate<T> findIndex)
            where T : ILevel
        {
            var index = source.FindIndex(findIndex);
            return source[index + 1].Level > source[index].Level;
        }
    }
}
