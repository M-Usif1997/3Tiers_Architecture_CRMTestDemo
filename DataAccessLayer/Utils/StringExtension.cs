using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public static class StringExtension
    {
        public static string GetGuidId(this string inputString)
        {
            int firstIndex = inputString.IndexOf("(");
            return inputString.Substring(firstIndex + 1, 36);
        }
    }
}
