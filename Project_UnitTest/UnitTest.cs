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

        IFileHandling _ifhArgument = new Argument();

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

            //assert
            Assert.IsFalse(File.Exists(_ifhArgument.GetFilePath(argArray, "-json")) && File.Exists(_ifhArgument.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _validJSONFile, "-xml", _validXMLFile, "-test", _validXMLFile };

            //assert
            Assert.IsTrue(File.Exists(_ifhArgument.GetFilePath(argArray, "-json")) && File.Exists(_ifhArgument.GetFilePath(argArray, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhArgument.GetFilePath(new string[] { "json", _validJSONFile }, "-json")));

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhArgument.GetFilePath(new string[] { "xml", _validXMLFile }, "-xml")));

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //assert
            Assert.IsFalse(File.Exists(_ifhArgument.GetFilePath(Array.Empty<string>(), "-json")) && File.Exists(_ifhArgument.GetFilePath(Array.Empty<string>(), "-xml")));
        }
    }
}
