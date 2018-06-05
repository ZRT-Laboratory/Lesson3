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
        public void Arguments_WithValidArguments()
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
        public void Arguments_WithInvalidArguments()
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
        public void Arguments_WithMissingArguments()
        {
            //assert
            try
            {
                Program.Main(Array.Empty<string>());
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            IFileHandling ifh = new JsonParser(new string[] { "-json", _validJSONFile });

            //assert
            Assert.IsTrue(ifh.GetParsedData(_validJSONFile).Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidJSONFormat()
        {
            //arrange
            IFileHandling ifh = new JsonParser(new string[] { "-json", _invalidJSONFile });

            //assert
            Assert.ThrowsException<JsonSerializationException>(() => ifh.GetParsedData(_invalidJSONFile));
        }
    }
}
