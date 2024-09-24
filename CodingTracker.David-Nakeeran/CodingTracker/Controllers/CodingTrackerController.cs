using CodingTracker.Utilities;
using Spectre.Console;

namespace CodingTracker.Controller;

class CodingTrackerController
{
    private readonly Validation _validation;
    // Ask user to insert start time in specific format
    // Ask user to insert end time in a specific format
    // GetTimeInput method return string
    internal CodingTrackerController(Validation validation)
    {
        _validation = validation;
    }
    internal string GetTimeInput()
    {
        var timeInput = AnsiConsole.Ask<string>("Please enter the time in the format of hh:mm or enter 0 to return to main menu");
        timeInput = _validation.CheckInputNullOrWhitespace("Please enter the time in the format of hh:mm or enter 0 to return to main menu", timeInput);
        while (!_validation.IsTimeValid(timeInput))
        {
            timeInput = AnsiConsole.Ask<string>("Please enter the time in the format of hh:mm or enter 0 to return to main menu");
        }
        return timeInput;
    }

    // IsTimeValid method return bool
    // CheckInputNullOrWhitespace method return string
}