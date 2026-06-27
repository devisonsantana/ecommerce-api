using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Ecommerce.Domain.Common;

public static class SlugHelper
{
    public static string Generate(string phrase)
    {
        var str = string.Concat(
            phrase.Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            .Normalize(NormalizationForm.FormC)
            .ToLowerInvariant();

        str = Regex.Replace(str, @"[^a-z0-9\s]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = Regex.Replace(str, @"\s", "-");

        return str;
    }
}