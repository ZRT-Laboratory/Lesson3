using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Json
{
    public class ArgumentJson : IFileHandling
    {
        #region  ' IFileHandling  '

        public string[] ParseFileData(string filePath)
        {
            string[] jsonItems = Array.Empty<string>();

            try
            {
                if (File.Exists(filePath))
                {
                    //load json file and retrieve objects
                    var jobjects = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(filePath));

                    //create a list of json values
                    jsonItems = jobjects.Select(jo => jo).Properties().Select(p => !string.IsNullOrEmpty(p.Value.ToString()) ? p.Value.ToString() : null).ToArray();
                }
            }
            catch
            {
                throw new JsonSerializationException("Error with JSON file.");
            }

            return jsonItems;
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

        public string[] GetParsedData(string[] clArguments) => ParseFileData(GetFilePath(clArguments, "-json")).ToArray();

        #endregion
    }
}
