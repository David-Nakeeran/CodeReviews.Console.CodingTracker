using System.Globalization;
using Spectre.Console;

namespace CodingTracker.Utilities;

class Conversion
{
    internal DateTime StringToTime(string dateTime)
    {
        return DateTime.ParseExact(dateTime, "HH:mm", null);
    }

    internal DateTime StringToDate(string dateTime)
    {
        return DateTime.ParseExact(dateTime, "dd/MM/yy", CultureInfo.InvariantCulture);
    }

    internal string TimeSpanToString(TimeSpan span)
    {
        return span.ToString(@"hh\:mm");
    }

    internal int ParseInt(string? input, string message)
    {
        int cleanNum;
        while (!int.TryParse(input, out cleanNum))
        {
            input = AnsiConsole.Ask<string>(message);
        }
        return cleanNum;
    }
}