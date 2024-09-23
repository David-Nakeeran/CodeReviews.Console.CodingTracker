namespace CodingTracker.Models;

class CodingSession
{
    internal int Id { get; set; }
    internal DateTime StartTime { get; set; }
    internal DateTime EndTime { get; set; }
    internal TimeSpan Duration { get; set; }
    internal DateTime Date { get; set; }
}