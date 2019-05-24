using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringSearchingAlgorithms;

namespace Tests
{
    [TestClass]
    public class NaiveAlgorithmTests
    {
        IStringSearchingAlgorithmTests<NaiveAlgorithm> _algorithmTests =
            new IStringSearchingAlgorithmTests<NaiveAlgorithm>();

        [TestMethod]
        public void TestGetAllEntriesWhenPatternIsNull()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetAllEntriesWhenPatternIsNull());
        }

        [TestMethod]
        public void TestGetAllEntriesWhenTextIsNull()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetAllEntriesWhenTextIsNull());
        }

        [TestMethod]
        public void TestGetAllEntriesWhenPatternIsEmpty()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetAllEntriesWhenPatternIsEmpty());
        }

        [TestMethod]
        public void TestGetAllEntriesWhenTextIsEmptyAndPatternNotEmpty()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetAllEntriesWhenTextIsEmptyAndPatternNotEmpty());
        }

        [TestMethod]
        public void TestGetAllEntriesWhenPatternIsNotSubstring()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetAllEntriesWhenPatternIsNotSubstring());
        }

        [TestMethod]
        public void TestGetAllEntriesOnFile()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetAllEntriesOnFile());
        }

        [TestMethod]
        public void EasyTestGetAllEntries()
        {
            Assert.AreEqual(true, _algorithmTests.EasyTestGetAllEntries());
        }

        [TestMethod]
        public void TestGetFirstEntryWhenPatternIsNull()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetFirstEntryWhenPatternIsNull());
        }

        [TestMethod]
        public void TestGetFirstEntryWhenTextIsNull()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetFirstEntryWhenTextIsNull());
        }

        [TestMethod]
        public void TestGetFirstEntryWhenPatternIsEmpty()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetFirstEntryWhenPatternIsEmpty());
        }

        [TestMethod]
        public void TestGetFirstEntryWhenTextIsEmptyAndPatternNotEmpty()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetFirstEntryWhenTextIsEmptyAndPatternNotEmpty());
        }

        [TestMethod]
        public void TestGetFirstEntryWhenPatternIsNotSubstring()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetFirstEntryWhenPatternIsNotSubstring());
        }

        [TestMethod]
        public void TestGetFirstEntryOnFile()
        {
            Assert.AreEqual(true, _algorithmTests.TestGetFirstEntryOnFile());
        }


        [TestMethod]
        public void EasyTestGetFirstEntry()
        {
            Assert.AreEqual(true, _algorithmTests.EasyTestGetFirstEntry());
        }

    }
}