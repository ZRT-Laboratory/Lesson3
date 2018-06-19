using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Project.Interface;
using System.Linq;

namespace Project.Json.Test
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void File_WithValidJSONFormat()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //act
            string[] parsedData = ifhJson.GetParsedData("[{'Name':'Red Apple'},{ 'Name':'Green Apple'}]");

            //assert
            Assert.IsTrue(parsedData.Any(pd => pd == "Red Apple"));
            Assert.IsTrue(parsedData.Any(pd => pd == "Green Apple"));
        }

        [TestMethod]
        public void File_WithInvalidJSONFormat()
        {
            //arrange
            IFileHandling ifhJson = new JsonParser();

            //assert
            Assert.ThrowsException<JsonSerializationException>(() => ifhJson.GetParsedData("[{'Name':Red Apple},{'Name':Green Apple}]"));
        }
    }
}
