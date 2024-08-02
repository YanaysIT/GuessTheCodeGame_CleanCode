using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Application;

public class MooGameService : IGameService<string>
{
    private readonly IGoalGenerator<string> _goalGenerator;

    public MooGameService(IGoalGenerator<string> goalGenerator) 
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

        var i = 0;
        foreach (var g in goal)
        {
            if (guess[i] == g)
            {
                bullsCount++;
            }
            else if (guess.Contains(g))
            {
                cowsCount++;
            }
            i++;
        }

        return new BullsAndCows(bullsCount, cowsCount);
    }
}
