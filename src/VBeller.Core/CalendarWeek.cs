using System;
using System.Globalization;
using VBeller.Extensions;

namespace VBeller
{
    /// <summary>
    /// Represents a calendar week in a specific year.
    /// </summary>
    public struct CalendarWeek : IComparable<CalendarWeek>
    {
        /// <summary>
        /// Stores the <see cref="DateTimeFormatInfo"/> used for this <see cref="CalendarWeek"/>.
        /// </summary>
        private readonly DateTimeFormatInfo _dfi;

        /// <summary>
        /// Gets the Year of the <see cref="CalendarWeek"/>.
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Gets the Week of the <see cref="CalendarWeek"/>.
        /// </summary>
        public int Week { get; }

        /// <summary>
        /// Creates a new <see cref="CalendarWeek"/> from the given <see cref="DateTime"/>.
        /// Uses the <see cref="DateTimeFormatInfo.CurrentInfo"/> for calculation.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to use.</param>
        public CalendarWeek(DateTime date) : this(date, DateTimeFormatInfo.CurrentInfo)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalendarWeek"/> from the given <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to use.</param>
        /// <param name="info">The <see cref="DateTimeFormatInfo"/> to use for calculation.</param>
        public CalendarWeek(DateTime date, DateTimeFormatInfo info)
        {
            _dfi = info ?? throw new ArgumentNullException(nameof(info));

            Year = date.Year;
            Week = _dfi.Calendar.GetWeekOfYear(date, _dfi.CalendarWeekRule, _dfi.FirstDayOfWeek);
        }

        /// <summary>
        /// Creates a new <see cref="CalendarWeek"/> from the given year and week.
        /// Uses the <see cref="DateTimeFormatInfo.CurrentInfo"/> for calculation.
        /// </summary>
        /// <param name="year">The year of the <see cref="CalendarWeek"/></param>
        /// <param name="week">The month of the <see cref="CalendarWeek"/></param>
        public CalendarWeek(int year, int week) : this(year, week, DateTimeFormatInfo.CurrentInfo)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalendarWeek"/> from the given year and week.
        /// </summary>
        /// <param name="year">The year of the <see cref="CalendarWeek"/></param>
        /// <param name="week">The month of the <see cref="CalendarWeek"/></param>
        /// <param name="info">The <see cref="DateTimeFormatInfo"/> to use for calculation.</param>
        public CalendarWeek(int year, int week, DateTimeFormatInfo info)
        {
            _dfi = info ?? throw new ArgumentNullException(nameof(info));

            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
                throw new ArgumentOutOfRangeException(nameof(year));

            if (week < 1 || week >
                _dfi.Calendar.GetWeekOfYear(new DateTime(year, 12, 31), _dfi.CalendarWeekRule, _dfi.FirstDayOfWeek))
                throw new ArgumentOutOfRangeException(nameof(week));

            Week = week;
            Year = year;
        }

        /// <summary>
        /// Gets the first day of the <see cref="CalendarWeek"/>.
        /// </summary>
        public DateTime FirstDay
        {
            get
            {
                var firstDay = _dfi.FirstDayOfWeek.GetFirstInYear(Year);
                var firstWeek = _dfi.Calendar.GetWeekOfYear(firstDay, _dfi.CalendarWeekRule, _dfi.FirstDayOfWeek);

                var weekNum = Week;
                if (firstWeek <= 1)
                    weekNum -= 1;
                var result = firstDay.AddDays(weekNum * 7);
                return result;
            }
        }

        /// <summary>
        /// Gets the last day of the <see cref="CalendarWeek"/>.
        /// </summary>
        public DateTime LastDay
        {
            get
            {
                var firstDay = _dfi.FirstDayOfWeek.GetFirstInYear(Year);
                var firstWeek = _dfi.Calendar.GetWeekOfYear(firstDay, _dfi.CalendarWeekRule, _dfi.FirstDayOfWeek);

                var weekNum = Week;
                if (firstWeek <= 1)
                    weekNum -= 1;
                var result = firstDay.AddDays(weekNum * 7 + 6);
                return result;
            }
        }

        /// <summary>
        /// Adds an amount of weeks to a <see cref="CalendarWeek"/>.
        /// Accepts negative values and handles year-breaks.
        /// </summary>
        /// <param name="weeks"></param>
        /// <returns></returns>
        public CalendarWeek AddWeeks(int weeks)
        {
            return new CalendarWeek(FirstDay.AddDays(weeks * 7), _dfi);
        }

        /// <summary>
        /// Checks if a <see cref="DateTime"/> is in the <see cref="CalendarWeek"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> to check.</param>
        /// <returns>True, if the Date is or is between <see cref="FirstDay"/> and <see cref="LastDay"/>.</returns>
        public bool IsInWeek(DateTime date)
        {
            return date >= FirstDay && date <= LastDay;
        }
        
        public static CalendarWeek operator +(CalendarWeek week, TimeSpan span)
        {
            return new CalendarWeek(week.FirstDay + span, week._dfi);
        }

        public static CalendarWeek operator -(CalendarWeek week, TimeSpan span)
        {
            return new CalendarWeek(week.FirstDay - span, week._dfi);
        }

        /// <summary>
        /// Compares a <see cref="CalendarWeek"/> to another <see cref="CalendarWeek"/> values based.
        /// </summary>
        /// <param name="other">The <see cref="CalendarWeek"/> to compare to.</param>
        /// <returns>-1 if <paramref name="other"/> is grater than this <see cref="CalendarWeek"/>.</returns>
        public int CompareTo(CalendarWeek other)
        {
            var yearComparison = Year.CompareTo(other.Year);
            if (yearComparison != 0) return yearComparison;
            return Week.CompareTo(other.Week);
        }

        /// <summary>
        /// Compares a <see cref="CalendarWeek"/> to this <see cref="CalendarWeek"/>.
        /// </summary>
        /// <param name="other">The <see cref="CalendarWeek"/> to compare to.</param>
        /// <returns>True, if both <see cref="CalendarWeek"/> have the same values.</returns>
        public bool Equals(CalendarWeek other)
        {
            return Equals(_dfi, other._dfi) && Year == other.Year && Week == other.Week;
        }

        /// <summary>
        /// Compares a <see cref="object"/> to this <see cref="CalendarWeek"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare to.</param>
        /// <returns>True, if the <paramref name="obj"/> point to the same object or have the same values.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is CalendarWeek && Equals((CalendarWeek)obj);
        }

        /// <summary>
        /// Calculates the hash code of this <see cref="CalendarWeek"/>.
        /// Includes the <see cref="DateTimeFormatInfo"/>, <see cref="Year"/> and <see cref="Week"/> of this.
        /// </summary>
        /// <returns>The unique hash code of this <see cref="CalendarWeek"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_dfi != null ? _dfi.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                hashCode = (hashCode * 397) ^ Week;
                return hashCode;
            }
        }
    }
}