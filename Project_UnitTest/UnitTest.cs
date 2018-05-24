using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Xml;
using System;
using System.IO;
using System.Xml;

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
            //assert
            Assert.IsTrue(File.Exists(_ifhXml.GetFilePath(new string[] { "-xml", _testFilePath + _validXMLFile, "-json", _testFilePath + _validJSONFile }, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(_ifhXml.GetFilePath(new string[] { "xml", _testFilePath }, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //assert
            Assert.IsTrue(File.Exists(_ifhXml.GetFilePath(new string[] { "-xml", _testFilePath + _validXMLFile, "-test1", _testFilePath, "-test2", _testFilePath }, "-xml")));
        }

        [TestMethod]
        public void Arguments_WithMissingArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(_ifhXml.GetFilePath(Array.Empty<string>(), "-xml")));
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
            var items = _ifhXml.GetFileData(_testFilePath + _invalidJSONFile);
        }
    }
}
