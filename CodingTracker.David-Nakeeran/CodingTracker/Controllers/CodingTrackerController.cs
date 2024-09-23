using Spectre.Console;

namespace CodingTracker.Controller;

class CodingTrackerController
{
    // Ask user to insert start time in specific format
    // Ask user to insert end time in a specific format
    // GetTimeInput method return string
    internal string GetTimeInput()
    {
        var timeInput = AnsiConsole.Ask<string>("Please enter the time in the format of hh:mm or enter 0 to return to main menu");
        // CheckInputNullOrWhitespace method return string
        // IsTimeValid method return bool
        return timeInput;
    }

    // IsTimeValid method return bool
    // CheckInputNullOrWhitespace method return string
}