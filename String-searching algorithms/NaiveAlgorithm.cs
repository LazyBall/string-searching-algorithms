using System.Collections.Generic;

namespace StringSearchingAlgorithms
{

    /// <summary>
    /// Наивный алгоритм поиска.
    /// </summary>
    public class NaiveAlgorithm : IStringSearchingAlgorithm
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
            int stop = text.Length - pattern.Length + 1;

            for (int i = 0; i < stop; i++)
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
    }
}