using System.Text.RegularExpressions;

namespace schnittstelle.mysql.library.extensionmethods
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string s, string sValue)
        {
            return Regex.Replace(sValue, @"\s+", "");
        }

        public static string ReplaceWhiteSpace(this string s, string sValue)
        {
            return Regex.Replace(sValue, @"\s+", ":");
        }
    }
}
