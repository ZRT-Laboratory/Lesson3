using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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

            try
            {
                //act
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
            string[] testArray = Array.Empty<string>();

            Assert.ThrowsException<ArgumentException>(() => testArray.GetSortedFileDataFromArguments(new string[] { "json", string.Empty, "xml", string.Empty }));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string testFile = CreateTempFile(Array.Empty<string>());
            string[] testArray = Array.Empty<string>();

            try
            {
                //act
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
            string[] testArray = Array.Empty<string>();
            Assert.ThrowsException<ArgumentException>(() => testArray.GetSortedFileDataFromArguments(Array.Empty<string>()));
        }

        [TestMethod]
        public void Arguments_WithInvalidFileNames()
        {
            string[] testArray = Array.Empty<string>();
            Assert.ThrowsException<FileNotFoundException>(() => testArray.GetSortedFileDataFromArguments(new string[] { "-json", "BadFile.txt", "-xml", "BadFile.txt" }));
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
