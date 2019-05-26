using System.Collections.Generic;
using System.Linq;
using StringSearchingAlgorithms;

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

            var text = Helper.DoRandomString(int.MaxValue);
            Tester.RunTest(text, "Random", Helper.GenerateSubstrings(text, 10, text.Length),
                algorithms);

            var tolkien = "TheLordOfTheRings.txt";
            var tolstoy = "WarAndPeace.txt";
            text = Helper.ReadFromFile(tolstoy);
            Tester.RunTest(text, "Tolstoy", Helper.DoWords(text).
                Concat(Helper.GenerateSubstrings(text, 0, text.Length)), algorithms);

            text = Helper.ReadFromFile(tolkien);
            Tester.RunTest(text, "Tolkien", Helper.DoWords(text).
                Concat(Helper.GenerateSubstrings(text, 0, text.Length)), algorithms);
        }      
    }
}