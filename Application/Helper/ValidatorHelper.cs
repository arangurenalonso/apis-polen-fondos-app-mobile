namespace Application.Helper
{
    using System.Globalization;
    public class ValidatorHelper
    {
        public static bool BeAValidDate(string? date)
        {
            if (date == null)
            {
                return true;
            }

            return DateTime.TryParse(date.ToString(), out _);
        }
        public static bool BeAValidFormatDate(string? date)
        {
            if (date == null)
            {
                return true;
            }
            return DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            //return DateTime.TryParseExact(date.Value.ToString("yyyy-MM-dd"), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}
