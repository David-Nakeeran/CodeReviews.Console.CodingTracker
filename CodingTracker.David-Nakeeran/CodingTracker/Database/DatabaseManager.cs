using System.Configuration;
using CodingTracker.Controller;
using CodingTracker.Models;
using CodingTracker.Utilities;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Database;
internal class DatabaseManager
{
    private readonly CodingTrackerController _controller;
    private readonly TimeCalculator _timeCalculator;
    private readonly Conversion _conversion;
    internal DatabaseManager(CodingTrackerController controller, TimeCalculator timeCalculator, Conversion conversion)
    {
        _controller = controller;
        _timeCalculator = timeCalculator;
        _conversion = conversion;
    }
    internal void CreateDatabaseTable()
    {
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        // Check connectionString is NullOrWhitespace

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
        string connectionString = ConfigurationManager.AppSettings["connectionString"];

        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string query = @"SELECT * from coding_tracker";

        var sessionsFromDb = connection.Query<CodingSession>(query).ToList();

        foreach (var session in sessionsFromDb)
        {
            session.StartTime = DateTime.ParseExact(session.StartTime.ToString(), "HH:mm", null);
            session.EndTime = DateTime.ParseExact(session.EndTime.ToString(), "HH:mm", null);
            session.Duration = session.EndTime - session.StartTime;
            session.Date = DateTime.ParseExact(session.Date.ToString(), "dd-MM-yy", new CultureInfo("en-GB"));
        }
        return sessionsFromDb;
    }
    internal void Insert()
    {
        AnsiConsole.MarkupLine("Please enter the start time");
        string startTime = _controller.GetTimeInput();
        AnsiConsole.MarkupLine("Please enter the end time");
        string endTime = _controller.GetTimeInput();
        // Conversion method - converts startTime and endTime from string to DateTime
        DateTime timeOne = _conversion.StringToTime(startTime);
        DateTime timeTwo = _conversion.StringToTime(endTime);
        // Pass to CalculateDuration
        // Convert to string, recieves TimeSpan
        TimeSpan durationConvertToString = _timeCalculator.CalculateDuration(timeOne, timeTwo);
        // convert duration to string with TimeSpanToString method
        string duration = _conversion.TimeSpanToString(durationConvertToString);
        string dateOfEntry = DateTime.Today.ToString();
        // DateTime.Today then .ToString() - get the current date
        // connect to database
        // insert variables into database
    }
}