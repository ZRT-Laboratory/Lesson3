using System.Collections.Generic;
using System.Linq;

namespace Project.ConsoleApp
{
    public static class ConsoleAppExtensions
    {
        /// <summary>
        /// SortNullValuesToBottom
        /// </summary>
        /// <param name="arrayItems"></param>
        /// <returns>sorted collection with nulls at the bottom of the collection</returns>
        public static IEnumerable<string> SortNullValuesToBottom(this IEnumerable<string> arrayItems)
        {
            return arrayItems.OrderBy(fd => fd == null).ThenBy(fd => fd);            
        }

        /// <summary>
        /// ReplaceNullsWithStringValue
        /// </summary>
        /// <param name="listItems"></param>
        /// <param name="stringValue"></param>
        /// <returns>collection with all nulls replaced with parameter</returns>
        public static IEnumerable<string> ReplaceNullsWithStringValue(this IEnumerable<string> listItems, string stringValue)
        {
            return listItems.Select(li => li ?? stringValue);
        }

    }
}
