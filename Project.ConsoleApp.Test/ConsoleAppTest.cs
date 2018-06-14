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
            string testFile = CreateTempFile(Array.Empty<string>());
            string[] testArray = Array.Empty<string>();

            //act
            try
            {
                testArray.GetSortedFileDataFromArguments(new string[] { "-json", testFile, "-xml", testFile });
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
            string[] testArray = Array.Empty<string>();

            //assert
            Assert.ThrowsException<ArgumentException>(() => testArray.GetSortedFileDataFromArguments(new string[] { "json", string.Empty, "xml", string.Empty }));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string testFile = CreateTempFile(Array.Empty<string>());
            string[] testArray = Array.Empty<string>();

            //act
            try
            {
                testArray.GetSortedFileDataFromArguments(new string[] { "-json", testFile, "-xml", testFile, "-test", testFile });
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //arrange
            string[] testArray = Array.Empty<string>();

            //assert
            Assert.ThrowsException<ArgumentException>(() => testArray.GetSortedFileDataFromArguments(Array.Empty<string>()));
        }

        [TestMethod]
        public void Arguments_WithInvalidFileNames()
        {
            //arrange
            string[] testArray = Array.Empty<string>();

            //assert
            Assert.ThrowsException<FileNotFoundException>(() => testArray.GetSortedFileDataFromArguments(new string[] { "-json", "BadFile.txt", "-xml", "BadFile.txt" }));
        }

        [TestMethod]
        public void AlphaNumericDataSortedProperly()
        {
            //arrange
            string[] testArray = new string[] { "Red Apple", "Naval Orange", null, "92", "55", null };
            string[] expectedResults = new string[] { "55", "92", "Naval Orange", "Red Apple", "No Value", "No Value" };

            //act
            string[] testResults = testArray.SortNullValuesToBottom().ReplaceNullsWithStringValue("No Value").ToArray();

            //assert - validate that the sorted test results match the sorted expected results
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }

        [TestMethod]
        public void AlphaNumericDataSortedWithNullsReturnedAsNoValue()
        {
            //arrange
            string[] testArray = new string[] { "Red Apple", "Naval Orange", null, "92", "55", null };

            //act
            string[] testResults = testArray.SortNullValuesToBottom().ReplaceNullsWithStringValue("No Value").ToArray();

            //assert           
            Assert.IsTrue(testResults.Any(pd => pd == "No Value"));
        }

        #region  " Non Test Methods "

        /// <summary>
        /// CreateTempFile
        /// </summary>
        /// <param name="fileData">array containing data you want to write to the file. pass an empty array if no data needed</param>
        /// <returns>filepath of new temp file</returns>
        string CreateTempFile(string[] fileData)
        {
            string testFile = Path.GetTempFileName();

            if (fileData.Length > 0)
            {
                File.WriteAllLines(testFile, fileData);
            }

            return testFile;
        }

        #endregion
    }
}
