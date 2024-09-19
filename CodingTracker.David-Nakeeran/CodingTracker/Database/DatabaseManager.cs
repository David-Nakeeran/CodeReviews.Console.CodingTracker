using System.Configuration;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Database;
internal class DatabaseManager
{
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
            Duration TEXT
        )";
        tableCmd.ExecuteNonQuery();
    }
}