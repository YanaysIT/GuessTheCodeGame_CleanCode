using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGameTests.Mock;

public class MockMooGoalGenerator : IGoalGenerator
{
    private readonly string _goal;

    public MockMooGoalGenerator(string goal)
    {
        _goal = goal;
    }

    public string GenerateGoal()
    {
        return _goal;
    }
}