using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Project.ConsoleApp.Test
{
    [TestClass]
    public class ConsoleAppTest
    {
        [TestMethod]
        public void Arguments_WithValidArguments()
        {
            //arrange
            string file = CreateTempFile(Array.Empty<string>());

            try
            {
                //act
                Program.Main(new string[] { "-json", file, "-xml", file });
                
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Arguments_WithInvalidArguments() => Assert.ThrowsException<ArgumentException>(() => Program.Main(new string[] { "json", string.Empty, "xml", string.Empty }));

        [TestMethod]
        public void Arguments_WithTooManyArguments()
        {
            //arrange
            string file = CreateTempFile(Array.Empty<string>());

            try
            {
                //act
                Program.Main(new string[] { "-json", file, "-xml", file, "-test", file });
            }
            catch (ArgumentException aex)
            {
                //assert
                Assert.Fail(aex.Message);
            }
            finally
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Arguments_WithMissingArguments() => Assert.ThrowsException<ArgumentException>(() => Program.Main(Array.Empty<string>()));

        #region  " Non Test Methods "

        /// <summary>
        /// CreateTempFile
        /// </summary>
        /// <param name="fileData">array containing data you want to write to the file. pass an empty array if no data needed</param>
        /// <returns>filepath of new temp file</returns>
        string CreateTempFile(string[] fileData)
        {
            string file = Path.GetTempFileName();

            if (fileData.Length > 0)
            {
                File.WriteAllLines(file, fileData);
            }

            return file;
        }

        #endregion
    }
}
