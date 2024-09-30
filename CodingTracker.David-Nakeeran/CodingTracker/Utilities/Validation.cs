using System.Globalization;
using Spectre.Console;

namespace CodingTracker.Utilities;

class Validation
{
    internal string CheckInputNullOrWhitespace(string message, string input)
    {
        while (string.IsNullOrWhiteSpace(input))
        {
            input = AnsiConsole.Ask<string>(message);
        }
        return input;
    }
    internal bool IsTimeValid(string input)
    {
        if (!DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return false;
        }
        return true;
    }
    internal string GetValidatedTimeInput(string message, InputHandler inputHandler)
    {
        var timeInput = AnsiConsole.Ask<string>(message);
        timeInput = CheckInputNullOrWhitespace("Please enter the time in the format of hh:mm or enter 0 to return to main menu", timeInput);
        if (inputHandler.IsInputZero(timeInput))
        {
            return "0";
        }
        while (!IsTimeValid(timeInput))
        {
            timeInput = AnsiConsole.Ask<string>("Please enter the time in the format of hh:mm");
        }
        return timeInput;
    }
    // IsTimeValid method return bool

}