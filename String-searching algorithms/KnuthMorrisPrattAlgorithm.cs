using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    public static class KnuthMorrisPrattAlgorithm
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
}