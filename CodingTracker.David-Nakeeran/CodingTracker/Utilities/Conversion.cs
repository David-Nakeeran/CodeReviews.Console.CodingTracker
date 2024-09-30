namespace CodingTracker.Utilities;

class Conversion
{
    internal DateTime StringToTime(string dateTime)
    {
        return DateTime.ParseExact(dateTime, "HH:mm", null);
    }

    internal string TimeSpanToString(TimeSpan span)
    {
        return span.ToString(@"hh\:mm");
    }
}