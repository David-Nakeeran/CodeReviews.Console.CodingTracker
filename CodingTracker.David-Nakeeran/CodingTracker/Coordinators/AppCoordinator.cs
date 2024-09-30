using CodingTracker.Database;
using CodingTracker.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CodingTracker.Coordinators;
class AppCoordinator
{
    private readonly MenuHandler _menuHandler;
    private readonly DatabaseManager _databaseManager;


    public AppCoordinator(MenuHandler menuHandler, DatabaseManager databaseManager)
    {
        _menuHandler = menuHandler;
        _databaseManager = databaseManager;
    }

    internal void Start()
    {
        _databaseManager.CreateDatabaseTable();
        bool closeApp = false;

        while (!closeApp)
        {
            var userSelection = _menuHandler.ShowMenu();
            switch ((int)userSelection)
            {
                case 1:
                    Console.WriteLine("View all records");
                    break;
                case 2:
                    _databaseManager.Insert();
                    Console.WriteLine("Insert record");
                    break;
                case 3:
                    Console.WriteLine("Delete record");
                    break;
                case 4:
                    Console.WriteLine("Update record");
                    break;
                case 5:
                    Console.WriteLine("Close app");
                    closeApp = true;
                    break;
            }

        }

    }
}