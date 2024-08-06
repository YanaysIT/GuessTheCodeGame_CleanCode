using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Application;

public class MooGameService : IGameService
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

    public BullsAndCows CompareGuessAndGoal(string goal, string guess)
    {
        guess = guess.PadRight(goal.Length).Substring(0, goal.Length);

        var bullsCount = 0;
        var cowsCount = 0;
        for (int i = 0; i < goal.Length; i++)
        {
            if (guess[i] == goal[i])
            {
                bullsCount++;
            }
            else if (guess.Contains(goal[i]))
            {
                cowsCount++;
            }
        }

        return new BullsAndCows(bullsCount, cowsCount);
    }
}
