using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    /// <summary>
    /// Алгоритм Кнута — Морриса — Пратта (КМП-алгоритм).
    /// </summary>
    public class KnuthMorrisPrattAlgorithm : IStringSearchingAlgorithm
    {

        /// <see cref="IStringSearchingAlgorithm.GetFirstIndex(string, string)"/>
        public int GetFirstIndex(string pattern, string text)
        {
            foreach (var element in GetIndexes(pattern, text))
            {
                return element;
            }

            return -1;
        }

        /// <see cref="IStringSearchingAlgorithm.GetIndexes(string, string)"/>
        public IEnumerable<int> GetIndexes(string pattern, string text)
        {
            var pi = PrefixFunction.Compute(pattern);
            int j = 0;

            for (int i = 0; i < text.Length; i++)
            {

                while ((j > 0) && (pattern[j] != text[i]))
                {
                    j = pi[j - 1];
                }

                if (pattern[j] == text[i])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    yield return (i - j + 1);
                    j = pi[j - 1];
                }

            }

        }
    }
}