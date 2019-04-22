using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using StringSearchingAlgorithms;
using System.Linq;
using System.IO;
using System.Numerics;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            //int n = 200000000, l = 45;
            //string haystack, needle;
            //var haystack = DoRandomString(n);
            //var needle = DoRandomString(l);

            //var haystack = "GCATCGCAGAGAGTATACAGTACG";
            //var needle = "GCAGAGAG";

            //Console.WriteLine(haystack);
            //Console.WriteLine(needle);

            var list = new List<IStringSearchingAlgorithm>
            {
                new NaiveAlgorithm(),
                new KnuthMorrisPrattAlgorithm(),
                new BoyerMooreAlgorithm(),
                new RabinKarpAlgorithm()
            };
            var rb = new RabinKarpAlgorithm();

            var watch = new Stopwatch();
            var haystack = GenerateThueMorseSequence(27);
            var needle = haystack.Substring(0, 113);

            foreach (var alg in list)
            {
                watch.Restart();
                Console.WriteLine(alg.GetFirstEntry(needle, haystack));
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
                watch.Restart();
                Console.WriteLine(alg.GetAllEntries(needle, haystack).Count());
                Console.WriteLine(watch.ElapsedMilliseconds);
                Console.WriteLine();
            }
        }

        static string DoRandomString(int length, int minValue = 0, int maxValue = char.MaxValue + 1)
        {
            var strBuilder = new StringBuilder(length);
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < length; i++)
            {
                strBuilder.Append((char)random.Next(minValue, maxValue));
            }

            return strBuilder.ToString();
        }

        static string ReadFromFile(string fileName)
        {
            using (var inputFile = new StreamReader(fileName))
            {
                return inputFile.ReadToEnd();
            }
        }

        static string GenerateThueMorseSequence(int depth)
        {
            var strBuilder = new StringBuilder(1 << depth);
            strBuilder.Append('1');

            for (int i = 0; i < depth; i++)
            {
                strBuilder.Append(Invert(strBuilder.ToString()));
            }

            return strBuilder.ToString();

            string Invert(string str)
            {
                var builder = new StringBuilder(str.Length);

                foreach(var symbol in str)
                {
                    if (symbol == '0') builder.Append('1');
                    else builder.Append('0');
                }

                return builder.ToString();
            }

        }
    }
}