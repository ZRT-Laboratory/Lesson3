using Project_Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Project_Xml
{
    public class ArgumentXml : IFileHandling
    {
        string[] _clArguments = null;

        public ArgumentXml(string[] clArguments)
        {
            _clArguments = clArguments;
        }

        public string[] ParseFileData(string filePath)
        {
            string[] parsedData = Array.Empty<string>();

            try
            {
                if (File.Exists(filePath))
                {
                    //load xml document
                    XDocument doc = XDocument.Load(filePath);

                    //create a list of xml values
                    parsedData = doc.Root.Elements().Select(xel => xel.Attributes("id").Any() ? xel.Attribute("id").Value : null).ToArray();
                }
            }
            catch
            {
                throw new XmlException("Error with XML file.");
            }

            return parsedData;
        }

        #region  ' IFileHandling  '

        public void DisplayData(List<string> parsedData)
        {
            //display the list and replace nulls with No Value
            parsedData.ForEach(vi => Console.WriteLine("{0}", vi ?? "No Value"));
        }

        public string GetFilePath(string clArgumentNameValue)
        {
            string filePath = _clArguments.SkipWhile(a => string.Compare(a, clArgumentNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty("")
                .First()
                .ToString();

            return filePath;
        }

        public string[] GetParsedData(string clArgumentNameValue) => ParseFileData(GetFilePath(clArgumentNameValue)).ToArray();

        #endregion
    }
}
