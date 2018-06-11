using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.Interface;
using System;
using System.IO;
using Project.ConsoleApp;
using System.Linq;

namespace Project.Json.Test
{
    [TestClass]
    public class JsonTest
    {
        #region " Test Methods "

        [TestMethod]
        public void Arguments_WithValidJSONArgument()
        {
            //arrange
            string jsonFile = CreateTempFile(new string[] { "[{'Name':'Red Apple'},{ 'Name':'Green Apple'}]" });

            try
            {
                //act
                Program.Main(new string[] { "-json", jsonFile });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
            finally
            {
                DeleteTempFile(jsonFile);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument()
        {
            //arrange
            string jsonFile = CreateTempFile(new string[] { "[{'Name':'Red Apple'},{ 'Name':'Green Apple'}]" });

            //assert
            try
            {
                Assert.ThrowsException<ArgumentException>(() => Program.Main(new string[] { "json", jsonFile }));
            }
            finally
            {
                DeleteTempFile(jsonFile);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingJSONArgument() => Assert.ThrowsException<ArgumentException>(() => Program.Main(Array.Empty<string>()));

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string jsonFile = CreateTempFile(new string[] { "[{'Name':'Red Apple'},{ 'Name':'Green Apple'}]" });

            try
            {
                //act
                Program.Main(new string[] { "-json", jsonFile, "-test1", jsonFile, "-test2", jsonFile });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
            finally
            {
                DeleteTempFile(jsonFile);
            }
        }

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
        public void File_WithValidJSONDataSortedProperly()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //create json string
            string json = "[{'Name':'Red Apple'},{'Name':'Green Apple'},{'Name':'Granny Smith Apple'},{'Name':null}]";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "Granny Smith Apple","Green Apple", "Red Apple",null };

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = Program.GetSortedData(ifhJson.GetParsedData(json)).ToArray();

            //assert
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
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
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    foreach (string dataItem in fileData)
                    {
                        writer.WriteLine(dataItem);
                    }
                }
            }

            return file;
        }

        /// <summary>
        /// DeleteTempFile
        /// </summary>
        /// <param name="file">temp file name to delete</param>
        void DeleteTempFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        #endregion
    }
}
