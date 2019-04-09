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

            watch.Start();
            Console.WriteLine($"NaiveAlgorithm-FindFirstEntry: {NaiveAlgorithm.FindFirstEntry(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");

            watch.Restart();
            foreach (var value in NaiveAlgorithm.FindAllEntries(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"Naive-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();


            watch.Restart();
            Console.WriteLine($"KMP-FindFirstEntry: {KnuthMorrisPrattAlgorithm.FindFirstEntry(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");

            watch.Restart();
            foreach (var value in KnuthMorrisPrattAlgorithm.FindAllEntries(needle, haystack))
            {
                Console.Write($"{value} ");
            }
            watch.Stop();
            Console.WriteLine($"KMP-FindAllEntries: {watch.ElapsedMilliseconds}");
            Console.WriteLine();


            watch.Restart();
            Console.WriteLine($"BoyerMoore-FindFirstEntry: {BoyerMooreAlgorithm.FindFirstEntry(needle, haystack)}");
            watch.Stop();
            Console.WriteLine($"ElapsedMilliseconds: {watch.ElapsedMilliseconds}");

            watch.Restart();
            foreach (var value in BoyerMooreAlgorithm.FindAllEntries(needle, haystack))
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