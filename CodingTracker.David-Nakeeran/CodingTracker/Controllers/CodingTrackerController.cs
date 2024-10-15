using CodingTracker.Utilities;
using Spectre.Console;

namespace CodingTracker.Controller;

class CodingTrackerController
{
    private readonly Validation _validation;
    private readonly InputHandler _inputHandler;
    private readonly TimeCalculator _timeCalculator;
    private readonly Conversion _conversion;

    public CodingTrackerController(Validation validation, InputHandler inputHandler, TimeCalculator timeCalculator, Conversion conversion)
    {
        _validation = validation;
        _inputHandler = inputHandler;
        _timeCalculator = timeCalculator;
        _conversion = conversion;
    }

    internal (string? startTime, string? endTime) GetTimeInputs()
    {
        string startTime = _validation.GetValidatedTimeInput("Please enter start time in the format of hh:mm or enter 0 to return to main menu", _inputHandler);
        if (startTime == "0") return (null, null);

        string endTime = _validation.GetValidatedTimeInput("Please enter end time in the format of hh:mm or enter 0 to return to main menu", _inputHandler);
        if (endTime == "0") return (null, null);

        while (!_timeCalculator.IsEndTimeGreater(startTime, endTime))
        {
            endTime = _validation.GetValidatedTimeInput("Please enter end time later than start time or enter 0 to return to main menu", _inputHandler);
            if (endTime == "0") return (null, null);
        }

        return (startTime, endTime);
    }

    internal int GetIdInput()
    {
        int inputNum;

        do
        {
            var recordForDeletion = AnsiConsole.Prompt(
            new TextPrompt<string>("Please enter the id of the record you wish to delete or 0 to return to main menu"));
            if (recordForDeletion == "0") return 0;
            recordForDeletion = _validation.CheckInputNullOrWhitespace("Please enter the id of the record you wish to delete", recordForDeletion);
            inputNum = _conversion.ParseInt(recordForDeletion, "Please enter number of id");

        } while (inputNum < 0);

        return inputNum;
    }
}