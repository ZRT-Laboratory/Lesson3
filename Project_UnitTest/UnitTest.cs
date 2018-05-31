using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project_Interface;
using Project_Json;
using System;
using System.IO;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        string _validJSONFile = string.Empty;
        string _invalidJSONFile = string.Empty;
        string _validXMLFile = string.Empty;
        string _invalidXMLFile = string.Empty;

        IFileHandling _ifhJson = new ArgumentJson();

        public UnitTest()
        {
            string testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";

            _validJSONFile = testFilePath + "JSON_ValidFormat.json";
            _invalidJSONFile = testFilePath + "JSON_InvalidFormat.json";
            _validXMLFile = testFilePath + "XML_ValidFormat.xml";
            _invalidXMLFile = testFilePath + "XML_InvalidFormat.xml";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_validJSONFile));
            Assert.IsTrue(File.Exists(_invalidJSONFile));
            Assert.IsTrue(File.Exists(_validXMLFile));
            Assert.IsTrue(File.Exists(_invalidXMLFile));
        }

        [TestMethod]
        public void Arguments_WithValidArguments() => Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile }, "-json")));

        [TestMethod]
        public void Arguments_WithInvalidArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(new string[] { "json", _validJSONFile }, "-json")));

        [TestMethod]
        public void Arguments_WithTooManyArguments() => Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(new string[] { "-json", _validJSONFile, "-test1", _validJSONFile, "-test2", _validJSONFile }, "-json")));

        [TestMethod]
        public void Arguments_WithMissingArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(Array.Empty<string>(), "-json")));

        [TestMethod]
        public void File_WithValidJSONFormat() => Assert.IsTrue(_ifhJson.GetParsedData(new string[] { "-json", _validJSONFile }).Length > 0);

        [TestMethod]
        public void File_WithInvalidJSONFormat() => Assert.ThrowsException<JsonSerializationException>(() => _ifhJson.GetParsedData(new string[] { "-json", _invalidJSONFile }));
    }
}
