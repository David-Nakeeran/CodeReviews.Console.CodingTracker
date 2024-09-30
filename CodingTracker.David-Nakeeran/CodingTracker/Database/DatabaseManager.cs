using System.Configuration;
using CodingTracker.Controller;
using CodingTracker.Models;
using CodingTracker.Utilities;
using System.Globalization;
using CodingTracker.Views;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.Database;
internal class DatabaseManager
{
    private readonly CodingTrackerController _codingTrackerController;
    private readonly TimeCalculator _timeCalculator;

    public DatabaseManager(CodingTrackerController controller, TimeCalculator timeCalculator, Conversion conversion)
    {
        _codingTrackerController = controller;
        _timeCalculator = timeCalculator;
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
            session.Date = DateTime.ParseExact(session.Date.ToString(), "dd-MM-yyyy", new CultureInfo("en-GB"));
        }
        return sessionsFromDb;
    }
    internal void Insert()
    {
        (string startTime, string endTime) = _codingTrackerController.GetTimeInputs();
        if (startTime == null || endTime == null)
        {
            AnsiConsole.MarkupLine("Returning to main menu..");
            return;
        }
        string duration = _timeCalculator.CalculateFormatDuration(startTime, endTime);
        string dateOfEntry = DateTime.Today.ToString("dd/MM/yyyy");

        // connect to database
        string connectionString = ConfigurationManager.AppSettings["connectionString"];
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
}