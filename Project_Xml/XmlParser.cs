using Project.Interface;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Project.Xml
{
    public class XmlParser : IFileHandling
    {
        string[] _clArguments = null;

        public XmlParser(string[] clArguments)
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

        public string[] GetParsedData(string filePath) => ParseFileData(filePath).ToArray();

        #endregion
    }
}
