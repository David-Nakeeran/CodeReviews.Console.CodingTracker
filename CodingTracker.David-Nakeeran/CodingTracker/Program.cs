using CodingTracker.Database;
namespace CodingTracker;
class Program
{
    static void Main(string[] args)
    {
        DatabaseManager databaseManager = new DatabaseManager();
        databaseManager.CreateDatabaseTable();
    }
}
