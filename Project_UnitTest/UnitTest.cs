using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Interface;
using Project_Xml;
using System;
using System.IO;
using System.Xml;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        string _validJSONFile = string.Empty;
        string _invalidJSONFile = string.Empty;
        string _validXMLFile = string.Empty;
        string _invalidXMLFile = string.Empty;

        IFileHandling _ifhXml = new ArgumentXml();

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
            Assert.IsTrue(File.Exists( _validJSONFile));
            Assert.IsTrue(File.Exists( _invalidJSONFile));
            Assert.IsTrue(File.Exists( _validXMLFile));
            Assert.IsTrue(File.Exists( _invalidXMLFile));
        }        

        [TestMethod]
        public void Arguments_WithValidArguments() => Assert.IsTrue(File.Exists(_ifhXml.GetFilePath(new string[] { "-xml",  _validXMLFile, "-json",  _validJSONFile }, "-xml")));

        [TestMethod]
        public void Arguments_WithInvalidArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhXml.GetFilePath(new string[] { "xml", _validXMLFile }, "-xml")));

        [TestMethod]
        public void Arguments_WithTooManyArguments() => Assert.IsTrue(File.Exists(_ifhXml.GetFilePath(new string[] { "-xml",  _validXMLFile, "-test1", _validXMLFile, "-test2", _validXMLFile }, "-xml")));

        [TestMethod]
        public void Arguments_WithMissingArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhXml.GetFilePath(Array.Empty<string>(), "-xml")));

        [TestMethod]
        public void File_WithValidXMLFormat() => Assert.IsTrue(_ifhXml.GetParsedData(new string[] { "-xml", _validXMLFile }).Length > 0);

        [TestMethod]
        public void File_WithInvalidXMLFormat() => Assert.ThrowsException<XmlException>(() => _ifhXml.GetParsedData(new string[] { "-xml", _invalidXMLFile }));
    }
}
