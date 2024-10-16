namespace CodingTracker.Models;

class CodingSession
{
    internal int Id { get; set; }
    internal DateTime StartTime { get; set; }
    internal DateTime EndTime { get; set; }
    internal string? Duration { get; set; }
    internal string? Date { get; set; }
}