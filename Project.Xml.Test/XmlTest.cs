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
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] {"<xml>", "<orange id = 'Round Orange' />", "<orange id = 'Naval Orange' />", "<orange id = 'Blood Orange' />", "<orange /></xml>" });            
            string[] expectedResults = new string[] { "Blood Orange", "Naval Orange", "Round Orange", "No Value" };

            //act
            string[] testResults = testArray.GetSortedFileData(new string[] { "-xml", testFile }).ToArray();

            //assert            
            try
            {
                //validate that the sorted testresults match the sorted expected results
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void File_WithValidXMLNumericDataSorted()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "<xml>", "<age id = '55' />", "<age id = '92' />", "<age id = '25' />", "<age /></xml>" });
            string[] expectedResults = new string[] { "25", "55", "92", "No Value" };

            //act
            string[] testResults = testArray.GetSortedFileData(new string[] { "-xml", testFile }).ToArray();

            //assert            
            try
            {
                //validate that the sorted testresults match the sorted expected results
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void File_WithValidXMLAlphaNumericDataSorted()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "<xml>","<orange id = 'Round Orange' />","<orange id = 'Naval Orange' />","<orange/>","<age id = '92' />","<age id = '55' />","<age /></xml>" });
            string[] expectedResults = new string[] { "55", "92", "Naval Orange", "Round Orange", "No Value", "No Value" };

            //act
            string[] testResults = testArray.GetSortedFileData(new string[] { "-xml", testFile }).ToArray();

            //assert            
            try
            {
                //validate that the sorted testresults match the sorted expected results
                for (int i = 0; i < expectedResults.Length; i++)
                {
                    Assert.AreEqual(testResults[i], expectedResults[i]);
                }
            }
            finally
            {
                File.Delete(testFile);
            }
        }

        [TestMethod]
        public void File_WithValidXMNoValue()
        {
            //arrange
            string[] testArray = Array.Empty<string>();
            string testFile = CreateTempFile(new string[] { "<xml>", "<orange id = 'Round Orange' />", "<orange/>", "<age id = '55' />", "<age /></xml>" });

            //act
            string[] testResults = testArray.GetSortedFileData(new string[] { "-xml", testFile }).ToArray();

            //assert            
            try
            {
                Assert.IsTrue(testResults.Any(pd => pd == "No Value"));
            }
            finally
            {
                File.Delete(testFile);
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

        #endregion
    }
}
