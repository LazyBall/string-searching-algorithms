using System;
using System.Collections.Generic;
using System.Text;
using StringSearchingAlgorithms;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            var list = new List<IStringSearchingAlgorithm>
            {
                new NaiveAlgorithm(),
                new RabinKarpAlgorithm(),
                new KnuthMorrisPrattAlgorithm(),
                new BoyerMooreAlgorithm()               
            };

            var tolkien = "TheLordOfTheRings.txt";
            var tolstoy = "WarAndPeace.txt";

            var test = new Test();
            var text = test.DoRandomString(int.MaxValue / 1000);
            test.RunTest(text, "Random", new List<string>(), list);
            text = test.ReadFromFile(tolkien);
            test.RunTest(text, "Lord", test.DoWords(text), list);
            text = test.ReadFromFile(tolstoy);
            test.RunTest(text, "War", test.DoWords(text), list);

            //Console.WriteLine("English: {0}", test.ReadFromFile(tolkien).Length);
            //Console.WriteLine("Russian: {0}", test.ReadFromFile(tolstoy).Length);

        }      
    }
}