using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Interface;
using System.Linq;
using System.Xml;

namespace Project.Xml.Test
{
    [TestClass]
    public class XmlTest
    {
        [TestMethod]
        public void File_WithValidXMLFormat()
        {
            //arrange
            IFileHandling ifhXml = new XmlParser();

            //act
            string[] parsedData = ifhXml.GetParsedData("<xml>,<orange id = 'Round Orange'/>,<orange id = 'Naval Orange'/>,</xml>");

            //assert            
            Assert.IsTrue(parsedData.Any(pd => pd == "Round Orange"));
            Assert.IsTrue(parsedData.Any(pd => pd == "Naval Orange"));
        }

        [TestMethod]
        public void File_WithInvalidXMLFormat()
        {
            //arrange
            IFileHandling ifh = new XmlParser();

            //assert
            Assert.ThrowsException<XmlException>(() => ifh.GetParsedData("<xml>, <orange id = 'Round Orange' >, </xml>"));
        }
    }
}
