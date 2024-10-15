using CodingTracker.Controller;
using CodingTracker.Models;
using CodingTracker.Utilities;
using System.Globalization;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Database;

internal class DatabaseManager
{
    private readonly CodingTrackerController _codingTrackerController;
    private readonly TimeCalculator _timeCalculator;
    private readonly Conversion _conversion;
    private readonly CodingSessionTracker _sessionTracker;

    public DatabaseManager(CodingTrackerController controller, TimeCalculator timeCalculator, Conversion conversion, CodingSessionTracker sessionTracker)
    {
        _codingTrackerController = controller;
        _timeCalculator = timeCalculator;
        _conversion = conversion;
        _sessionTracker = sessionTracker;
    }

    internal void CreateDatabaseTable()
    {
        string? connectionString = ConfigurationManager.AppSettings["connectionString"];

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS coding_tracker (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            StartTime TEXT,
            EndTime TEXT,
            Duration TEXT,
            Date TEXT
        )";
        tableCmd.ExecuteNonQuery();
    }

    internal List<CodingSession> LoadCodingSessionDataFromDb()
    {
        string? connectionString = ConfigurationManager.AppSettings["connectionString"];

        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string query = @"SELECT * from coding_tracker";

        var sessionsFromDb = connection.Query<CodingSession>(query).ToList();
        foreach (var session in sessionsFromDb)
        {
            Console.WriteLine(session.Date);
        }
        return sessionsFromDb;
    }

    internal void Insert()
    {
        (string? startTime, string? endTime) = _codingTrackerController.GetTimeInputs();

        if (startTime == null || endTime == null)
        {
            AnsiConsole.MarkupLine("Returning to main menu..");
            return;
        }

        string duration = _timeCalculator.CalculateFormatDuration(startTime, endTime);
        string dateOfEntry = DateTime.Today.ToString("dd/MM/yy", CultureInfo.InvariantCulture);

        string? connectionString = ConfigurationManager.AppSettings["connectionString"];
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = @"INSERT INTO coding_tracker(StartTime, EndTime, Duration, Date) VALUES(@StartTime, @EndTime, @Duration, @Date);";
        tableCmd.Parameters.AddWithValue("@StartTime", startTime);
        tableCmd.Parameters.AddWithValue("@EndTime", endTime);
        tableCmd.Parameters.AddWithValue("@Duration", duration);
        tableCmd.Parameters.AddWithValue("@Date", dateOfEntry);
        tableCmd.ExecuteNonQuery();
        connection.Close();
    }

    internal void ViewAllRecords()
    {
        var sessions = LoadCodingSessionDataFromDb();

        if (sessions.Any())
        {
            foreach (var session in sessions)
            {
                if (!_sessionTracker.CheckForExistingEntry(session.Id))
                {
                    _sessionTracker.AddSession(session);
                }
            }
            _sessionTracker.PrintCodingSessions();
        }
        else
        {
            AnsiConsole.MarkupLine("No previous records");
        }
    }

    internal void Delete()
    {
        ViewAllRecords();

        int recordId = _codingTrackerController.GetIdInput();
        if (recordId == 0)
        {
            return;
        }

        string? connectionString = ConfigurationManager.AppSettings["connectionString"];
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = "DELETE FROM coding_tracker WHERE id = @id";
        tableCmd.Parameters.AddWithValue("@id", recordId);
        var rowDeleted = tableCmd.ExecuteNonQuery();

        _sessionTracker.DeleteMatchingID(recordId);

        if (rowDeleted == 0)
        {
            AnsiConsole.MarkupLine($"Record with an ID of {recordId} does not exist");
        }
        else
        {
            AnsiConsole.MarkupLine($"Record with an ID of {recordId} was deleted successfully");
        }
        connection.Close();
    }

    internal void Update()
    {
        ViewAllRecords();

        int recordId = _codingTrackerController.GetIdInput();
        if (recordId == 0)
        {
            return;
        }

        (string? startTime, string? endTime) = _codingTrackerController.GetTimeInputs();

        if (startTime == null || endTime == null)
        {
            AnsiConsole.MarkupLine("Returning to main menu..");
            return;
        }

        string duration = _timeCalculator.CalculateFormatDuration(startTime, endTime);
        string dateOfEntry = DateTime.Today.ToString("dd/MM/yy", CultureInfo.InvariantCulture);

        string? connectionString = ConfigurationManager.AppSettings["connectionString"];
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = "UPDATE coding_tracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration, Date = @Date WHERE id = @id";
        tableCmd.Parameters.AddWithValue("@id", recordId);
        tableCmd.Parameters.AddWithValue("@StartTime", startTime);
        tableCmd.Parameters.AddWithValue("@EndTime", endTime);
        tableCmd.Parameters.AddWithValue("@Duration", duration);
        tableCmd.Parameters.AddWithValue("@Date", dateOfEntry);
        tableCmd.ExecuteNonQuery();
        connection.Close();

        DateTime start = _conversion.StringToTime(startTime);
        DateTime end = _conversion.StringToTime(endTime);
        _sessionTracker.UpdateExistingRecord(recordId, start, end, duration, dateOfEntry);
    }
}