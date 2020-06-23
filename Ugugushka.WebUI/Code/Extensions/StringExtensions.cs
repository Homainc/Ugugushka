using System.Text.RegularExpressions;

namespace Ugugushka.WebUI.Code.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceSpecSymbols(this string str)
            => Regex.Replace(str, "[$#@\"'._+=*&%^;:()!?-]", " ").Trim();
    }
}
