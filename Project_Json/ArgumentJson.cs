using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Project_Json
{
    public class ArgumentJson : IFileHandling
    {
        public ArgumentJson()
        { }

        public ArgumentJson(string[] clArguments)
        {
            ValueItems = Array.Empty<string>();

            string filePath = GetFilePathFromArgument(clArguments, "-json");
            if (!string.IsNullOrEmpty(filePath))
            {
                ValueItems = GetValuesFromFile(GetFileNameFromDialog(filePath, "Select JSON File"));
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
            string[] jsonItems = Array.Empty<string>();

            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    //load json file and retrieve objects
                    var jobjects = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(fileName));

                    //create a list of json values
                    jsonItems = jobjects.Select(jo => jo).Properties().Select(p => !string.IsNullOrEmpty(p.Value.ToString()) ? p.Value.ToString() : "No Value").ToArray();
                }
            }
            catch
            {
                throw new Exception("Error with JSON file.");
            }

            return jsonItems;
        }
    }
}
