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

    static class KnuthMorrisPrattAlgorithm
    {
        public static int[] ComputePrefixFunction(string str)
        {
            var pi = new int[str.Length]; // значения префикс-функции
                                          // индекс вектора соответствует номеру последнего символа аргумента
            pi[0] = 0; // для префикса из нуля и одного символа функция равна нулю
            int j = 0;
            for (int i = 1; i < str.Length;)
            {
                if (str[i] == str[j])
                {
                    pi[i] = ++j;
                    i++;
                }
                else if (j == 0)
                {
                    pi[i] = 0;
                    i++;
                }
                else
                {
                    j = pi[j - 1];
                }


            }
            return pi;
        }

        public static int FindFirstEntry(string haystack, string needle)
        {
            var pi = ComputePrefixFunction(needle);
            int i = 0, j = 0;
            for (; i < haystack.Length;)
            {
                if (haystack[i] == needle[j])
                {
                    i++;
                    j++;
                    if (j == needle.Length)
                    {
                        return (i - j + 1);
                    }
                }
                else if (j == 0)
                {
                    i++;
                }
                else
                {
                    j = pi[j - 1];
                }
            }
            return -1;
        }
    }
}
