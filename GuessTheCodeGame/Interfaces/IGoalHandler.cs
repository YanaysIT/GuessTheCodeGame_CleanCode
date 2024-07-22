namespace GuessTheCodeGame.Interfaces;

internal interface IGoalHandler
{
    string GenerateGoal();
    string CompareGuessAndGoal(string guess, string goal);
}
