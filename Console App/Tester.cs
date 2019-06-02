using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StringSearchingAlgorithms;
using System.Diagnostics;

namespace Console_App
{
    static class Tester
    {

        class BenchmarkResult
        {
            public long GetFirstEntryTime { get; set; }
            public long GetAllEntriesTime { get; set; }

            public void Add(BenchmarkResult result)
            {
                this.GetAllEntriesTime += result.GetAllEntriesTime;
                this.GetFirstEntryTime += result.GetFirstEntryTime;
            }
        }

        class TestResult
        {
            public BenchmarkResult BenchmarkResult { get; set; }
            public int NumberTests { get; set; }
        }

        public static void RunTest(string text, string nameText, IEnumerable<string> words,
           List<IStringSearchingAlgorithm> algorithms)
        {
            var statistics = new List<Dictionary<int, TestResult>>();

            for (int i = 0; i < algorithms.Count; i++)
            {
                statistics.Add(new Dictionary<int, TestResult>());
            }

            foreach (var word in words)
            {
                for (int i = 0; i < algorithms.Count; i++)
                {
                    var benchmarkResult = RunBenchmark(text, word, algorithms[i]);
                    if (statistics[i].TryGetValue(word.Length, out TestResult testResult))
                    {
                        testResult.BenchmarkResult.Add(benchmarkResult);
                        testResult.NumberTests++;
                    }
                    else
                    {
                        statistics[i].Add(word.Length, new TestResult()
                        {
                            BenchmarkResult = benchmarkResult,
                            NumberTests = 1
                        });
                    }
                }
            }

            for (int i = 0; i < statistics.Count; i++)
            {
                CreateReport(statistics[i], nameText + algorithms[i].ToString() + "Result.txt");
            }

        }

        static BenchmarkResult RunBenchmark(string text, string word, IStringSearchingAlgorithm algorithm)
        {
            GC.Collect(2, GCCollectionMode.Forced, true);
            var result = new BenchmarkResult();
            var watch = new Stopwatch();
            watch.Restart();
            algorithm.GetFirstEntry(word, text);
            watch.Stop();
            result.GetFirstEntryTime = watch.ElapsedMilliseconds;
            GC.Collect(2, GCCollectionMode.Forced, true);
            watch.Restart();
            algorithm.GetAllEntries(word, text).Count();
            watch.Stop();
            result.GetAllEntriesTime = watch.ElapsedMilliseconds;
            return result;
        }      

        static void CreateReport(IReadOnlyDictionary<int, TestResult> statistics, string fileName)
        {
            using (var outputFile = new StreamWriter(fileName))
            {
                foreach (var record in (from t in statistics orderby t.Key select t))
                {
                    var averageGetFirst = Math.Round((double)record.Value.BenchmarkResult.GetFirstEntryTime
                        / record.Value.NumberTests, 0);
                    var averageGetAll = Math.Round((double)record.Value.BenchmarkResult.GetAllEntriesTime
                        / record.Value.NumberTests, 0);
                    outputFile.WriteLine($"WordLength: {record.Key}, \t" +
                        $"AverageTime(GetFirstEntry): {averageGetFirst} ms, \t" +
                        $"AverageTime(GetAllEntries): {averageGetAll} ms");
                }
            }
        }

    }
}