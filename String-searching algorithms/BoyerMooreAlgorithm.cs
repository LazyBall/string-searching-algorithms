using System;
using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    /// <summary>
    /// Алгоритм поиска строки Бойера — Мура.
    /// </summary>
    public class BoyerMooreAlgorithm : IStringSearchingAlgorithm
    {

        public int GetFirstEntry(string pattern, string text)
        {

            foreach (var element in GetAllEntries(pattern, text))
            {
                return element;
            }

            return -1;
        }

        public IEnumerable<int> GetAllEntries(string pattern, string text)
        {
            int n = text.Length;
            int m = pattern.Length;
            var lambda = ComputeLastOccurrenceFunction(pattern);
            var gamma = ComputeGoodSuffixFunction(pattern);
            int s = 0;

            while (s < n - m + 1)
            {
                int j = m - 1;
                while ((j > -1) && (pattern[j] == text[s + j]))
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
                    int position = (lambda.TryGetValue(text[s + j], out int value)) ? value : (-1);
                    s += Math.Max(gamma[j + 1], j - position);
                }
            }

        }

        //Эвристика стоп-символа
        private IReadOnlyDictionary<char, int> ComputeLastOccurrenceFunction(string str)
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
        private int[] ComputeGoodSuffixFunction(string str)
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

        private string ReverseString(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);
            return (new string(array));
        }
    }
}