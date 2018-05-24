using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project_Json;
using System;
using System.IO;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        string _testFilePath = string.Empty;
        const string _validJSONFile = "JSON_ValidFormat.json";
        const string _invalidJSONFile = "JSON_InvalidFormat.json";
        const string _validXMLFile = "XML_ValidFormat.xml";
        const string _invalidXMLFile = "XML_InvalidFormat.xml";

        public UnitTest()
        {
            _testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_testFilePath + _validJSONFile));
            Assert.IsTrue(File.Exists(_testFilePath + _invalidJSONFile));
            Assert.IsTrue(File.Exists(_testFilePath + _validXMLFile));
            Assert.IsTrue(File.Exists(_testFilePath + _invalidXMLFile));
        }

        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath + _validJSONFile, "-xml", _testFilePath + _validXMLFile };

            //assert
            Assert.IsTrue(File.Exists(new ArgumentJson().GetFilePath(argArray, "-json")));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //assert
            Assert.IsTrue(File.Exists(new ArgumentJson().GetFilePath(new string[] { "-json", _testFilePath + _validJSONFile, "-test1", _testFilePath, "-test2", _testFilePath }, "-json")));
        }

        [TestMethod]
        public void Arguments_WithInvalidArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentJson().GetFilePath(new string[] { "json", _testFilePath }, "-json")));
        }

        [TestMethod]
        public void Arguments_WithMissingArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentJson().GetFilePath(Array.Empty<string>(), "-json")));
        }

        [TestMethod]
        public void File_WithValidFormat()
        {
            //act
            var jsonItems = new ArgumentJson().GetFileData(_testFilePath + _validJSONFile);

            //assert
            Assert.IsTrue(jsonItems.Length > 0);
            CollectionAssert.AllItemsAreNotNull(jsonItems, "Null values in XML.");
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException), "No Exception was thrown.")]
        public void File_WithInvalidFormat()
        {
            //act
            var xmlItems = new ArgumentJson().GetFileData(_testFilePath + _invalidJSONFile);
        }
    }
}
