using System.Configuration;
using CodingTracker.Controller;
using CodingTracker.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Database;
internal class DatabaseManager
{
    private readonly CodingTrackerController _controller;
    internal DatabaseManager(CodingTrackerController controller)
    {
        _controller = controller;
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
        // string duration = endTime - startTime;
        string dateOfEntry = DateTime.Today.ToString();
        // DateTime.Today then .ToString() - get the current date
        // connect to database
        // insert variables into database
    }
}