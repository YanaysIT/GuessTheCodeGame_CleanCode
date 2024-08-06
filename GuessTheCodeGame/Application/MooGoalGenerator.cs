using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Application;

public class MooGoalGenerator : IGoalGenerator
{
    private readonly int _goalLength = 4;
    private readonly int _maxDigitValue = 9;
    private readonly IRandomNumberGenerator _randomNumberGenerator;

    public MooGoalGenerator(IRandomNumberGenerator randomNumberGenerator)
    {
        _randomNumberGenerator = randomNumberGenerator;
    }

    public string GenerateGoal()
    { 
        var goal = string.Empty;

        while (goal.Length < _goalLength)
        {
            var randomDigit = _randomNumberGenerator.Next(_maxDigitValue + 1).ToString();
            if (!goal.Contains(randomDigit))
            {
                goal += randomDigit;
            }
        }

        return goal;
    }
}
