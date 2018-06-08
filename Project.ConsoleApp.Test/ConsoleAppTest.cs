using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Project.ConsoleApp.Test
{
    [TestClass]
    public class ConsoleAppTest
    {
        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            try
            {
                //act
                Program.Main(new string[] { "-json", string.Empty, "-xml", string.Empty });
                
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments()
        {
            //assert
            Assert.ThrowsException<ArgumentException>(() => Program.Main(new string[] { "json", string.Empty, "xml", string.Empty }));
        }

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            try
            {
                //act
                Program.Main(new string[] { "-json", string.Empty, "-xml", string.Empty, "-test", string.Empty });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingArguments()
        {
            //assert
            Assert.ThrowsException<ArgumentException>(() => Program.Main(Array.Empty<string>()));
        }
    }
}
