using System.Collections.Generic;

namespace StringSearchingAlgorithms
{

    /// <summary>
    /// Наивный алгоритм поиска.
    /// </summary>
    public static class NaiveAlgorithm
    {
        ///  <summary>
        ///  Возвращает первое вхождение образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Индекс первого вхождения или -1, если вхождение не найдено.
        ///  </returns>
        ///  <param name = "needle" > Строка, вхождение которой нужно найти.</param>
        ///  <param name = "haystack" > Строка, в которой осуществляется поиск.</param>
        public static int FindFirstEntry(string needle, string haystack)
        {

            foreach (var element in FindAllEntries(needle, haystack))
            {
                return element;
            }

            return -1;
        }

        ///  <summary>
        ///  Возвращает все вхождения образца в строку, в которой осуществляется поиск.
        ///  </summary>
        ///  <returns>
        ///  Индекс первого вхождения или -1, если вхождение не найдено.
        ///  </returns>
        ///  <param name = "needle" > Строка, вхождения которой нужно найти.</param>
        ///  <param name = "haystack" > Строка, в которой осуществляется поиск.</param>
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
}