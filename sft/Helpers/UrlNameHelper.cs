using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace sft.Helpers
{
  public static class UrlNameHelper
  {
    public static string ConvertToUrlSafeString(this string value)
    {
      value = value.Replace("=", "-is-");
      value = value.Replace("&", "-en-");
      value = value.Replace("+", "-plus-");
      value = value.Replace("%", "-procent-");
      value = new string(Enumerable.ToArray<char>(Enumerable.Select<char, char>((IEnumerable<char>)value.Normalize(NormalizationForm.FormD), (Func<char, char>)(character =>
      {
        switch (CharUnicodeInfo.GetUnicodeCategory(character))
        {
          case UnicodeCategory.UppercaseLetter:
            return char.ToLower(character);
          case UnicodeCategory.LowercaseLetter:
          case UnicodeCategory.DecimalDigitNumber:
            return character;
          default:
            return '-';
        }
      }))));
      value = Regex.Replace(value, "\\-+", "-").Trim('-');
      return value;
    }
  }
}