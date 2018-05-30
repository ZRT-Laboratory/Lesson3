﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project_Interface;
using Project_Json;
using System;
using System.IO;

namespace Project_UnitTest
{
    [TestClass]
    public class UnitTest
    {
        string _validJSONFile = string.Empty;
        string _invalidJSONFile = string.Empty;
        string _validXMLFile = string.Empty;
        string _invalidXMLFile = string.Empty;

        string _testFilePath = string.Empty;

        IFileHandling _ifhJson = new ArgumentJson();

        public UnitTest()
        {
            _testFilePath = Directory.GetCurrentDirectory() + @"\TestFiles\";

            _validJSONFile = _testFilePath + "JSON_ValidFormat.json";
            _invalidJSONFile = _testFilePath + "JSON_InvalidFormat.json";
            _validXMLFile = _testFilePath + "XML_ValidFormat.xml";
            _invalidXMLFile = _testFilePath + "XML_InvalidFormat.xml";
        }

        [TestMethod]
        public void AllTestFilesExist()
        {
            Assert.IsTrue(File.Exists(_validJSONFile));
            Assert.IsTrue(File.Exists(_invalidJSONFile));
            Assert.IsTrue(File.Exists(_validXMLFile));
            Assert.IsTrue(File.Exists(_invalidXMLFile));
        }

        [TestMethod]
        public void Arguments_WithValidArguments() => Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(new string[] { "-json", _validJSONFile, "-xml", _validXMLFile }, "-json")));

        [TestMethod]
        public void Arguments_WithInvalidArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(new string[] { "json", _testFilePath }, "-json")));

        [TestMethod]
        public void Arguments_WithTooManyArguments() => Assert.IsTrue(File.Exists(_ifhJson.GetFilePath(new string[] { "-json", _validJSONFile, "-test1", _testFilePath, "-test2", _testFilePath }, "-json")));

        [TestMethod]
        public void Arguments_WithMissingArgument() => Assert.IsTrue(string.IsNullOrEmpty(_ifhJson.GetFilePath(Array.Empty<string>(), "-json")));

        [TestMethod]
        public void File_WithValidJSONFormat() => Assert.IsTrue(_ifhJson.GetParsedData(new string[] { "-json", _validJSONFile }).Length > 0);

        [TestMethod]
        public void File_WithInvalidJSONFormat() => Assert.ThrowsException<JsonSerializationException>(() => _ifhJson.GetParsedData(new string[] { "-json", _invalidJSONFile }));
    }
}
