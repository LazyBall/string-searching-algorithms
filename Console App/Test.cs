using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StringSearchingAlgorithms;
using System.Diagnostics;

namespace Console_App
{
    class Test
    {

        class BenchmarkResult
        {
            public int Count { get; set; }
            public long TotalTime { get; set; }

            public BenchmarkResult(int count, long time)
            {
                this.Count = count;
                this.TotalTime = time;
            }
        }

        public void RunTest(string text, string nameText, IEnumerable<string> words,
            IEnumerable<IStringSearchingAlgorithm> algorithms)
        {
            var set = new HashSet<string>(words);
            set.UnionWith(GenerateSubstrings(text, 2, text.Length / 2));

            //foreach (var alg in algorithms)
            //{
            //    switch (alg)
            //    {
            //        case NaiveAlgorithm naive:
            //            CreateReport(TestFirstEntry(text, set, naive), nameText + "NaiveFirstEntry.txt");
            //            break;
            //        case KnuthMorrisPrattAlgorithm knuth:
            //            CreateReport(TestFirstEntry(text, set, knuth), nameText + "KnuthFirstEntry.txt");
            //            break;
            //        case BoyerMooreAlgorithm bm:
            //            CreateReport(TestFirstEntry(text, set, bm), nameText + "BoyerFirstEntry.txt");
            //            break;
            //        case RabinKarpAlgorithm rabin:
            //            CreateReport(TestFirstEntry(text, set, rabin), nameText + "RabinFirstEntry.txt");
            //            break;
            //    }
            //}

            //foreach (var alg in algorithms)
            //{
            //    switch (alg)
            //    {
            //        case NaiveAlgorithm naive:
            //            CreateReport(TestAllEntries(text, set, naive), nameText + "NaiveAllEntries.txt");
            //            break;
            //        case KnuthMorrisPrattAlgorithm knuth:
            //            CreateReport(TestAllEntries(text, set, knuth), nameText + "KnuthAllEntries.txt");
            //            break;
            //        case BoyerMooreAlgorithm bm:
            //            CreateReport(TestAllEntries(text, set, bm), nameText + "BoyerAllEntries.txt");
            //            break;
            //        case RabinKarpAlgorithm rabin:
            //            CreateReport(TestAllEntries(text, set, rabin), nameText + "RabinAllEntries.txt");
            //            break;
            //    }
            //}


            foreach (var alg in algorithms)
            {
                switch (alg)
                {
                    case NaiveAlgorithm naive:
                        CreateReport(FindAll(text, set, naive), nameText + "NaiveFindAll.txt");
                        break;
                    case KnuthMorrisPrattAlgorithm knuth:
                        CreateReport(FindAll(text, set, knuth), nameText + "KnuthFindAll.txt");
                        break;
                    case BoyerMooreAlgorithm bm:
                        CreateReport(FindAll(text, set, bm), nameText + "BoyerFindAll.txt");
                        break;
                    //case RabinKarpAlgorithm rabin:
                    //    CreateReport(FindAll(text, set, rabin), nameText + "RabinFindAll.txt");
                        break;
                }
            }
        }

        IReadOnlyDictionary<int, BenchmarkResult> FindAll(string text, IEnumerable<string> words, 
            IStringSearchingAlgorithm algorithm)
        {
            int n = words.Count();
            var watch = new Stopwatch();
            //if(algorithm is RabinKarpAlgorithm rabin)
            //{
            //    watch.Restart();
            //    rabin.GetIndexes(words, text).Count();
            //    watch.Stop();
            //}
            //else
            {
                watch.Restart();
                foreach(var word in words)
                {
                    algorithm.GetAllEntries(word,text).Count();
                }
                watch.Stop();
            }

            return new Dictionary<int, BenchmarkResult>()
                { { 1, new BenchmarkResult(1, watch.ElapsedMilliseconds) } };

        }

        IReadOnlyDictionary<int,BenchmarkResult> TestFirstEntry(string text, 
            IEnumerable<string> words,  IStringSearchingAlgorithm algorithm)
        {
            var statistics = new Dictionary<int, BenchmarkResult>();
            var watch = new Stopwatch();

            foreach (var word in words)
            {
                watch.Restart();
                algorithm.GetFirstEntry(word, text);
                watch.Stop();

                if (statistics.TryGetValue(word.Length, out BenchmarkResult result))
                {
                    checked
                    {
                        result.Count++;
                        result.TotalTime += watch.ElapsedMilliseconds;
                    }
                }
                else
                {
                    statistics.Add(word.Length, new BenchmarkResult(1, watch.ElapsedMilliseconds));
                }

            }

            return statistics;
        }

        IReadOnlyDictionary<int, BenchmarkResult> TestAllEntries(string text,
            IEnumerable<string> words, IStringSearchingAlgorithm algorithm)
        {
            var statistics = new Dictionary<int, BenchmarkResult>();
            var watch = new Stopwatch();

            foreach (var word in words)
            {
                watch.Restart();
                algorithm.GetAllEntries(word, text).Count();
                watch.Stop();                
                if (statistics.TryGetValue(word.Length, out BenchmarkResult result))
                {
                    checked
                    {
                        result.Count++;
                        result.TotalTime += watch.ElapsedMilliseconds;
                    }
                }
                else
                {
                    statistics.Add(word.Length, new BenchmarkResult(1, watch.ElapsedMilliseconds));
                }
            }

            return statistics;
        }     

        void CreateReport(IReadOnlyDictionary<int, BenchmarkResult> statistics, string fileName)
        {
            using (var outputFile = new StreamWriter(fileName))
            {
                foreach (var record in (from t in statistics orderby t.Key select t))
                {
                    outputFile.WriteLine(string.Format($"Length: {record.Key}, \t" +
                        $" AverageTime: {record.Value.TotalTime / record.Value.Count} ms"));
                }
            }
        }

        IEnumerable<string> GenerateSubstrings(string text, int startLength, int endLength)
        {
            var random = new Random(DateTime.Now.Millisecond);
            while (startLength < endLength)
            {
                for (int i = 0; i < 4; i++)
                {
                    var index = random.Next(0, text.Length - startLength + 1);
                    yield return text.Substring(index, startLength);
                }
                startLength *= 100;
            }
        }

        public string ReadFromFile(string fileName)
        {
            using (var inputFile = new StreamReader(fileName))
            {
                return inputFile.ReadToEnd();
            }
        }

        public IEnumerable<string> DoWords(IEnumerable<char> text)
        {
            var strBuilder = new StringBuilder();
            foreach (var symbol in text)
            {
                if (char.IsLetter(symbol))
                {
                    strBuilder.Append(symbol);
                }
                else
                {
                    if (strBuilder.Length > 0)
                    {
                        yield return strBuilder.ToString();
                        strBuilder.Clear();
                    }
                }
            }
        }

        public string DoRandomString(int length, int minValue = char.MinValue, 
            int maxValue = char.MaxValue + 1)
        {
            var strBuilder = new StringBuilder(length);
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < length; i++)
            {
                strBuilder.Append((char)random.Next(minValue, maxValue));
            }

            return strBuilder.ToString();
        }

        public void CheckAlgorithms(string text, IEnumerable<IStringSearchingAlgorithm> algorithms)
        {
            var dictionary = CreateDictionary(DoWords(text));
            int index = -100;
            int i = 1;
            foreach (var word in dictionary.Keys)
            {
                Console.WriteLine(i++);
                foreach (var alg in algorithms)
                {
                    if (index != -100)
                    {
                        if (index != alg.GetFirstEntry(word, text))
                        {
                            throw new Exception("Fail");
                        }
                    }
                    else index = alg.GetFirstEntry(word, text);
                }
                index = -100;
            }

            index = -1;
            i = 1;
            foreach (var word in dictionary.Keys)
            {
                Console.WriteLine(i++);
                foreach (var alg in algorithms)
                {
                    if (index != -1)
                    {
                        if (index != alg.GetAllEntries(word, text).Count())
                        {
                            throw new Exception("Fail");
                        }
                    }
                    else index = alg.GetAllEntries(word, text).Count();
                }
                index = -1;
            }
        }

        IReadOnlyDictionary<string, int> CreateDictionary(IEnumerable<string> words)
        {
            var dictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (dictionary.ContainsKey(word)) dictionary[word]++;
                else dictionary.Add(word, 1);
            }
            return dictionary;
        }

        string GenerateThueMorseSequence(int depth)
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

                foreach (var symbol in str)
                {
                    if (symbol == '0') builder.Append('1');
                    else builder.Append('0');
                }

                return builder.ToString();
            }

        }
    }
}