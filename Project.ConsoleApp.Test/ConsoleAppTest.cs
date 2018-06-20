using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Project.ConsoleApp.Test
{
    [TestClass]
    public class ConsoleAppTest
    {
        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] clArguments = new string[] { ConsoleAppExtensions.Json, string.Empty, ConsoleAppExtensions.Xml, string.Empty };

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
            string[] clArguments = new string[] { ConsoleAppExtensions.Json, "BadFile.txt", ConsoleAppExtensions.Xml, "BadFile.txt" };

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

            //assert - validate that we have 2 null No Values returned
            Assert.IsTrue(testResults.Count(tr => tr == "No Value") == 2);
        }
    }
}
