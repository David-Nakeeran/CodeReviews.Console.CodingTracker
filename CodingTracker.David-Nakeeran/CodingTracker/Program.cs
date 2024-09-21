using CodingTracker.Database;
using CodingTracker.Utilities;
using CodingTracker.Views;
namespace CodingTracker;
class Program
{
    static void Main(string[] args)
    {
        DatabaseManager databaseManager = new DatabaseManager();
        CodingSessionTracker codingSessionTracker = new CodingSessionTracker();
        UserInput userInput = new UserInput();
        databaseManager.CreateDatabaseTable();

        userInput.ShowMenu();

        var sessions = databaseManager.LoadCodingSessionDataFromDb();
        foreach (var session in sessions)
        {
            codingSessionTracker.AddSession(session);
        }
    }
}
