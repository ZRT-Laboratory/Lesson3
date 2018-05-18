using System;
using System.Linq;

namespace Lesson3
{
    class Program
    {
        static void Main(string[] clArguments)
        {
            clArguments.ToList().ForEach(cl => Console.WriteLine("{0}", cl));
        }
    }
}
