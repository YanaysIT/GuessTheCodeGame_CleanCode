using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Application;

internal class GoalGenerator : IGoalGenerator
{
    private readonly int _goalLength;
    private readonly int _maxDigitValue;
    private readonly Random _randomGenerator;

    public GoalGenerator(int goalLength, int maxDigitValue)
    {
        if (goalLength > maxDigitValue + 1)
        {
            throw new ArgumentException("Goal length cannot be greater than the range of unique digits.");
        }

        _goalLength = goalLength;
        _maxDigitValue = maxDigitValue;
        _randomGenerator = new Random();
    }

    public string GenerateGoal()
    { 
        var goal = string.Empty;

        while (goal.Length < _goalLength)
        {
            string randomDigit = _randomGenerator.Next(_maxDigitValue + 1).ToString();
            if (!goal.Contains(randomDigit))
            {
                goal += randomDigit;
            }
        }

        return goal;
    }
}
