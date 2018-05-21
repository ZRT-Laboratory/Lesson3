using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Json;

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

        #region  ' Json Tests '

        [TestMethod]
        public void JsonArgument_WithValidAbsolutePath()
        {
            //assert
            Assert.IsTrue(File.Exists(new ArgumentJson().GetFilePathFromArgument(new string[] { "-json", _testFilePath + _validJSONFile }, "-json")));
        }

        [TestMethod]
        public void JsonArgument_WithValidRelativePath()
        {
            //assert
            Assert.IsTrue(Directory.Exists(new ArgumentJson().GetFilePathFromArgument(new string[] { "-json", _testFilePath }, "-json")));
        }

        [TestMethod]
        public void JsonArgument_WithTooManyArguments()
        {
            //assert
            Assert.IsTrue(File.Exists(new ArgumentJson().GetFilePathFromArgument(new string[] { "-json", _testFilePath + _validJSONFile, "-test1", _testFilePath, "-test2", _testFilePath }, "-json")));
        }

        [TestMethod]
        public void JsonArgument_WithInvalidArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentJson().GetFilePathFromArgument(new string[] { "json", _testFilePath }, "-json")));
        }

        [TestMethod]
        public void JsonArgument_WithMissingArgument()
        {
            //assert
            Assert.IsTrue(string.IsNullOrEmpty(new ArgumentJson().GetFilePathFromArgument(Array.Empty<string>(), "-json")));
        }

        [TestMethod]
        public void JsonFile_WithValidFormat()
        {
            //act
            var jsonItems = new ArgumentJson().GetValuesFromFile(_testFilePath + _validJSONFile);

            //assert
            Assert.IsTrue(jsonItems.Length > 0);
            CollectionAssert.AllItemsAreNotNull(jsonItems, "Null values in XML.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No Exception was thrown.")]
        public void JsonFile_WithInvalidFormat()
        {
            //act
            var xmlItems = new ArgumentJson().GetValuesFromFile(_testFilePath + _invalidJSONFile);
        }

        #endregion
    }
}
