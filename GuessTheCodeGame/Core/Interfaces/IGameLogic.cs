namespace GuessTheCodeGame.Core.Interfaces;

public interface IGameLogic
{
    public GameTypes GameName { get; }
    public bool IsCorrectGuess { get; }
    public string GenerateGoal();
    public string CompareGuessAndGoal(string guess);  
}