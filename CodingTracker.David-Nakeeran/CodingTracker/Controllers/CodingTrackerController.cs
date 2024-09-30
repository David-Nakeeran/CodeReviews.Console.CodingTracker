using CodingTracker.Coordinators;
using CodingTracker.Utilities;
using CodingTracker.Views;
using Spectre.Console;

namespace CodingTracker.Controller;

class CodingTrackerController
{
    private readonly Validation _validation;
    private readonly InputHandler _inputHandler;
    private readonly TimeCalculator _timeCalculator;


    public CodingTrackerController(Validation validation, InputHandler inputHandler, TimeCalculator timeCalculator)
    {
        _validation = validation;
        _inputHandler = inputHandler;
        _timeCalculator = timeCalculator;
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
}