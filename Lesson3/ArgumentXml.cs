using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Project_Console
{
    public class ArgumentXml : IFileHandling
    {
        #region  ' IFileHandling  '

        public string[] GetFileData(string filePath)
        {
            string[] xmlItems = Array.Empty<string>();

            try
            {
                if (File.Exists(filePath))
                {
                    //load xml document
                    XDocument doc = XDocument.Load(filePath);

                    //create a list of xml values
                    xmlItems = doc.Root.Elements().Select(xel => xel.Attributes("id").Any() ? xel.Attribute("id").Value : "No Value").ToArray();
                }
            }
            catch
            {
                throw new Exception("Error with XML file.");
            }

            return xmlItems;
        }

        public string GetFilePath(string[] clArguments, string clNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, clNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty("")
                .First()
                .ToString();

            return filePath;
        }

        public string[] GetParsedData(string[] clArguments)
        {
            return GetFileData(GetFilePath(clArguments, "-xml")).ToArray();
        }

        #endregion
    }
}
