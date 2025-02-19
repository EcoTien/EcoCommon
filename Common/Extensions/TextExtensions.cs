using System.Globalization;

namespace EcoMine.Common.Extensions
{
    internal static class TextExtensions
    {
        public static string FormatNumber(this int number)
        {
            NumberFormatInfo numberFormat = new NumberFormatInfo();
            numberFormat.NumberGroupSeparator = ".";
            return number.ToString("N0", numberFormat);
        }
        
        public static string FormatNumber(this float number)
        {
            NumberFormatInfo numberFormat = new NumberFormatInfo();
            numberFormat.NumberGroupSeparator = ".";
            return number.ToString("N0", numberFormat);
        }
    }
}