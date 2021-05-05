using System.Globalization;

namespace SC.API.CleanArchitecture.Application.Common
{
    public static class DecimalExtensions
    {
        public static string ToLocaleString(this decimal value)
        {
            return value.ToString(CultureInfo.CreateSpecificCulture("hr-HR"));
        }
    }
}
