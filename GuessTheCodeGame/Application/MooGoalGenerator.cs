using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Application;

public class MooGoalGenerator : IGoalGenerator
{
    private readonly int _goalLength;
    private readonly int _maxDigitValue;
    private readonly Random _randomGenerator;

    public MooGoalGenerator(int goalLength, int maxDigitValue)
    {
        if (goalLength > maxDigitValue + 1)
        {
            throw new ArgumentException($"Goal length {goalLength} cannot be greater than maximum digit value {maxDigitValue}.");
        }

        _goalLength = goalLength;
        _maxDigitValue = maxDigitValue;
        _randomGenerator = new Random();
    }

    public string GenerateGoal()
    { 
        string goal = string.Empty;

        while (goal.Length < _goalLength)
        {
            var randomDigit = _randomGenerator.Next(_maxDigitValue + 1).ToString();
            if (!goal.Contains(randomDigit))
            {
                goal += randomDigit;
            }
        }

        return goal;
    }
}
