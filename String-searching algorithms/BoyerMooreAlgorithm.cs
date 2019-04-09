using System;
using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    /// <summary>
    /// Алгоритм поиска строки Бойера — Мура.
    /// </summary>
    public static class BoyerMooreAlgorithm
    {
        //Эвристика стоп-символа
        private static IReadOnlyDictionary<char, int> ComputeLastOccurrenceFunction(string str)
        {
            // Если алфавит маленький типа ASCII (256 символов) то можно сделать массив
            // var lambda=new int[256];
            // и для каждого символа lambda[(byte)symbol]=i; , где i-самая правая позиция в строке
            // кроме последнего символа (это изменение поможет в алгоритме Хорспула)
            // В случае большого алфавита лучше использовать Dictionary
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

        ///  <summary>
        ///  Возвращает первое вхождение образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Индекс первого вхождения или -1, если вхождение не найдено.
        ///  </returns>
        ///  <param name = "needle" > Строка, вхождение которой нужно найти.</param>
        ///  <param name = "haystack" > Строка, в которой осуществляется поиск.</param>
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