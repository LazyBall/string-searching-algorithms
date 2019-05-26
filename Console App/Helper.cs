using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Console_App
{
    static class Helper
    {

        public static IEnumerable<string> GenerateSubstrings(string text, int startLength, int endLength,
            int step = 10, int numberWordsOneLength = 10)
        {
            var random = new Random(DateTime.Now.Millisecond);

            while (startLength <= endLength)
            {
                for (int i = 0; i < numberWordsOneLength; i++)
                {
                    var index = random.Next(0, text.Length - startLength + 1);
                    yield return text.Substring(index, startLength);
                }
                startLength += step;
            }

        }

        public static string ReadFromFile(string fileName)
        {
            using (var inputFile = new StreamReader(fileName, Encoding.Default))
            {
                return inputFile.ReadToEnd();
            }
        }

        public static IEnumerable<string> DoWords(IEnumerable<char> text)
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

        public static string DoRandomString(int length, int minValue = char.MinValue,
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

    }
}