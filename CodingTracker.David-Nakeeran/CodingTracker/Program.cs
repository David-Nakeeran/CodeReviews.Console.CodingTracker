using CodingTracker.Database;
using CodingTracker.Utilities;
using CodingTracker.Views;
using CodingTracker.Controller;
namespace CodingTracker;
class Program
{
    static void Main(string[] args)
    {
        Validation validation = new Validation();
        CodingTrackerController codingTrackerController = new CodingTrackerController(validation);
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
