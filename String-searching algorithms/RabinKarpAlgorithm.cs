using System;
using System.Collections.Generic;

namespace StringSearchingAlgorithms
{
    public class RabinKarpAlgorithm : IStringSearchingAlgorithm
    {
        //source http://www-igm.univ-mlv.fr/~lecroq/string/node5.html#SECTION0050        
        public IEnumerable<int> GetAllEntries(string pattern, string text)
        {
            uint factor = 1; 
            for (int i = 1; i < pattern.Length; i++)
            {
                factor <<= 1;
            }

            uint hashText, hashPattern;
            hashText = hashPattern = 0;

            for (int i = 0; i < pattern.Length; i++)
            {
                hashText = (hashText << 1) + text[i];
                hashPattern = (hashPattern << 1) + pattern[i];
            }

            int stop = text.Length - pattern.Length;

            for (int i = 0; i < stop; i++)
            {
                if (hashPattern == hashText)
                {
                    //если нужно нечеткое сравнение строк, то это сравнение убираем
                    if (string.CompareOrdinal(pattern, 0, text, i, pattern.Length) == 0)
                        yield return i;
                }
                hashText = ((hashText - factor * text[i]) << 1) + text[i + pattern.Length];
            }

            if (hashPattern == hashText)
            {
                //если нужно нечеткое сравнение строк, то это сравнение убираем
                if (string.CompareOrdinal(pattern, 0, text, stop, pattern.Length) == 0)
                    yield return stop;
            }
        }
       
        //Более безопасный хэш, но медленнее в 2 раза
        private IEnumerable<int> GetAllEntries1(string pattern, string text)
        {
            //int alphabetSize = (new Random().Next(1, char.MaxValue + 2)); //Вики
            int alphabetSize = char.MaxValue + 1; //размер алфавита, как в Кормене
            int prime = int.MaxValue; //простое число, по модулю которго проводим вычисления
            long factor = 1;
            for (int i = 1; i < pattern.Length; i++)
            {
                factor = (factor * alphabetSize) % prime;
            }

            long hashText, hashPattern;
            hashText = hashPattern = 0;

            for (int i = 0; i < pattern.Length; i++)
            {
                hashText = (hashText * alphabetSize + text[i]) % prime;
                hashPattern = (hashPattern * alphabetSize + pattern[i]) % prime;
            }

            int stop = text.Length - pattern.Length;

            for (int i = 0; i < stop; i++)
            {
                if (hashPattern == hashText)
                {
                    if (string.CompareOrdinal(pattern, 0, text, i, pattern.Length) == 0)
                        yield return i;
                }
                hashText = ((hashText - factor * text[i]) * alphabetSize + text[i + pattern.Length]) % prime;
                if (hashText < 0) hashText += prime;
            }

            if (hashPattern == hashText)
            {
                if (string.CompareOrdinal(pattern, 0, text, stop, pattern.Length) == 0)
                    yield return stop;
            }
        }

        public int GetFirstEntry(string pattern, string text)
        {
            foreach (var entry in GetAllEntries(pattern, text))
            {
                return entry;
            }

            return -1;
        }

        //алгоритм Рабина-Карпа позволяет вести поиск сразу нескольких паттернов
        public IEnumerable<KeyValuePair<int, string>> GetIndexes(IEnumerable<string> patterns, string text)
        {
            var dictionary = new Dictionary<int, HashSet<string>>();

            foreach (var pattern in patterns)
            {
                if (dictionary.TryGetValue(pattern.Length, out HashSet<string> set)) set.Add(pattern);
                else dictionary.Add(pattern.Length, new HashSet<string>() { pattern });
            }

            foreach(var patternSet in dictionary.Values)
            {
                foreach(var value in IndexOfAny(patternSet,text))
                {
                    yield return value;
                }
            }

        }

        private IEnumerable<KeyValuePair<int, string>> IndexOfAny(HashSet<string> patterns, string text)
        {
            var dictionary = new Dictionary<uint, List<string>>(patterns.Count);
            int patternLength = text.Length;

            foreach (var pattern in patterns)
            {
                patternLength = pattern.Length;
                var hashPattern = GetHash(pattern, 0, pattern.Length);
                if (dictionary.TryGetValue(hashPattern, out List<string> list)) list.Add(pattern);
                else dictionary.Add(hashPattern, new List<string>() { pattern });
            }

            uint hashText = GetHash(text, 0, patternLength);
            int stop = text.Length - patternLength;
            var factor = CreateFactor(patternLength - 1);

            for (int i = 0; i < stop; i++)
            {
                if (dictionary.TryGetValue(hashText, out List<string> list))
                {
                    foreach (var pattern in list)
                    {
                        if (string.CompareOrdinal(pattern, 0, text, i, pattern.Length) == 0)
                        {
                            yield return new KeyValuePair<int, string>(i, pattern);
                        }
                    }
                }

                hashText = ((hashText - factor * text[i]) << 1) + text[i + patternLength];
            }

            if (dictionary.TryGetValue(hashText, out List<string> patternList))
            {
                foreach (var pattern in patternList)
                {
                    if (string.CompareOrdinal(pattern, 0, text, stop, pattern.Length) == 0)
                    {
                        yield return new KeyValuePair<int, string>(stop, pattern);
                    }
                }
            }
        }

        private uint GetHash(string text, int startIndex, int length)
        {
            uint hashText = 0;
            int stop = startIndex + length;

            for (int i = startIndex; i < stop; i++)
            {
                hashText = (hashText << 1) + text[i];
            }

            return hashText;
        }

        private uint CreateFactor(int length)
        {
            uint factor = 1;

            for (int i = 0; i < length; i++)
            {
                factor <<= 1;
            }

            return factor;
        }
    }
}