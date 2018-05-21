using Project_Interface;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project_Xml
{
    public class ArgumentXml : IFileHandling
    {
        public ArgumentXml()
        { }

        public ArgumentXml(string[] clArguments)
        {
            ValueItems = Array.Empty<string>();

            string filePath = GetFilePathFromArgument(clArguments, "-xml");
            if (!string.IsNullOrEmpty(filePath))
            {
                ValueItems = GetValuesFromFile(GetFileNameFromDialog(filePath, "Select XML File"));
            }
        }

        public string[] ValueItems { get; }

        public string GetFilePathFromArgument(string[] clArguments, string clNameValue)
        {
            string filePath = clArguments.SkipWhile(a => string.Compare(a, clNameValue, true) != 0)
                .Skip(1)
                .DefaultIfEmpty("")
                .First()
                .ToString();

            return filePath;
        }

        public string GetFileNameFromDialog(string filePath, string title)
        {
            OpenFileDialog fd = new OpenFileDialog() { InitialDirectory = Path.GetDirectoryName(filePath), FileName = Path.GetFileName(filePath), Multiselect = false, Title = title };
            return fd.ShowDialog() == DialogResult.OK ? fd.FileName : null;
        }

        public string[] GetValuesFromFile(string fileName)
        {
            string[] xmlItems = Array.Empty<string>();

            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    //load xml document
                    XDocument doc = XDocument.Load(fileName);

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
    }
}
