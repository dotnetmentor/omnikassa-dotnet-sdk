using System;
using System.Globalization;
using OmniKassa.Exceptions;

namespace OmniKassa.Utils
{
    /// <summary>
    /// Utility class for conversions between DateTime and string
    /// </summary>
    public class DateTimeUtils
    {
        private static readonly String DATE_TIME_FORMATTER = "yyyy-MM-dd'T'HH:mm:ss.fffzzz";

        private DateTimeUtils()
        {

        }

        /// <summary>
        /// Converts a DateTime to string
        /// </summary>
        /// <param name="dateTime">DateTime value</param>
        /// <returns>DateTime string value</returns>
        public static String DateToString(DateTime dateTime)
        {
            return dateTime.ToString(DATE_TIME_FORMATTER);
        }

        /// <summary>
        /// Converts a string to DateTime
        /// </summary>
        /// <param name="date">DateTime string value</param>
        /// <returns>DateTime</returns>
        public static DateTime StringToDate(String date)
        {
            if (date == null)
            {
                return DateTime.MinValue;
            }

            try
            {
                DateTime parsedDate;

                // if date string is UTC
                if (date.EndsWith("Z"))
                {
                    parsedDate = DateTime.Parse(date, CultureInfo.InvariantCulture);
                }
                else
                {
                    parsedDate = DateTime.ParseExact(date, DATE_TIME_FORMATTER, CultureInfo.InvariantCulture);
                }

                return parsedDate;
            }
            catch (Exception)
            {
                throw new RabobankSdkException("Could not convert date string to DateTime");
            }
        }
    }
}
