using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace VBeller.Tests
{
    public class CalendarWeekTest
    {
        [Theory]
        [InlineData(2017, 01)]
        [InlineData(2016, 43)]
        [InlineData(1987, 23)]
        [InlineData(1900, 5)]
        public void ConstructorShouldReturnCorrectCalendarWeekFromYearAndWeek(int year, int week)
        {
            //Act
            var calendarWeek = new CalendarWeek(year, week);

            //Assert
            Assert.Equal(year, calendarWeek.Year);
            Assert.Equal(week, calendarWeek.Week);
        }

        [Theory]
        [InlineData(2017, 01, 01)]
        [InlineData(2016, 8, 5)]
        [InlineData(1987, 5, 5)]
        [InlineData(1900, 12, 31)]
        public void ConstructorShouldReturnCorrectCalendarWeekFromDate(int year, int month, int day)
        {
            //Arrange
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var date = new DateTime(year, month, day);
            var expectedYear = year;
            var expectedWeek = dfi.Calendar.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            //Act
            var calendarWeek = new CalendarWeek(new DateTime(year, month, day));

            //Assert
            Assert.Equal(expectedYear, calendarWeek.Year);
            Assert.Equal(expectedWeek, calendarWeek.Week);
        }

        [Theory]
        [InlineData(2017, 53)]
        [InlineData(2017, 1000)]
        [InlineData(1998, 54)]
        [InlineData(1974, 53)]
        public void ConstructorShouldThrowOutOfRangeOnWeekOutOfRange(int year, int week)
        {
            //Act
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new CalendarWeek(2017, 53));

            //Assert
            Assert.Equal("week", ex.ParamName);
        }
        
        [Theory]
        [MemberData(nameof(ValidateFirstDayData))]
        public void FirstDayShouldReturnCorrectDate(CalendarWeek calendarWeek, DateTime firstDay)
        {
            //Act
            var result = calendarWeek.FirstDay;

            //Assert 
            Assert.Equal(firstDay, result);
        }

        public static IEnumerable<object[]> ValidateFirstDayData => new[]
        {
            new object[] {new CalendarWeek(2017, 01), new DateTime(2017, 1, 2)},
            new object[] {new CalendarWeek(2017, 21), new DateTime(2017, 5, 22)},
            new object[] {new CalendarWeek(2016, 52), new DateTime(2016, 12, 26)}
        };
        
        [Theory]
        [MemberData(nameof(ValidateLastDayData))]
        public void LastDayShouldReturnCorrectDate(CalendarWeek calendarWeek, DateTime lastDay)
        {
            //Act
            var result = calendarWeek.LastDay;

            //Assert 
            Assert.Equal(lastDay, result);
        }

        public static IEnumerable<object[]> ValidateLastDayData => new[]
        {
            new object[] {new CalendarWeek(2017, 01), new DateTime(2017, 1, 8)},
            new object[] {new CalendarWeek(2017, 21), new DateTime(2017, 5, 28)},
            new object[] {new CalendarWeek(2016, 52), new DateTime(2017, 1, 1)}
        };
    }
}