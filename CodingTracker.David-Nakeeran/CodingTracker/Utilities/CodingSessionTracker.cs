using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Utilities;

class CodingSessionTracker
{
    internal List<CodingSession> CodingSessions = new List<CodingSession>();

    internal void AddSession(CodingSession session)
    {
        CodingSessions.Add(session);
    }

    internal bool CheckForExistingEntry(int id)
    {
        if (CodingSessions.Any(session => session.Id == id))
        {
            return true;
        }
        return false;
    }

    internal void UpdateExistingRecord(int id, DateTime startTime, DateTime endTime, string duration, string date)
    {
        var recordToUpdate = CodingSessions.Find(session => session.Id == id);
        if (recordToUpdate != null)
        {
            recordToUpdate.StartTime = startTime;
            recordToUpdate.EndTime = endTime;
            recordToUpdate.Duration = duration;
            recordToUpdate.Date = date;
        }
    }

    internal void PrintCodingSessions()
    {
        foreach (var session in CodingSessions)
        {
            AnsiConsole.MarkupLine("----------------------------------------------------------");
            AnsiConsole.MarkupLine($"ID: {session.Id} - Start time: {session.StartTime.ToString("HH:mm")} | End Time: {session.EndTime.ToString("HH:mm")} | Duration: {session.Duration} | Date: {session.Date}");
            AnsiConsole.MarkupLine("----------------------------------------------------------");
            AnsiConsole.MarkupLine("");
        }
    }

    internal void DeleteMatchingID(int id)
    {
        CodingSessions.RemoveAll(session => session.Id == id);
    }
}