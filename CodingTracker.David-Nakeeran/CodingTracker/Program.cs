using CodingTracker.Database;
using CodingTracker.Utilities;
namespace CodingTracker;
class Program
{
    static void Main(string[] args)
    {
        DatabaseManager databaseManager = new DatabaseManager();
        CodingSessionTracker codingSessionTracker = new CodingSessionTracker();
        databaseManager.CreateDatabaseTable();

        var sessions = databaseManager.LoadCodingSessionDataFromDb();
        foreach (var session in sessions)
        {
            codingSessionTracker.AddSession(session);
        }
    }
}
