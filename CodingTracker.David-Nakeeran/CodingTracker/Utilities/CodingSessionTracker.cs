using CodingTracker.Models;
namespace CodingTracker.Utilities;
class CodingSessionTracker
{
    internal List<CodingSession> CodingSessions { get; set; } = new List<CodingSession>();

    internal void AddSession(CodingSession session)
    {
        CodingSessions.Add(session);
    }
}