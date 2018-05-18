using Project_Json;
using System;
using System.Linq;

namespace Lesson3
{
    class Program
    {
        [STAThread]
        static void Main(string[] clArguments)
        {
            new ArgumentJson(clArguments).ValueItems.OrderBy(vi => vi).ToList().ForEach(vi => Console.WriteLine("{0}", vi));
        }
    }
}
