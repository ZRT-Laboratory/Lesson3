using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void ValidArguments()
        {
            //arrange
            string[] argArray = new string[] { "-json", _testFilePath, "-xml", _testFilePath };

            //act
            bool valid = argArray.Any(a => String.Compare(a, "-json", true) == 0) || argArray.Any(a => String.Compare(a, "-xml", true) == 0);

            //assert
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void InvalidArguments()
        {
            //arrange
            string[] argArray = new string[] { "json", _testFilePath, "xml", _testFilePath };

            //act
            bool valid = argArray.Any(a => String.Compare(a, "-json", true) == 0) || argArray.Any(a => String.Compare(a, "-xml", true) == 0);

            //assert
            Assert.IsFalse(valid);
        }
    }
}
