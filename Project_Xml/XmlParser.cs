using Project.Interface;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Project.Xml
{
    public class XmlParser : IFileHandling
    {
        #region  ' IFileHandling  '

        public string[] GetParsedData(string fileData)
        {
            string[] parsedData = Array.Empty<string>();

            try
            {
                if (!string.IsNullOrEmpty(fileData))
                {
                    //create array of xml items
                    parsedData = XDocument.Parse(fileData)
                        .Root
                        .Elements()
                        .Select(xel => xel.Attributes("id").Any() ? xel.Attribute("id").Value : null)
                        .ToArray();
                }
            }
            catch
            {
                throw new XmlException("Error with XML file.");
            }

            return parsedData;
        }

        #endregion
    }
}
