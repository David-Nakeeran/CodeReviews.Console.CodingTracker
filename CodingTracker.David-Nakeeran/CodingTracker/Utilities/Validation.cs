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
    // IsTimeValid method return bool

}