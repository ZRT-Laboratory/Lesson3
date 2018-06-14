using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.Interface;
using System.IO;

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
