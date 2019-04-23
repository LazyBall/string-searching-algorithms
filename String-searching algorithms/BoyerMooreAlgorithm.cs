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

            foreach (var element in this.GetAllEntries(pattern, text))
            {
                return element;
            }

            return -1;
        }

        public IEnumerable<int> GetAllEntries(string pattern, string text)
        {
            var lambda = ComputeLastOccurrenceFunction(pattern);
            var gamma = ComputeGoodSuffixFunction(pattern);           
            int stop = text.Length - pattern.Length + 1;
            int s = 0;

            while (s < stop)
            {
                int j = pattern.Length - 1;
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
            // кроме последнего символа (это изменение важно в алгоритме Хорспула)
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

        //Эвристика хорошего суффикса
        private int[] ComputeGoodSuffixFunction(string str)
        {
            var pi = PrefixFunction.Compute(str);
            var piRev = PrefixFunction.Compute(ReverseString(str));
            int[] gamma = new int[str.Length + 1];
            // В массивах pi и piRev относительно Кормена индексы начинаются с нуля
            // при этом в gamma нумерация идет как и в книге

            for (int j = 0; j < gamma.Length; j++)
            {
                gamma[j] = str.Length - pi[str.Length - 1];
            }

            for (int l = 1; l < gamma.Length; l++)
            {
                int j = str.Length - piRev[l - 1];
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