using System;
using System.Collections.Generic;
using System.Threading;

namespace EcoMine.Common.Extensions
{
    public static class ListExtensions
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

            if (list.Count < randomOption.RandomCount)
                throw new ArgumentException("Count random must be greater than list!");

            List<T> listCopy = new(list);
            listCopy.RemoveAll(randomOption.Ignores.Contains);

            List<RandomResult<T>> listRandom = new();
            for (int i = 0; i < randomOption.RandomCount; i++)
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
    }

    public struct RandomResult<T>
    {
        public int Index;
        public T Value;
    }

    public class RandomOption<T>
    {
        public int RandomCount = 1;
        public List<T> Ignores = new();
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