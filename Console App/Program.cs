using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using StringSearchingAlgorithms;
using System.Linq;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            int n = 200000000, l = 45;
            string haystack, needle;
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
            long res, res1;
            res = res1 = 0;

            for (int j = 0; j < 10; j++)
            {
                haystack = DoRandomString(n);
                needle = DoRandomString(l);
                for (int i = 0; i < 5; i++)
                {
                    watch.Restart();
                    Console.WriteLine(rb.GetAllEntries(needle, haystack).Count());
                    watch.Stop();
                    res += watch.ElapsedMilliseconds;
                    Console.WriteLine(watch.ElapsedMilliseconds);
                    watch.Restart();
                    Console.WriteLine(rb.GetAllEntries1(needle, haystack).Count());
                    Console.WriteLine(watch.ElapsedMilliseconds);
                    watch.Stop();
                    res1 += watch.ElapsedMilliseconds;
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Res:{0}, Res1:{1}", res, res1);

            //foreach(var alg in list)
            //{
            //    Console.WriteLine(alg.GetFirstEntry(needle, haystack));
            //    Console.WriteLine(alg.GetAllEntries(needle, haystack).Count());
            //    Console.WriteLine();
            //}           
        }

        static string DoRandomString(int length)
        {
            var strBuilder = new StringBuilder(length);
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                //strBuilder.Append((char)random.Next(char.MaxValue));
                strBuilder.Append((char)random.Next(78, 80));
            }
            return strBuilder.ToString();
        }
    }
}