using System;
using System.Collections.Generic;
using StringSearchingAlgorithms;
using System.Text;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            var algorithms = new List<IStringSearchingAlgorithm>
            {
                new NaiveAlgorithm(),
                new KnuthMorrisPrattAlgorithm(),
                new BoyerMooreAlgorithm()
            };

            string text = string.Empty;

            //Random Test
            text = Helper.DoRandomString(int.MaxValue >> 12);
            Tester.RunTest(text, "Random", Helper.GenerateSubstrings(text, 100, text.Length, 100, 5),
                algorithms);

            //Russian test
            text = Helper.ReadFromFile("Russian.txt");
            Tester.RunTest(text, "Russian", Helper.GenerateSubstrings(text, 10, 1000,
                numberWordsOneLength: 50), algorithms);

            //English test
            text = Helper.ReadFromFile("English.txt");
            Tester.RunTest(text, "English", Helper.GenerateSubstrings(text, 10, 1000,
               numberWordsOneLength: 50), algorithms);

            //a..a test
            text = new StringBuilder().Append('a', 10000).ToString();
            Tester.RunTest(text, "NA", Helper.GenerateSubstrings(text, 100, 10000, 100, 5), algorithms);

            Console.WriteLine("Completed!");
            Console.ReadKey();
        }
    }
}