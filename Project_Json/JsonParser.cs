using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Json
{
    public class JsonParser : IFileHandling
    {
        #region  ' IFileHandling  '

        public string[] GetParsedData(string fileData)
        {
            string[] parsedData = Array.Empty<string>();

            try
            {
                if (!string.IsNullOrEmpty(fileData))
                {
                    //create a list of json values
                    parsedData = JsonConvert.DeserializeObject<JObject[]>(fileData)
                        .Select(jo => jo).Properties()
                        .Select(p => !string.IsNullOrEmpty(p.Value.ToString()) ? p.Value.ToString() : null)
                        .ToArray();
                }
            }
            catch
            {
                throw new JsonSerializationException("Error with JSON file.");
            }

            return parsedData;
        }

        #endregion
    }
}
