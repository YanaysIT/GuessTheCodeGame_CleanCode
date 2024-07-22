using GuessTheCodeGame.Interfaces;

namespace GuessTheCodeGame;

internal class MooGoalHandler : IGoalHandler
{
    private const int _goalLength = 4;
    public MooGoalHandler() { }

    public string GenerateGoal()
    {
        var randomGenerator = new Random();
        var goal = string.Empty;
        while (goal.Length < _goalLength)
        {
            string randomDigit = randomGenerator.Next(10).ToString();
            if (!goal.Contains(randomDigit))
            {
                goal += randomDigit;
            }
        }
        return goal;
    }

    public string CompareGuessAndGoal(string guess, string goal)
    {
        var cows = string.Empty;
        var bulls = string.Empty;
        var index = -1;
        foreach (var g in guess)
        {
            index++;
            if (!goal.Contains(g))
            {
                continue;
            }
       
            if (g == goal[index])
            {
                bulls += "B";
            } 
            else
            {
                cows += "C";
            }      
        }
        return $"{bulls},{cows}";
    }
}
