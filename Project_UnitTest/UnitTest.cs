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
        public void Arguments_WithValidArguments()
        {
            //arrange
            IFileHandling ifh = new ArgumentXml(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile });

            //assert
            Assert.IsTrue(File.Exists(ifh.GetFilePath("-json")) && File.Exists(ifh.GetFilePath("-xml")));
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //arrange
            IFileHandling ifh = new ArgumentXml(new string[] { "-json", _validJSONFile, "xml", _validXMLFile, });

            //assert
            Assert.IsFalse(File.Exists(ifh.GetFilePath("-xml")));
            Assert.IsTrue(File.Exists(ifh.GetFilePath("-json")));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            IFileHandling ifh = new ArgumentXml(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile, "-test2", _validJSONFile });

            //assert
            Assert.IsTrue(File.Exists(ifh.GetFilePath("-json")) && File.Exists(ifh.GetFilePath("-xml")));
        }

        [TestMethod]
        public void Arguments_WithMissingArgument()
        {
            //arrange
            IFileHandling ifh = new ArgumentXml(Array.Empty<string>());

            //assert
            Assert.IsFalse(File.Exists(ifh.GetFilePath("-json")) && File.Exists(ifh.GetFilePath("-xml")));
        }

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new ArgumentXml(new string[] { "-xml", _validXMLFile });

            //assert
            Assert.IsTrue(ifh.GetParsedData("-xml").Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new ArgumentXml(new string[] { "-xml", _invalidXMLFile });

            //assert
            Assert.ThrowsException<XmlException>(() => ifh.GetParsedData("-xml"));
        }
    }
}
