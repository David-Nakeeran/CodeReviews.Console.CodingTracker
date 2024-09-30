using CodingTracker.Models;
namespace CodingTracker.Utilities;
class CodingSessionTracker
{
    internal List<CodingSession> CodingSessions = new List<CodingSession>();

    internal void AddSession(CodingSession session)
    {
        CodingSessions.Add(session);
    }
}