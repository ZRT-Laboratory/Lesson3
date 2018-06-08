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
        #region " Test Methods "

        [TestMethod]
        public void Arguments_WithValidXMLArgument()
        {
            //arrange
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' />", "</xml>" });

            try
            {
                //act
                Program.Main(new string[] { "-xml", xmlFile });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
            finally
            {
                DeleteTempFile(xmlFile);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidXMLArgument()
        {
            //arrange
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' />", "</xml>" } );

            //assert
            try
            {
                Assert.ThrowsException<ArgumentException>(() => Program.Main(new string[] { "xml", xmlFile, }));
            }
            finally
            {
                DeleteTempFile(xmlFile);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingXMLArgument() => Assert.ThrowsException<ArgumentException>(() => Program.Main(Array.Empty<string>()));

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' />", "</xml>" });

            try
            {
                //act
                Program.Main(new string[] { "-xml", xmlFile, "-test1", xmlFile, "-test2", xmlFile, });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
            finally
            {
                DeleteTempFile(xmlFile);
            }
        }

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();
            
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' />", "</xml>" });

            //assert
            try
            {
                Assert.IsTrue(ifhXml.GetParsedData(xmlFile).Length > 0);
            }
            finally
            {
                DeleteTempFile(xmlFile);
            }
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser();

            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' >", "</xml>" });

            //assert
            try
            {
                Assert.ThrowsException<XmlException>(() => ifh.GetParsedData(xmlFile));
            }
            finally
            {
                DeleteTempFile(xmlFile);
            }
        }

        [TestMethod]
        public void File_WithValidXMLDataSortedProperly()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();

            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' />", "<orange id = 'Naval Orange' />", "<orange id = 'Blood Orange' />", "<orange />", "</xml>" });

            //create the expected sorted results
            string[] expectedResults = new string[] { "Blood Orange", "Naval Orange", "Round Orange", null };

            //act
            //create a list then order it so nulls are last in the list
            string[] testResults = ifhXml.GetParsedData(xmlFile)
                .OrderBy(ifh => ifh)
                .ToArray()
                .OrderBy(ifh => ifh == null)
                .ToArray();

            //assert
            try
            {
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                DeleteTempFile(xmlFile);
            }
        }

        #endregion

        #region  " Non Test Methods "

        /// <summary>
        /// CreateTempFile
        /// </summary>
        /// <param name="fileData">array containing data you want to write to the file. pass an empty array if no data needed</param>
        /// <returns>filepath of new temp file</returns>
        string CreateTempFile(string[] fileData)
        {
            string file = Path.GetTempFileName();

            if (fileData.Length > 0)
            {
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    foreach (string dataItem in fileData)
                    {
                        writer.WriteLine(dataItem);
                    }
                }
            }

            return file;
        }

        /// <summary>
        /// DeleteTempFile
        /// </summary>
        /// <param name="file">temp file name to delete</param>
        void DeleteTempFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        #endregion
    }
}
