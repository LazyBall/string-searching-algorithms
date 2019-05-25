using System;
using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    /// <summary>
    /// Алгоритм Бойера — Мура.
    /// </summary>
    public class BoyerMooreAlgorithm : IStringSearchingAlgorithm
    {

        //Эвристика стоп-символа
        private Dictionary<char, int> ComputeLastOccurrenceFunction(string str)
        {
            // Если алфавит маленький типа ASCII (256 символов) то можно сделать массив
            // var lambda=new int[256];
            // и для каждого символа lambda[(int)symbol]=i; , где i-самая правая позиция в строке
            // кроме последнего символа (это изменение важно в алгоритме Хорспула)
            // В случае большого алфавита лучше использовать Dictionary
            var lambda = new Dictionary<char, int>();

            for (int i = str.Length - 2; i >= 0; i--)
            {
                var symbol = str[i];
                if (!lambda.ContainsKey(symbol))
                {
                    lambda.Add(symbol, i);
                }
            }

            return lambda;
        }

        private string ReverseString(string str)
        {
            var charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return (new string(charArray));
        }

        //Эвристика хорошего суффикса
        private int[] ComputeGoodSuffixFunction(string str)
        {
            var pi = PrefixFunction.Compute(str);
            var piRev = PrefixFunction.Compute(ReverseString(str));
            var gamma = new int[(long)str.Length + 1];
            // В массивах pi и piRev относительно Кормена индексы начинаются с нуля
            // при этом в gamma нумерация идет как и в книге

            for (int j = 0, initialShift = str.Length - pi[str.Length - 1]; j < gamma.Length; j++)
            {
                gamma[j] = initialShift;
            }

            for (int l = 1; l < gamma.Length; l++)
            {
                var j = str.Length - piRev[l - 1];
                var shift = l - piRev[l - 1];
                if (gamma[j] > shift)
                {
                    gamma[j] = shift;
                }
            }

            return gamma;
        }

        ///  <summary>
        ///  Находит все вхождения образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Перечисление индексов вхождения образца в строку, в которой осуществляется поиск,
        ///  в порядке возрастания.
        ///  </returns>
        ///  <exception cref="System.ArgumentNullException">text или pattern null</exception>
        ///  <param name = "pattern">Строка, вхождения которой нужно найти.</param>
        ///  <param name = "text">Строка, в которой осуществляется поиск.</param>
        public IEnumerable<int> GetAllEntries(string pattern, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text is null.");
            }
            if (pattern == null)
            {
                throw new ArgumentNullException("pattern is null.");
            }
            if (pattern == string.Empty)
            {

                for (int i = 0; i <= text.Length; i++)
                {
                    yield return i;
                }

                yield break;
            }
            var lambda = ComputeLastOccurrenceFunction(pattern);
            var gamma = ComputeGoodSuffixFunction(pattern);
            int stop = text.Length - pattern.Length;
            int currentShift = 0;

            while (currentShift <= stop)
            {
                int j = pattern.Length - 1;
                while ((j > -1) && (pattern[j] == text[currentShift + j]))
                {
                    j--;
                }
                if (j == -1)
                {
                    yield return currentShift;
                    currentShift += gamma[0];
                }
                else
                {
                    var rightmostPosition = (lambda.TryGetValue(text[currentShift + j], out int value)) ?
                        value : (-1);
                    currentShift += Math.Max(gamma[j + 1], j - rightmostPosition);
                }
            }

        }

        ///  <summary>
        ///  Находит первое вхождение образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Индекс первого вхождения или -1, если вхождение не найдено.
        ///  </returns>
        ///  <exception cref="System.ArgumentNullException">text или pattern null</exception>
        ///  <param name = "pattern">Строка, вхождение которой нужно найти.</param>
        ///  <param name = "text">Строка, в которой осуществляется поиск.</param>
        public int GetFirstEntry(string pattern, string text)
        {

            foreach (var element in this.GetAllEntries(pattern, text))
            {
                return element;
            }

            return -1;
        }
       
    }
}