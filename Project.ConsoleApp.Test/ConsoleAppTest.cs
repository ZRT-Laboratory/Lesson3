using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Project.ConsoleApp;

namespace Project.ConsoleApp.Test
{
    [TestClass]
    public class ConsoleAppTest
    {
        const string _json = "-json";
        const string _xml = "-xml";

        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] clArguments = new string[] { _json, string.Empty, _xml, string.Empty };

            //assert
            Assert.IsTrue(clArguments.HaveValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] clArguments = new string[] { "csv", string.Empty, "txt", string.Empty };

            //assert
            Assert.IsFalse(clArguments.HaveValidArguments());
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //arrange
            string[] clArguments = Array.Empty<string>();

            //assert
            Assert.IsFalse(clArguments.HaveValidArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidFileNames()
        {
            //arrange
            string[] clArguments = new string[] { _json, "BadFile.txt", _xml, "BadFile.txt" };

            //assert
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetJsonData());
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetXmlData());
        }

        [TestMethod]
        public void AlphaNumericDataSortedProperly()
        {
            //arrange
            string[] testData = new string[] { "Red Apple", "Naval Orange", null, "92", "55", null };
            string[] expectedResults = new string[] { "55", "92", "Naval Orange", "Red Apple", "No Value", "No Value" };

            //act
            string[] testResults = testData.SortNullValuesToBottom().ReplaceNullsWithStringValue("No Value").ToArray();

            //assert - validate that the sorted test results match the sorted expected results
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }

            //assert - validate that test data has nulls         
            Assert.IsTrue(testData.Any(ta => ta == null));

            //assert - validate that null values returned as no value  
            Assert.IsTrue(testResults.Any(tr => tr == "No Value"));
        }
    }
}
