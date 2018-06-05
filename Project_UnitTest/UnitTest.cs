using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Interface;
using Project.ConsoleApp;
using System;
using System.IO;
using System.Xml;
using Project.Xml;

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
            //assert
            try
            {
                Program.Main(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile });
            }
            catch (ArgumentException aex)
            {
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //assert
            try
            {
                Program.Main(new string[] { "-json", _validJSONFile, "xml", _validXMLFile, });
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //assert
            try
            {
                Program.Main(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile, "-test2", _validJSONFile });
            }
            catch (ArgumentException aex)
            {
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingArgument()
        {
            //assert
            try
            {
                Program.Main(Array.Empty<string>());
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser(new string[] { "-xml", _validXMLFile });

            //assert
            Assert.IsTrue(ifh.GetParsedData(_validXMLFile).Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser(new string[] { "-xml", _invalidXMLFile });

            //assert
            Assert.ThrowsException<XmlException>(() => ifh.GetParsedData(_invalidXMLFile));
        }
    }
}
