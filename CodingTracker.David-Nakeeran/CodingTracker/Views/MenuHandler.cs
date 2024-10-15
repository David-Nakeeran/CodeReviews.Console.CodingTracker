using Spectre.Console;

namespace CodingTracker.Views;

class MenuHandler
{

    internal enum MenuOption
    {
        ViewAllRecords = 1,
        InsertRecord = 2,
        DeleteRecord = 3,
        UpdateRecord = 4,
        CloseApp = 5
    }

    internal MenuOption ShowMenu()
    {
        Console.Clear();
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
        return userSelection;
    }

    internal void WaitForUserInput()
    {
        AnsiConsole.MarkupLine("Press any key to continue.....");
        Console.ReadKey(true);
    }
}