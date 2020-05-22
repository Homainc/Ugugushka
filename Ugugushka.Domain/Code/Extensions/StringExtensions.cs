using System.Text.RegularExpressions;

namespace Ugugushka.Domain.Code.Extensions
{
    public static class StringExtensions
    {
        public static string ToUserName(this string str) =>
            Regex.Replace(str, "[a-z0-9!#$%&'*+/=?^`{|}~-]", "_");
    }
}
