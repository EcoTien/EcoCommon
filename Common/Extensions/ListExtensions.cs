using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

namespace EcoMine.Common.Extensions
{
    internal static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static List<RandomResult<T>> Random<T>(this List<T> list, RandomOption<T> randomOption = null)
        {
            randomOption ??= new RandomOption<T>();

            if (list.Count < randomOption.randomCount)
                throw new ArgumentException("Count random must be greater than list!");

            List<T> listCopy = new(list);
            listCopy.RemoveAll(randomOption.ignores.Contains);

            List<RandomResult<T>> listRandom = new();
            for (int i = 0; i < randomOption.randomCount; i++)
            {
                int randomCurrentIndex = ThreadSafeRandom.ThisThreadsRandom.Next(0, listCopy.Count);
                T valueRandom = listCopy[randomCurrentIndex];
                listRandom.Add(new RandomResult<T> { Value = valueRandom, Index = list.IndexOf(valueRandom) });
                listCopy.RemoveAt(randomCurrentIndex);
            }

            return listRandom;
        }
        
        public static RandomResult<T> Random<T>(this List<T> list)
        {
            int randomIndex = ThreadSafeRandom.ThisThreadsRandom.Next(0, list.Count);
            return new RandomResult<T>
            {
                Index = randomIndex,
                Value = list[randomIndex]
            };
        }

        public static bool Contains<T>(this List<T> list, List<T> collection)
        {
            foreach (var obj in list)
            {
                if (collection.Contains(obj))
                    return true;
            }
            return false;
        }

        public static T FindSame<T>(this List<T> list, List<T> collection, T ignore = default)
        {
            foreach (T obj in list)
            {
                if (collection.Contains(obj) && !obj.Equals(ignore))
                    return obj;
            }
            return default;
        }
        
        public static T FindSameWithPriorities<T>(this List<T> list, List<T> collection, T priorities)
        {
            T objSelect = default;
            
            foreach (T obj in list)
            {
                if (obj.Equals(priorities) && collection.Contains(obj))
                    return obj;
                if (collection.Contains(obj))
                    objSelect = obj;
            }
            return objSelect;
        }
        
        public static T FindSame<T>(this List<T> list, List<T> collection, List<T> ignore)
        {
            foreach (T obj in list)
            {
                if (collection.Contains(obj) && !ignore.Contains(obj))
                    return obj;
            }
            return default;
        }
    }

    public struct RandomResult<T>
    {
        public int Index;
        public T Value;
    }

    public class RandomOption<T>
    {
        public int randomCount;
        public List<T> ignores;

        public RandomOption()
        {
            this.randomCount = 1;
            this.ignores = new();
        }

        public RandomOption(int randomCount = 1, List<T> ignores = null)
        {
            this.randomCount = randomCount;
            if (ignores == null)
                this.ignores = new();
            else
                this.ignores = ignores;
        }

        public RandomOption(T ignore)
        {
            this.ignores = new() { ignore };
        }
    }
    
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random _local;

        public static Random ThisThreadsRandom
        {
            get { return _local ??= new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)); }
        }
    }
}