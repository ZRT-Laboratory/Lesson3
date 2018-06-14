using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.ConsoleApp;
using Project.Interface;
using System;
using System.IO;
using System.Linq;

namespace Project.Json.Test
{
    [TestClass]
    public class JsonTest
    {
        #region " Test Methods "

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //assert
            Assert.IsTrue(ifhJson.GetParsedData("[{'Name':'Red Apple'},{ 'Name':'Green Apple'}]").Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidJSONFormat()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //assert
            Assert.ThrowsException<JsonSerializationException>(() => ifhJson.GetParsedData("[{'Name':Red Apple},{'Name':Green Apple}]"));
        }

        [TestMethod]
        public void File_WithValidJSONNonNumericDataSorted()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "[{'Name':'Red Apple'},{'Name':'Green Apple'},{'Name':'Granny Smith Apple'},{'Name':null}]" });
            string[] expectedResults = new string[] { "Granny Smith Apple", "Green Apple", "Red Apple", "No Value" };

            //act
            string[] testResults = testArray.GetSortedFileDataFromArguments(new string[] { "-json", testFile }).ToArray();

            //assert            
            try
            {
                //validate that the sorted testresults match the sorted expected results
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void File_WithValidJSONNumericDataSorted()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "[{'Age':55},{'Age':92},{'Age':25},{'Age':null}]" });
            string[] expectedResults = new string[] { "25", "55", "92", "No Value" };

            //act
            string[] testResults = testArray.GetSortedFileDataFromArguments(new string[] { "-json", testFile }).ToArray();

            //assert            
            try
            {
                //validate that the sorted testresults match the sorted expected results
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void File_WithValidJSONAlphaNumericDataSorted()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "[{'Name':'Red Apple'},{'Name':'Green Apple'},{'Name':null},{'Age':92},{'Age':55},{'Age':null}]" });
            string[] expectedResults = new string[] { "55", "92", "Green Apple", "Red Apple", "No Value", "No Value" };

            //act
            string[] testResults = testArray.GetSortedFileDataFromArguments(new string[] { "-json", testFile }).ToArray();

            //assert            
            try
            {
                //validate that the sorted testresults match the sorted expected results
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void File_WithValidJSONNoValue()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "[{ 'Name':'Red Apple'},{ 'Name':'Green Apple'},{'Name':null}]" });

            //act
            string[] testResults = testArray.GetSortedFileDataFromArguments(new string[] { "-json", testFile }).ToArray();

            //assert            
            try
            {
                Assert.IsTrue(testResults.Any(pd => pd == "No Value"));
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        #endregion

        #region  " Non Test Methods "

        /// <summary>
        /// CreateTempFile
        /// </summary>
        /// <param name="fileData">array containing data you want to write to the file. pass an empty array if no data needed</param>
        /// <returns>filepath of new temp file</returns>
        string CreateTempFile(string[] fileData)
        {
            string file = Path.GetTempFileName();

            if (fileData.Length > 0)
            {
                File.WriteAllLines(file, fileData);
            }

            return file;
        }

        #endregion
    }
}
