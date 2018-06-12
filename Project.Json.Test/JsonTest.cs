using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.ConsoleApp;
using Project.Interface;
using System;
using System.Collections.Generic;
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
            IFileHandling ifhJson = new JsonParser();

            //create json string
            string json = "[{'Name':'Red Apple'},{'Name':'Green Apple'},{'Name':'Granny Smith Apple'},{'Name':null}]";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "Granny Smith Apple","Green Apple", "Red Apple", null };

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = ifhJson.GetParsedData(json);

            //assert
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }

        [TestMethod]
        public void File_WithValidJSONNumericDataSorted()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //create json string
            string json = "[{'Age':55},{'Age':92},{'Age':25},{'Age':null}]";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "25", "55", "92", null };

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = ifhJson.GetParsedData(json);

            //assert
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }

        [TestMethod]
        public void File_WithValidJSONAlphaNumericDataSorted()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //create json string
            string json = "[{'Name':'Red Apple'},{'Name':'Green Apple'},{'Name':null},{'Age':92},{'Age':55},{'Age':null}]";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "55", "92", "Green Apple", "Red Apple", null, null};

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = ifhJson.GetParsedData(json);

            //assert
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }

        [TestMethod]
        public void File_WithValidJSONNoValue()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            List<string> parsedData = ifhJson.GetParsedData("[{ 'Name':'Red Apple'},{ 'Name':'Green Apple'},{'Name':null}]").ToList();
            parsedData = parsedData.Select(x => x != null ? x : "No Value").ToList();

            //assert
            Assert.IsTrue(parsedData.Any(pd => pd == "No Value"));
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
