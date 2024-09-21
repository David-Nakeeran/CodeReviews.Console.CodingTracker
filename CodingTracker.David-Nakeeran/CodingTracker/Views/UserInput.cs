using Spectre.Console;
namespace CodingTracker.Views;
class UserInput
{
    enum MenuOption
    {
        ViewAllRecords = 1,
        InsertRecord = 2,
        DeleteRecord = 3,
        UpdateRecord = 4,
        CloseApp = 5
    }
    internal void ShowMenu()
    {
        Console.Clear();
        bool closeApp = false;
        while (!closeApp)
        {
            AnsiConsole.MarkupLine("[bold]Main Menu[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[green](Use arrow keys to navigate, then press enter)[/]");
            AnsiConsole.WriteLine();
            var userSelection = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                    .Title("Select an option:")
                    .AddChoices(
                        MenuOption.ViewAllRecords,
                        MenuOption.InsertRecord,
                        MenuOption.DeleteRecord,
                        MenuOption.UpdateRecord,
                        MenuOption.CloseApp
                    ));
            switch ((int)userSelection)
            {
                case 1:
                    Console.WriteLine("View all records");
                    break;
                case 2:
                    // Insert record to be next worked on
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