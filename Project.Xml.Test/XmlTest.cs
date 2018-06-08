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
            //assert
            Assert.IsTrue(File.Exists(_validXMLFile));
            Assert.IsTrue(File.Exists(_invalidXMLFile));
        }

        [TestMethod]
        public void Arguments_WithValidXMLArgument()
        {
            try
            {
                //act
                Program.Main(new string[] { "-xml", _validXMLFile });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //assert
            Assert.ThrowsException<ArgumentException>(() => Program.Main(new string[] { "xml", _validXMLFile, }));
        }

        [TestMethod]
        public void Arguments_WithMissingXMLArgument()
        {
            //assert
            Assert.ThrowsException<ArgumentException>(() => Program.Main(Array.Empty<string>()));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            try
            {
                //act
                Program.Main(new string[] { "-xml", _validXMLFile, "-test1", _validXMLFile, "-test2", _validXMLFile, });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser();

            //assert
            Assert.IsTrue(ifh.GetParsedData(_validXMLFile).Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser();

            //assert
            Assert.ThrowsException<XmlException>(() => ifh.GetParsedData(_invalidXMLFile));
        }
    }
}
