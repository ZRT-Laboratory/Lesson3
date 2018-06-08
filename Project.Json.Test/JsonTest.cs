using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.Interface;
using System;
using System.IO;
using Project.ConsoleApp;

namespace Project.Json.Test
{
    [TestClass]
    public class JsonTest
    {
        string _validJSONFile = string.Empty;
        string _invalidJSONFile = string.Empty;

        public JsonTest()
        {
            string testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";

            _validJSONFile = testFilePath + "JSON_ValidFormat.json";
            _invalidJSONFile = testFilePath + "JSON_InvalidFormat.json";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_validJSONFile));
            Assert.IsTrue(File.Exists(_invalidJSONFile));
        }

        [TestMethod]
        public void Arguments_WithValidJSONArgument()
        {
            //assert
            try
            {
                Program.Main(new string[] { "-json", _validJSONFile});
            }
            catch (ArgumentException aex)
            {
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument()
        {
            //assert
            try
            {
                Program.Main(new string[] { "json", _validJSONFile});
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }

        [TestMethod]
        public void Arguments_WithMissingJSONArgument()
        {
            //assert
            Assert.ThrowsException<ArgumentException>(() => Program.Main(Array.Empty<string>()));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //assert
            try
            {
                Program.Main(new string[] { "-json", _validJSONFile, "-test1", _validJSONFile, "-test2", _validJSONFile });
            }
            catch (ArgumentException aex)
            {
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //assert
            Assert.IsTrue(ifhJson.GetParsedData(_validJSONFile).Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidJSONFormat()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //assert
            Assert.ThrowsException<JsonSerializationException>(() => ifhJson.GetParsedData(_invalidJSONFile));
        }
    }
}
