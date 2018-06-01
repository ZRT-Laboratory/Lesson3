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
            IFileHandling ifhArgument = new Argument(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile });

            //assert
            Assert.IsTrue(File.Exists(ifhArgument.GetFilePath("-json")) && File.Exists(ifhArgument.GetFilePath("-xml")));
        }        

        [TestMethod]
        public void Arguments_WithInvalidJSONArgument()
        {
            //arrange
            IFileHandling ifhArgument = new Argument(new string[] { "json", _validJSONFile, "-xml", _validXMLFile, });

            //assert
            Assert.IsFalse(File.Exists(ifhArgument.GetFilePath("-json")));
            Assert.IsTrue(File.Exists(ifhArgument.GetFilePath("-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //arrange
            IFileHandling ifhArgument = new Argument(new string[] { "-json", _validJSONFile, "xml", _validXMLFile });

            //assert
            Assert.IsTrue(File.Exists(ifhArgument.GetFilePath("-json")));
            Assert.IsFalse(File.Exists(ifhArgument.GetFilePath("-xml")));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            IFileHandling ifhArgument = new Argument(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile, "-test", _validXMLFile });

            //assert
            Assert.IsTrue(File.Exists(ifhArgument.GetFilePath("-json")) && File.Exists(ifhArgument.GetFilePath("-xml")));
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //arrange
            IFileHandling ifhArgument = new Argument(Array.Empty<string>());

            //assert
            Assert.IsFalse(File.Exists(ifhArgument.GetFilePath("-json")) && File.Exists(ifhArgument.GetFilePath("-xml")));
        }
    }
}
