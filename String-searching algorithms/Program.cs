using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace String_searching_algorithms
{
    class Program
    {
        static void Main()
        {
            var str1 = "ABCABD#ABCABCAABCABD";
            foreach(var val in Func1(str1))
            {
                Console.Write($"{val}| ");
            }
            Console.WriteLine();
            foreach (var val in Func2(str1))
            {
                Console.Write($"{val}| ");
            }
            //var watch = new Stopwatch();
            //for (int i = 1; i < 6; i++)
            //{
            //    var str = DoRandomString(10000000 * i);
            //    watch.Start();
            //    KnuthMorrisPrattAlgorithm.ComputePrefixFunctionHack(str);
            //    watch.Stop();
            //    Console.Write("Test: {0} \t Length: {1} \t Algorithm Hack: {2} ms \t", i, str.Length, watch.ElapsedMilliseconds);
            //    watch.Reset();
            //    watch.Start();
            //    KnuthMorrisPrattAlgorithm.ComputePrefixFunction(str);
            //    watch.Stop();
            //    Console.WriteLine("Algorithm Standart: {0} ms", watch.ElapsedMilliseconds);
            //    watch.Reset();
            //}

        }

        static int[] Func1(string str)
        {
            var pi = new int[str.Length + 1]; // значения префикс-функции + -1 в начале
            int i = 0;
            int j = pi[0] = -1; // благодаря -1 можно убрать лишнее условие из стандартной функции и КМП
            while (i < str.Length)
            {
                while ((j > -1) && (str[i] != str[j]))
                {
                    j = pi[j];
                }
                pi[++i] = ++j;
            }
            return pi;
        }

        static int[] Func2(string str)
        {
            var pi = new int[str.Length]; // значения префикс-функции
            pi[0] = 0; // для префикса из нуля и одного символа функция равна нулю
            int j = 0;
            for (int i = 1; i < str.Length; i++)
            {
                while ((j > 0) && (str[i] != str[j]))
                {
                    j = pi[j - 1];
                }
                if (str[i] == str[j])
                {
                    j++;
                }
                pi[i] = j;
            }
            return pi;
        }

        static string DoRandomString(int length)
        {
            var strBuilder = new StringBuilder(length);
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                strBuilder.Append((char)random.Next(50, 60));
            }
            return strBuilder.ToString();
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
                if (j >= needle.Length)
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
                if (j >= needle.Length)
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
            pi[0] = 0; // для префикса из нуля и одного символа функция равна нулю
            int j = 0;
            for (int i = 1; i < str.Length; i++)
            {
                while ((j > 0) && (str[i] != str[j]))
                {
                    j = pi[j - 1];
                }
                if (str[i] == str[j])
                {
                    j++;
                }
                pi[i] = j;
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

        public static IEnumerable<int> FindAllEntries(string haystack, string needle)
        {
            var pi = ComputePrefixFunction(needle);
            bool found = false;
            int j = 0;
            for (int i = 0; i < haystack.Length; i++)
            {
                while ((j > 0) && (haystack[i] != needle[j]))
                {
                    j = pi[j - 1];
                }
                if (haystack[i] == needle[j])
                {
                    j++;
                    if (j == needle.Length)
                    {
                        yield return (i - j + 1);
                        j = pi[j - 1];
                        found = true;
                    }
                }
            }
            if (!found)
            {
                yield return -1;
            }
        }
    }
}