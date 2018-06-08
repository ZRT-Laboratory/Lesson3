using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project.Json
{
    public class JsonParser : IFileHandling
    {
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

        public string[] GetParsedData(string filePath) => ParseFileData(filePath).ToArray();

        #endregion
    }
}
