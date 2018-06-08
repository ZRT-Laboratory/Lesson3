using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.ConsoleApp;
using Project.Interface;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace Project.Xml.Test
{
    [TestClass]
    public class XmlTest
    {
        static string _validXMLFile = string.Empty;
        static string _invalidXMLFile = string.Empty;

        [ClassInitialize]
        public static void ClassInit(TestContext tc)
        {
            //create valid XML file
            _validXMLFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(_validXMLFile, true))
            {
                writer.WriteLine("<xml>");
                writer.WriteLine("<orange id = 'Round Orange' />");
                writer.WriteLine("<orange id = 'Naval Orange' />");
                writer.WriteLine("<orange id = 'Blood Orange' />");
                writer.WriteLine("<orange />");
                writer.WriteLine("</xml>");
            }

            //create invalid XML file
            _invalidXMLFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(_invalidXMLFile, true))
            {
                writer.WriteLine("<xml>");
                writer.WriteLine("<orange id = 'Round Orange' />");
                writer.WriteLine("<orange id = 'Naval Orange' />");
                writer.WriteLine("<orange id = 'Blood Orange' >");
                writer.WriteLine("</xml>");
            }
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            if (File.Exists(_validXMLFile))
            {
                File.Delete(_validXMLFile);
            }

            if (File.Exists(_invalidXMLFile))
            {
                File.Delete(_invalidXMLFile);
            }
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
            IFileHandling ifhXml = new XmlParser();

            //assert
            Assert.IsTrue(ifhXml.GetParsedData(_validXMLFile).Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser();

            //assert
            Assert.ThrowsException<XmlException>(() => ifh.GetParsedData(_invalidXMLFile));
        }

        [TestMethod]
        public void File_WithValidXMLDataSortedProperly()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();
            
            string xmlFile = Path.GetTempFileName();
            using (StreamWriter writer = new StreamWriter(xmlFile, true))
            {
                writer.WriteLine("<xml>");
                writer.WriteLine("<orange id = 'Yellow Orange' />");
                writer.WriteLine("<orange id = 'Black Orange' />");
                writer.WriteLine("<orange id = 'Blue Orange' />");
                writer.WriteLine("<orange />");
                writer.WriteLine("</xml>");
            }

            //create the expected sorted results
            string[] expectedResults = new string[] { "Black Orange", "Blue Orange", "Yellow Orange", null };

            //act
            //create a list then order it so nulls are last in the list
            string[] testResults = ifhXml.GetParsedData(xmlFile)
                .OrderBy(ifh => ifh)
                .ToArray()
                .OrderBy(ifh => ifh == null)
                .ToArray();

            //assert
            for (int i =0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }
    }
}
