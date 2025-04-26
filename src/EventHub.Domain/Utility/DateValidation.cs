using System;
using System.Globalization;
using EventHub.Exceptions;

namespace EventHub.Utility;

public static class DateValidation
{
    public static void IsValidTime(DateTime date1, DateTime date2)
    {
        if (date1.CompareTo(date2) > 0)
        {
            throw new EndTimeEarlierThanStartTimeException(date2.ToString(CultureInfo.InvariantCulture));
        }
    }
}