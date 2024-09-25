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
        InputHandler inputHandler = new InputHandler();
        UserInput userInput = new UserInput();
        CodingTrackerController codingTrackerController = new CodingTrackerController(validation, inputHandler, userInput);
        DatabaseManager databaseManager = new DatabaseManager(codingTrackerController);
        CodingSessionTracker codingSessionTracker = new CodingSessionTracker();

        databaseManager.CreateDatabaseTable();

        userInput.ShowMenu();

        var sessions = databaseManager.LoadCodingSessionDataFromDb();
        foreach (var session in sessions)
        {
            codingSessionTracker.AddSession(session);
        }
    }
}
