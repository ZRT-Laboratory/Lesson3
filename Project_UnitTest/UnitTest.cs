using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Console;
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
            Assert.IsTrue(File.Exists(new ArgumentXml().GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath + _validJSONFile, "-xml", _testFilePath + _validXMLFile, "-test", _testFilePath };

            //assert
            Assert.IsTrue(File.Exists(new ArgumentJson().GetFilePath(argArray, "-json")));
            Assert.IsTrue(File.Exists(new ArgumentXml().GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] argArray = new string[] { "json", _testFilePath, "xml", _testFilePath };

            //act
            bool invalidArguments = string.IsNullOrEmpty(new ArgumentJson().GetFilePath(argArray, "-json")) &&
                                    string.IsNullOrEmpty(new ArgumentXml().GetFilePath(argArray, "-xml"));

            //assert
            Assert.IsTrue(invalidArguments);
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentJson().GetFilePath(new string[] { "json", _testFilePath }, "-json")));
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentXml().GetFilePath(new string[] { "xml", _testFilePath }, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //act
            bool missingArguments = string.IsNullOrEmpty(new ArgumentJson().GetFilePath(Array.Empty<string>(), "-json")) &&
                                    string.IsNullOrEmpty(new ArgumentXml().GetFilePath(Array.Empty<string>(), "-xml"));

            //assert
            Assert.IsTrue(missingArguments);
        }

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //act
            var xmlValueItems = new ArgumentXml().GetFileData(_testFilePath + _validXMLFile);

            //assert
            Assert.IsTrue(xmlValueItems.Length > 0);
            CollectionAssert.AllItemsAreNotNull(xmlValueItems, "Null values in XML.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No Exception was thrown.")]
        public void File_WithInvalidXMLFormat()
        {
            //act
            var xmlValueItems = new ArgumentXml().GetFileData(_testFilePath + _invalidXMLFile);
        }

        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //act
            var jsonValueItems = new ArgumentJson().GetFileData(_testFilePath + _validJSONFile);

            //assert
            Assert.IsTrue(jsonValueItems.Length > 0);
            CollectionAssert.AllItemsAreNotNull(jsonValueItems, "Null values in XML.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No Exception was thrown.")]
        public void File_WithInvalidJSONFormat()
        {
            //act
            var xmlValueItems = new ArgumentJson().GetFileData(_testFilePath + _invalidJSONFile);
        }
    }
}
