using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Application;

internal class MooGameService : IGameService
{
    private readonly IGoalGenerator _goalGenerator;

    public MooGameService(IGoalGenerator goalGenerator) 
    {
        _goalGenerator = goalGenerator;
    }

    public string GenerateGoal()
    {
        return _goalGenerator.GenerateGoal();
    }

    public BullsAndCows CompareGuessAndGoal(string guess, string goal)
    {
        var cowsCount = 0;
        var bullsCount = 0;

        guess = guess.PadRight(goal.Length);

        var index = 0;
        foreach (var g in goal)
        {
            if (guess[index] == g)
            {
                bullsCount++;
            }
            else if (guess.Contains(g))
            {
                cowsCount++;
            }
            index++;
        }

        return new BullsAndCows(bullsCount, cowsCount);
    }
}
