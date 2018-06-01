using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Json
{
    public class ArgumentJson : IFileHandling
    {
        string[] _clArguments = null;

        public ArgumentJson(string[] clArguments)
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
                    //load json file and retrieve objects
                    var jobjects = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(filePath));

                    //create a list of json values
                    parsedData = jobjects.Select(jo => jo).Properties().Select(p => !string.IsNullOrEmpty(p.Value.ToString()) ? p.Value.ToString() : null).ToArray();
                }
            }
            catch
            {
                throw new JsonSerializationException("Error with JSON file.");
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

        public string[] GetParsedData(string clArgumentNameValues) => ParseFileData(GetFilePath(clArgumentNameValues)).ToArray();

        #endregion
    }
}
