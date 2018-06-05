using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Interface;
using Project.ConsoleApp;
using System;
using System.IO;
using System.Xml;

namespace Project.Xml.Test
{
    [TestClass]
    public class XmlTest
    {
        string _validXMLFile = string.Empty;
        string _invalidXMLFile = string.Empty;

        public XmlTest()
        {
            string testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";

            _validXMLFile = testFilePath + "XML_ValidFormat.xml";
            _invalidXMLFile = testFilePath + "XML_InvalidFormat.xml";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_validXMLFile));
            Assert.IsTrue(File.Exists(_invalidXMLFile));
        }

        [TestMethod]
        public void Arguments_WithValidXMLArgument()
        {
            //assert
            try
            {
                Program.Main(new string[] { "-xml", _validXMLFile });
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
                Program.Main(new string[] { "xml", _validXMLFile, });
            }
            catch (ArgumentException aex)
            {
                Assert.IsTrue(aex.Message == "Invalid arguments.");
            }
        }

        [TestMethod]
        public void Arguments_WithMissingXMLArgument()
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
        public void Arguments_WithTooManyArguments()
        {
            //assert
            try
            {
                Program.Main(new string[] { "-xml", _validXMLFile, "-test1", _validXMLFile, "-test2", _validXMLFile, });
            }
            catch (ArgumentException aex)
            {
                Assert.Fail(aex.Message);
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
