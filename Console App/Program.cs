using System;
using System.Diagnostics;
using System.Text;
using StringSearchingAlgorithms;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            int n = 200000000, l = 10;
            var haystack = DoRandomString(n);
            var needle = DoRandomString(l);           

            //var haystack = "GCATCGCAGAGAGTATACAGTACG";
            //var needle = "GCAGAGAG";

            //Console.WriteLine(haystack);
            //Console.WriteLine(needle);

            var watch = new Stopwatch();

            var naive = new NaiveAlgorithm();
            var kmp = new KnuthMorrisPrattAlgorithm();
            var bm = new BoyerMooreAlgorithm();

            watch.Start();
            Console.WriteLine($"NaiveAlgorithm-FindFirstEntry: {naive.GetFirstIndex(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");

            watch.Restart();
            foreach (var value in naive.GetIndexes(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"Naive-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();


            watch.Restart();
            Console.WriteLine($"KMP-FindFirstEntry: {kmp.GetFirstIndex(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");

            watch.Restart();
            foreach (var value in kmp.GetIndexes(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"KMP-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();


            watch.Restart();
            Console.WriteLine($"BoyerMoore-FindFirstEntry: {bm.GetFirstIndex(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");

            watch.Restart();
            foreach (var value in bm.GetIndexes(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"BoyerMoore-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();
        }

        static string DoRandomString(int length)
        {
            var strBuilder = new StringBuilder(length);
            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                strBuilder.Append((char)random.Next(70, 80));
            }
            return strBuilder.ToString();
        }
    }
}