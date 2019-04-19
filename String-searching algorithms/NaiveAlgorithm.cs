using System.Collections.Generic;

namespace StringSearchingAlgorithms
{

    /// <summary>
    /// Наивный алгоритм поиска.
    /// </summary>
    public class NaiveAlgorithm : IStringSearchingAlgorithm
    {
        ///  <see cref="IStringSearchingAlgorithm.GetFirstIndex(string, string)"/>     
        public int GetFirstIndex(string pattern, string text)
        {
            foreach (var element in this.GetIndexes(pattern, text))
            {
                return element;
            }

            return -1;
        }

        ///  <see cref="IStringSearchingAlgorithm.GetIndexes(string, string)"/>
        public IEnumerable<int> GetIndexes(string pattern, string text)
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