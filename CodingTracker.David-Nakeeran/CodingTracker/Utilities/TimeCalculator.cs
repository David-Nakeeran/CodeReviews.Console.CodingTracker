namespace CodingTracker.Utilities;

class TimeCalculator
{
    internal TimeSpan CalculateDuration(DateTime startTime, DateTime endTime)
    {
        return endTime - startTime;
    }
}