using System;
using System.Globalization;

namespace Raje.Infra.Util
{
    public static class DateHelper
    {
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            return ConvertEpochToDateTime(unixTimeStamp);
        }

        public static DateTime JavascriptTimeStampToDateTime(long javascriptTimeStamp)
        {
            return ConvertEpochToDateTime(javascriptTimeStamp / 1000);
        }

        public static long DateTimeToJavascriptTimeStamp(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return 0;
            }

            var epoch = ConvertDateTimeToEpoch(dateTime.Value);

            return epoch;
        }

        public static long DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            var epoch = ConvertDateTimeToEpoch(dateTime) / 1000;

            return epoch;
        }

        public static string DateTimeToString(DateTime dateTime)
        {
            string friendly_date = ToTitleCase(dateTime.ToString("dddd", new CultureInfo("pt-BR"))) + "\n" + dateTime.Day + " de " + ToTitleCase(dateTime.ToString("MMMM", new CultureInfo("pt-BR"))) + " de " + dateTime.Year;
            return friendly_date;
        }

        public static string ToDateIndex(this DateTime date)
        {
            if (date == null)
                return string.Empty;
            else
                return date.ToString("yyyyMMdd");
        }

        #region PRIVATE

        private static DateTime ConvertEpochToDateTime(long epoch)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(epoch).ToLocalTime();
            return dtDateTime;
        }

        private static long ConvertDateTimeToEpoch(DateTime date)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long epoch = (long)TimeSpan.FromTicks(date.AddTicks(-(dtDateTime.Ticks)).Ticks).TotalMilliseconds;

            return epoch;
        }

        private static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        #endregion PRIVATE
    }
}