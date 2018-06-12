using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.ConsoleApp;
using Project.Interface;
using System;
using System.IO;
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
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange'/>", "<orange id = 'Naval Orange'/>", "</xml>" });

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
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange'/>", "<orange id = 'Naval Orange'/>", "</xml>" });

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
            string xmlFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange'/>", "<orange id = 'Naval Orange'/>", "</xml>" });

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
        public void Arguments_WithInvalidXMLFileName() => Assert.ThrowsException<ArgumentException>(() => Program.Main(new string[] { "-xml", "BadFile.txt"}));

        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();

            //assert
            Assert.IsTrue(ifhXml.GetParsedData("<xml>,<orange id = 'Round Orange'/>,<orange id = 'Naval Orange'/>,</xml>").Length > 0);
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser();

            //assert
            Assert.ThrowsException<XmlException>(() => ifh.GetParsedData("<xml>, <orange id = 'Round Orange' >, </xml>"));
        }

        [TestMethod]
        public void File_WithValidXMLNonNumericDataSorted()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();

            //create xml string
            string xml = "<xml> <orange id = 'Round Orange' /><orange id = 'Naval Orange' /><orange id = 'Blood Orange' /><orange /></xml>";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "Blood Orange", "Naval Orange", "Round Orange", null };

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = Program.GetSortedData(ifhXml.GetParsedData(xml)).ToArray();

            //assert
            //validate that the sorted testresults match the sorted expected results
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }

        [TestMethod]
        public void File_WithValidXMLNumericDataSorted()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();

            //create xml string
            string xml = "<xml> <age id = '55' /><age id = '92' /><age id = '25' /><orange /></xml>";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "25", "55", "92", null };

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = Program.GetSortedData(ifhXml.GetParsedData(xml)).ToArray();

            //assert
            //validate that the sorted testresults match the sorted expected results
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
            }
        }

        [TestMethod]
        public void File_WithValidXMLAlphaNumericDataSorted()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();

            //create xml string
            string xml = "<xml> <orange id = 'Round Orange' /><orange id = 'Naval Orange' /><orange /><age id = '55' /><age id = '92' /><age /></xml>";

            //create the expected results in the expected sort order
            string[] expectedResults = new string[] { "55", "92", "Naval Orange", "Round Orange", null, null };

            //act
            //create the testresults sorted with nulls at the end
            string[] testResults = Program.GetSortedData(ifhXml.GetParsedData(xml)).ToArray();

            //assert
            //validate that the sorted testresults match the sorted expected results
            for (int i = 0; i < expectedResults.Length; i++)
            {
                Assert.AreEqual(testResults[i], expectedResults[i]);
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
