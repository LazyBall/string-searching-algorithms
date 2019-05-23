using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                new KnuthMorrisPrattAlgorithm(),
                new BoyerMooreAlgorithm()               
            };

            var tolkien = "TheLordOfTheRings.txt";
            var tolstoy = "WarAndPeace.txt";

            var test = new Test();
            //var text = test.DoRandomString(int.MaxValue / 1000);
            //test.RunTest(text, "Random", new List<string>(), list);
            //text = test.ReadFromFile(tolkien);
            //test.RunTest(text, "Lord", test.DoWords(text), list);
            var text = test.ReadFromFile(tolstoy);
            //test.RunTest(text, "War", test.DoWords(text), list);

            //Console.WriteLine("English: {0}", test.ReadFromFile(tolkien).Length);
            //Console.WriteLine("Russian: {0}", test.ReadFromFile(tolstoy).Length);

            var pattern= Console.ReadLine();
            var watch = new Stopwatch();
            
            foreach(var alg in list)
            {
                watch.Restart();
                alg.GetAllEntries(pattern, text).Count();
                watch.Stop();
                Console.WriteLine("{0} - {1}", alg.ToString(), watch.ElapsedMilliseconds);
            }

            //Console.WriteLine(new BoyerMooreAlgorithm().GetAllEntries("ab", "abracadabra").Count() +
            //    new BoyerMooreAlgorithm().GetAllEntries("bra", "abracadabra").Count());

            //Console.WriteLine(new RabinKarpAlgorithm().GetIndexes(new List<string>() {"ab","bra" }, "abracadabra").Count());
        }      
    }
}