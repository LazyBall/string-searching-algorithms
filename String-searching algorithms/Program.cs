using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace String_searching_algorithms
{
    class Program
    {
        static void Main()
        {
            int n = 1000000, l = 4;
            var haystack = DoRandomString(n);
            var needle = DoRandomString(l);

            //var haystack = "GCATCGCAGAGAGTATACAGTACG";
            //var needle = "GCAGAGAG";

            //Console.WriteLine(haystack);
            //Console.WriteLine(needle);

            var watch = new Stopwatch();


            watch.Start();
            Console.WriteLine($"NaiveAlgorithm-FindFirstEntry: {NaiveAlgorithm.FindFirstEntry(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");
            Console.WriteLine();

            watch.Restart();
            foreach (var value in NaiveAlgorithm.FindAllEntries(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"Naive-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();


            watch.Restart();
            Console.WriteLine($"KMP-FindFirstEntry: {KnuthMorrisPrattAlgorithm.FindFirstEntry(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");
            Console.WriteLine();

            watch.Restart();
            foreach (var value in KnuthMorrisPrattAlgorithm.FindAllEntries(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"KMP-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();


            watch.Restart();
            Console.WriteLine($"BoyerMoore-FindFirstEntry: {BoyerMooreAlgorithm.FindFirstEntry(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");
            Console.WriteLine();

            watch.Restart();
            foreach (var value in BoyerMooreAlgorithm.FindAllEntries(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"BoyerMoore-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();
        }

        static string DoRandomString(int length)
        {
            var strBuilder = new StringBuilder(length);
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                strBuilder.Append((char)random.Next(70, 80));
            }
            return strBuilder.ToString();
        }
    }

    static class NaiveAlgorithm
    {
        public static int FindFirstEntry(string needle, string haystack)
        {

            foreach (var element in FindAllEntries(needle, haystack))
            {
                return element;
            }

            return -1;
        }

        public static IEnumerable<int> FindAllEntries(string needle, string haystack)
        {
            int stop = haystack.Length - needle.Length + 1;

            for (int i = 0; i < stop; i++)
            {
                int j = 0;

                while ((j < needle.Length) && (needle[j] == haystack[i + j]))
                {
                    j++;
                }

                if (j == needle.Length)
                {
                    yield return i;
                }
            }

        }
    }

    static class PrefixFunction
    {
        public static int[] Compute(string str)
        {
            var pi = new int[str.Length]; // значения префикс-функции
            pi[0] = 0; // для префикса из нуля и одного символа функция равна нулю
            int k = 0;

            for (int q = 1; q < str.Length; q++)
            {

                while ((k > 0) && (str[k] != str[q]))
                {
                    k = pi[k - 1];
                }

                if (str[k] == str[q])
                {
                    k++;
                }
                pi[q] = k;
            }

            return pi;
        }
    }

    static class KnuthMorrisPrattAlgorithm
    {

        /// <summary>
        /// Ищет первое вхождение образца в данной строке.
        /// </summary>
        /// <returns>
        /// Индекс первого вхождения или -1, если вхождение не найдено.
        /// </returns>
        /// <param name="needle">Строка, вхождение которой нужно найти.</param>
        /// <param name="haystack">Строка, в которой осуществляется поиск.</param>
        public static int FindFirstEntry(string needle, string haystack)
        {

            foreach (var element in FindAllEntries(needle, haystack))
            {
                return element;
            }

            return -1;
        }


        public static IEnumerable<int> FindAllEntries(string needle, string haystack)
        {
            var pi = PrefixFunction.Compute(needle);
            int j = 0;

            for (int i = 0; i < haystack.Length; i++)
            {

                while ((j > 0) && (needle[j] != haystack[i]))
                {
                    j = pi[j - 1];
                }

                if (needle[j] == haystack[i])
                {
                    j++;
                    //if (j == needle.Length)
                    //{
                    //    yield return (i - j + 1);
                    //    j = pi[j - 1];
                    //    found = true;
                    //}
                }
                if (j == needle.Length)
                {
                    yield return (i - j + 1);
                    j = pi[j - 1];
                }
            }

        }
    }

    static class BoyerMooreAlgorithm
    {
        //Эвристика стоп-символа
        private static IReadOnlyDictionary<char, int> ComputeLastOccurrenceFunction(string str)
        {
            //Если алфавит маленький типа ASCII (256 символов) то можно сделать массив
            // var lambda=new int[256];
            // и для каждого символа lambda[(byte)symbol]=i; , где i-самая правая позиция в строке
            // кроме последнего символа (это изменение поможет в алгоритме Хорспула)
            //В случае большого алфавита лучше использовать Dictionary
            var lambda = new Dictionary<char, int>();
            int m = str.Length - 1;

            for (int i = 0; i < m; i++)
            {
                char symbol = str[i];
                if (lambda.ContainsKey(symbol))
                {
                    lambda[symbol] = i;
                }
                else
                {
                    lambda.Add(symbol, i);
                }
            }

            return lambda;
        }

        //Эвристика безопасного суффикса
        private static int[] ComputeGoodSuffixFunction(string str)
        {
            var pi = PrefixFunction.Compute(str);
            var piRev = PrefixFunction.Compute(ReverseString(str));
            int m = str.Length;
            int[] gamma = new int[m + 1];
            // В массивах pi и piRev относительно Кормена индексы начинаются с нуля
            // при этом в gamma нумерация идет как и в книге

            for (int j = 0; j < m + 1; j++)
            {
                gamma[j] = m - pi[m - 1];
            }

            for (int l = 1; l < m + 1; l++)
            {
                int j = m - piRev[l - 1];
                if (gamma[j] > l - piRev[l - 1])
                {
                    gamma[j] = l - piRev[l - 1];
                }
            }

            return gamma;
        }

        public static int FindFirstEntry(string needle, string haystack)
        {

            foreach (var element in FindAllEntries(needle, haystack))
            {
                return element;
            }

            return -1;
        }

        public static IEnumerable<int> FindAllEntries(string needle, string haystack)
        {
            int n = haystack.Length;
            int m = needle.Length;
            var lambda = ComputeLastOccurrenceFunction(needle);
            var gamma = ComputeGoodSuffixFunction(needle);
            int s = 0;

            while (s < n - m + 1)
            {
                int j = m - 1;
                while ((j > -1) && (needle[j] == haystack[s + j]))
                {
                    j--;
                }
                if (j == -1)
                {
                    yield return s;
                    s += gamma[0];
                }
                else
                {
                    int position = (lambda.TryGetValue(haystack[s + j], out int value)) ? value : (-1);
                    s += Math.Max(gamma[j + 1], j - position);
                }
            }

        }

        private static string ReverseString(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);
            return (new string(array));
        }
    }
}