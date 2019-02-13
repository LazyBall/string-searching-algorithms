using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_searching_algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    static class NaiveAlgorithm
    {
        public static int FindFirstEntry(string haystack, string needle)
        {
            int stop = haystack.Length - needle.Length + 1;
            for (int i = 0; i < stop; i++)
            {
                int j;
                for (j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        break;
                    }
                }
                if (j == (needle.Length - 1))
                {
                    return i;
                }
            }
            return -1;
        }

        public static IEnumerable<int> FindAllEntries(string haystack, string needle)
        {
            int stop = haystack.Length - needle.Length + 1;
            bool found = false;
            for (int i = 0; i < stop; i++)
            {
                int j;
                for (j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        break;
                    }
                }
                if (j == (needle.Length - 1))
                {
                    found = true;
                    yield return i;
                }
            }
            if (found == false)
            {
                yield return -1;
            }
        }
    }
}
