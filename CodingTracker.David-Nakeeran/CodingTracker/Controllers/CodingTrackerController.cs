using CodingTracker.Utilities;
using CodingTracker.Views;
using Spectre.Console;

namespace CodingTracker.Controller;

class CodingTrackerController
{
    private readonly Validation _validation;
    private readonly InputHandler _inputHandler;
    private readonly UserInput _userInput;
    internal CodingTrackerController(Validation validation, InputHandler inputHandler, UserInput userInput)
    {
        _validation = validation;
        _inputHandler = inputHandler;
        _userInput = userInput;
    }
    internal string GetTimeInput()
    {
        var timeInput = AnsiConsole.Ask<string>("Please enter the time in the format of hh:mm or enter 0 to return to main menu");
        timeInput = _validation.CheckInputNullOrWhitespace("Please enter the time in the format of hh:mm or enter 0 to return to main menu", timeInput);
        // _inputhandler.IsInputZero(string input)
        while (!_validation.IsTimeValid(timeInput))
        {
            timeInput = AnsiConsole.Ask<string>("Please enter the time in the format of hh:mm");
        }
        return timeInput;
    }
}