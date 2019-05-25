using System;
using System.Collections.Generic;

namespace StringSearchingAlgorithms
{

    /// <summary>
    /// Наивный алгоритм поиска.
    /// </summary>
    public class NaiveAlgorithm : IStringSearchingAlgorithm
    {
      
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
            int stop = text.Length - pattern.Length;

            for (int i = 0; i <= stop; i++)
            {
                int j = 0;

                while ((j < pattern.Length) && (pattern[j] == text[i + j]))
                {
                    j++;
                }

                if (j == pattern.Length)
                {
                    yield return i;
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