namespace CodingTracker.Utilities;

class TimeCalculator
{
    private readonly Conversion _conversion;
    public TimeCalculator(Conversion conversion)
    {
        _conversion = conversion;
    }
    internal TimeSpan CalculateDuration(DateTime startTime, DateTime endTime)
    {
        return endTime - startTime;
    }
    internal string CalculateFormatDuration(string startTime, string endTime)
    {
        DateTime timeOne = _conversion.StringToTime(startTime);
        DateTime timeTwo = _conversion.StringToTime(endTime);

        TimeSpan durationConvertToString = CalculateDuration(timeOne, timeTwo);
        return _conversion.TimeSpanToString(durationConvertToString);
    }
    internal bool IsEndTimeGreater(string startTime, string endTime)
    {
        DateTime timeOne = _conversion.StringToTime(startTime);
        DateTime timeTwo = _conversion.StringToTime(endTime);

        if (timeTwo > timeOne)
        {
            return true;
        }
        return false;

    }
}