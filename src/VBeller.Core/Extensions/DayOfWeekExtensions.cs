using System;

namespace VBeller.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="DayOfWeek"/> enumeration.
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// Gets the first occurance of a <see cref="DayOfWeek"/> in a year.
        /// </summary>
        /// <param name="dayOfWeek">The <see cref="DayOfWeek"/> to find.</param>
        /// <param name="year">The year in where to find the <see cref="DayOfWeek"/>.</param>
        /// <returns>The first <see cref="DayOfWeek"/> in the given year.</returns>
        public static DateTime GetFirstInYear(this DayOfWeek dayOfWeek, int year)
        {
            var jan1 = new DateTime(year, 1, 1);

            var daysOffset = dayOfWeek - jan1.DayOfWeek;
            var firstDay = jan1.AddDays(daysOffset);
            return firstDay;
        }
    }
}