using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project_Console;
using System;
using System.IO;
using System.Xml;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        const string _validJSONFile = "JSON_ValidFormat.json";
        const string _invalidJSONFile = "JSON_InvalidFormat.json";
        const string _validXMLFile = "XML_ValidFormat.xml";
        const string _invalidXMLFile = "XML_InvalidFormat.xml";

        string _testFilePath = string.Empty;

        IFileHandling _ifhJson = new ArgumentJson();
        IFileHandling _ifhXml = new ArgumentXml();

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
            Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(argArray, "-json")) && File.Exists(_ifhXml.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] argArray = new string[] { "json", _testFilePath + _validJSONFile, "xml", _testFilePath + _validXMLFile };

            //act
            bool invalidArguments = string.IsNullOrEmpty(_ifhJson.GetFilePath(argArray, "-json")) &&
                                    string.IsNullOrEmpty(_ifhXml.GetFilePath(argArray, "-xml"));

            //assert
            Assert.IsTrue(invalidArguments);
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath + _validJSONFile, "-xml", _testFilePath + _validXMLFile, "-test", _testFilePath };

            //assert
            Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(argArray, "-json")));
            Assert.IsTrue(File.Exists(_ifhXml.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(new string[] { "json", _testFilePath + _validJSONFile }, "-json")));
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(_ifhXml.GetFilePath(new string[] { "xml", _testFilePath + _validXMLFile }, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //act
            bool missingArguments = string.IsNullOrEmpty(_ifhJson.GetFilePath(Array.Empty<string>(), "-json")) &&
                                    string.IsNullOrEmpty(_ifhXml.GetFilePath(Array.Empty<string>(), "-xml"));

            //assert
            Assert.IsTrue(missingArguments);
        }

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //act
            var items = _ifhXml.GetFileData(_testFilePath + _validXMLFile);

            //assert
            Assert.IsTrue(items.Length > 0);
            CollectionAssert.AllItemsAreNotNull(items, "Null values in XML.");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException), "No Exception was thrown.")]
        public void File_WithInvalidXMLFormat()
        {
            //act
            var items = _ifhXml.GetFileData(_testFilePath + _invalidXMLFile);
        }

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //act
            var items = _ifhJson.GetFileData(_testFilePath + _validJSONFile);

            //assert
            Assert.IsTrue(items.Length > 0);
            CollectionAssert.AllItemsAreNotNull(items, "Null values in XML.");
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
