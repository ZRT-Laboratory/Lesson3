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

        IFileHandling _ifhJson = new ArgumentJson();

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
        public void Arguments_WithValidArguments() => Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(new string[] { "-json", _testFilePath + _validJSONFile, "-xml", _testFilePath + _validXMLFile }, "-json")));

        [TestMethod]
        public void Arguments_WithInvalidArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(new string[] { "json", _testFilePath }, "-json")));

        [TestMethod]
        public void Arguments_WithTooManyArguments() => Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(new string[] { "-json", _testFilePath + _validJSONFile, "-test1", _testFilePath, "-test2", _testFilePath }, "-json")));

        [TestMethod]
        public void Arguments_WithMissingArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(Array.Empty<string>(), "-json")));

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //act
            var items = _ifhJson.GetFileData(_testFilePath + _validJSONFile);

            //assert
            Assert.IsTrue(items.Length > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException), "No Exception was thrown.")]
        public void File_WithInvalidJSONFormat()
        {
            //act
            var items = _ifhJson.GetFileData(_testFilePath + _invalidJSONFile);
        }
    }
}
