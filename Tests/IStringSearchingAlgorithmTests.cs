using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using StringSearchingAlgorithms;

namespace Tests
{
    class IStringSearchingAlgorithmTests<T> where T : IStringSearchingAlgorithm, new()
    {

        private string ReadFromFile(string fileName)
        {
            using (var inputFile = new StreamReader(fileName, Encoding.Default))
            {
                return inputFile.ReadToEnd();
            }
        }

        private HashSet<string> DoWords(IEnumerable<char> text)
        {
            var strBuilder = new StringBuilder();
            var words = new HashSet<string>();

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
                        words.Add(strBuilder.ToString());
                        strBuilder.Clear();
                    }
                }
            }

            return words;
        }

        public bool TestGetAllEntriesWhenTextIsNull()
        {
            var flag = false;
            try
            {
                new T().GetAllEntries("abc", null).Count();
            }
            catch
            {
                flag = true;
            }
            return flag;
        }

        public bool TestGetAllEntriesWhenPatternIsNull()
        {
            var flag = false;
            try
            {
                new T().GetAllEntries(null, "abc").Count();
            }
            catch
            {
                flag = true;
            }
            return flag;
        }

        public bool TestGetAllEntriesWhenTextIsEmptyAndPatternNotEmpty()
        {
            return (new T().GetAllEntries("abc", string.Empty).Count() == 0);
        }

        public bool TestGetAllEntriesWhenPatternIsEmpty()
        {
            var flag = true;
            int i = -1;
            var text = "abc";

            foreach (var item in new T().GetAllEntries(string.Empty, text))
            {
                i++;
                flag = flag && (i == item);
            }

            flag = flag && (i == text.Length);
            return flag;
        }

        public bool TestGetAllEntriesWhenPatternIsNotSubstring()
        {
            return (new T().GetAllEntries("abcd", "efghrthyt").Count() == 0);
        }

        public bool TestGetFirstEntryWhenTextIsNull()
        {
            var flag = false;
            try
            {
                new T().GetFirstEntry("abc", null);
            }
            catch
            {
                flag = true;
            }
            return flag;
        }

        public bool TestGetFirstEntryWhenPatternIsNull()
        {
            var flag = false;
            try
            {
                new T().GetFirstEntry(null, "abc");
            }
            catch
            {
                flag = true;
            }
            return flag;
        }

        public bool TestGetFirstEntryWhenTextIsEmptyAndPatternNotEmpty()
        {
            return (new T().GetFirstEntry("abc", string.Empty) == -1);
        }

        public bool TestGetFirstEntryWhenPatternIsEmpty()
        {
            var text = "abc";
            return (new T().GetFirstEntry(string.Empty, text) == 0);
        }

        public bool EasyTestGetAllEntries()
        {
            var text = "abracadabra";
            var pattern = "abr";
            var flag = true;
            var expected = new List<int>() { 0, 7 }.GetEnumerator();
            expected.MoveNext();

            foreach (var index in new T().GetAllEntries(pattern, text))
            {
                flag = flag && (index == expected.Current);
                expected.MoveNext();
            }

            return flag;
        }

        public bool EasyTestGetFirstEntry()
        {
            var text = "bracadabra";
            var pattern = "abr";
            return (6 == new T().GetFirstEntry(pattern, text));
        }      

        public bool TestGetFirstEntryWhenPatternIsNotSubstring()
        {
            return (new T().GetFirstEntry("abcd", "efghrthyt") == -1);
        }

        public bool TestGetAllEntriesOnFile()
        {
            var text = ReadFromFile("WarAndPeace.txt");
            text = text.Substring(text.Length - text.Length / 100);
            var flag = true;
            var algorithm = new T();

            foreach (var word in DoWords(text))
            {
                int i = 0;

                foreach (var index in algorithm.GetAllEntries(word, text))
                {
                    flag = flag && (index == text.IndexOf(word, i));
                    i = index + 1;
                }

            }

            return flag;
        }

        public bool TestGetFirstEntryOnFile()
        {
            var text = ReadFromFile("WarAndPeace.txt");
            text = text.Substring(text.Length - text.Length / 100);
            var algorithm = new T();
            var flag = true;

            foreach (var word in DoWords(text))
            {
                flag = flag && (algorithm.GetFirstEntry(word, text) == text.IndexOf(word));
            }

            return flag;
        }

    }
}