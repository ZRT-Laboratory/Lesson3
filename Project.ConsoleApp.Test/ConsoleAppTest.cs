using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

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
            string testFile = CreateTempFile(Array.Empty<string>());
            string[] clArguments = new string[] { _json, testFile, _xml, testFile };

            //act
            try
            {
                clArguments.GetSortedFileDataFromArguments();
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] clArguments = new string[] { "json", string.Empty, "xml", string.Empty };

            //assert
            Assert.ThrowsException<ArgumentException>(() => clArguments.GetSortedFileDataFromArguments());
        }
                
        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //arrange
            string[] clArguments = Array.Empty<string>();

            //assert
            Assert.ThrowsException<ArgumentException>(() => clArguments.GetSortedFileDataFromArguments());
        }

        [TestMethod]
        public void Arguments_WithInvalidFileNames()
        {
            //arrange
            string[] clArguments = new string[] { _json, "BadFile.txt", _xml, "BadFile.txt" };

            //assert
            Assert.ThrowsException<FileNotFoundException>(() => clArguments.GetSortedFileDataFromArguments());
        }

        [TestMethod]
        public void AlphaNumericDataSortedProperly()
        {
            //arrange
            string testFile = CreateTempFile(Array.Empty<string>());
            string[] testArray = new string[] { "Red Apple", "Naval Orange", null, "92", "55", null };
            string[] expectedResults = new string[] { "55", "92", "Naval Orange", "Red Apple", "No Value", "No Value" };

            //act
            string[] testResults = testArray.SortNullValuesToBottom().ReplaceNullsWithStringValue("No Value").ToArray();

            //assert - validate that the sorted test results match the sorted expected results
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }

            //assert - validate that test data has nulls         
            Assert.IsTrue(testArray.Any(ta => ta == null));

            //assert - validate that null values returned as no value  
            Assert.IsTrue(testResults.Any(tr => tr == "No Value"));
        }

        #region  " Non Test Methods "

        /// <summary>
        /// CreateTempFile
        /// </summary>
        /// <param name="fileData">array containing data you want to write to the file. pass an empty array if no data needed</param>
        /// <returns>filepath of new temp file</returns>
        private string CreateTempFile(string[] fileData)
        {
            string testFile = Path.GetTempFileName();
            File.WriteAllLines(testFile, fileData);

            return testFile;
        }

        #endregion
    }
}
