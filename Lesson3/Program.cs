using Project_Xml;
using System;
using System.Linq;

namespace Lesson3
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            new ArgumentXml(clArguments).ValueItems.OrderBy(vi => vi).ToList().ForEach(vi => Console.WriteLine("{0}", vi));
        }
    }
}
