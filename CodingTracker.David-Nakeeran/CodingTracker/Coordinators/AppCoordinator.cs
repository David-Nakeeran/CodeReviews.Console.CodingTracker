using CodingTracker.Database;
using CodingTracker.Views;

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
                    _databaseManager.ViewAllRecords();
                    _menuHandler.WaitForUserInput();
                    break;
                case 2:
                    _databaseManager.Insert();
                    break;
                case 3:
                    _databaseManager.Delete();
                    break;
                case 4:
                    _databaseManager.Update();
                    break;
                case 5:
                    closeApp = true;
                    break;
            }
        }
    }
}