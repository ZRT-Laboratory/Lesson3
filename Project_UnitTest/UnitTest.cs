using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Console;
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

        string _testFilePath = string.Empty;

        IFileHandling _ifhArgument = new Argument();

        public UnitTest()
        {
            _testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";

            _validJSONFile = _testFilePath + "JSON_ValidFormat.json";
            _invalidJSONFile = _testFilePath + "JSON_InvalidFormat.json";
            _validXMLFile = _testFilePath + "XML_ValidFormat.xml";
            _invalidXMLFile = _testFilePath + "XML_InvalidFormat.xml";
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
        public void Arguments_WithValidArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _validJSONFile, "-xml", _validXMLFile };

            //assert
            Assert.IsTrue(File.Exists(_ifhArgument.GetFilePath(argArray, "-json")) && File.Exists(_ifhArgument.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //arrange
            string[] argArray = new string[] { "json", _validJSONFile, "xml", _validXMLFile };

            //act
            bool invalidArguments = string.IsNullOrEmpty(_ifhArgument.GetFilePath(argArray, "-json")) &&
                                    string.IsNullOrEmpty(_ifhArgument.GetFilePath(argArray, "-xml"));

            //assert
            Assert.IsTrue(invalidArguments);
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _validJSONFile, "-xml", _validXMLFile, "-test", _testFilePath };

            //assert
            Assert.IsTrue(File.Exists(_ifhArgument.GetFilePath(argArray, "-json")));
            Assert.IsTrue(File.Exists(_ifhArgument.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhArgument.GetFilePath(new string[] { "json", _validJSONFile }, "-json")));

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhArgument.GetFilePath(new string[] { "xml", _validXMLFile }, "-xml")));

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //act
            bool missingArguments = string.IsNullOrEmpty(_ifhArgument.GetFilePath(Array.Empty<string>(), "-json")) &&
                                    string.IsNullOrEmpty(_ifhArgument.GetFilePath(Array.Empty<string>(), "-xml"));

            //assert
            Assert.IsTrue(missingArguments);
        }
    }
}
