using CodingTracker.Database;
using CodingTracker.Utilities;
using CodingTracker.Views;
using CodingTracker.Controller;
namespace CodingTracker;
class Program
{
    static void Main(string[] args)
    {
        Conversion conversion = new Conversion();
        TimeCalculator timeCalculator = new TimeCalculator();
        Validation validation = new Validation();
        InputHandler inputHandler = new InputHandler();
        UserInput userInput = new UserInput();
        CodingTrackerController codingTrackerController = new CodingTrackerController(validation, inputHandler, userInput);
        DatabaseManager databaseManager = new DatabaseManager(codingTrackerController, timeCalculator, conversion);
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
