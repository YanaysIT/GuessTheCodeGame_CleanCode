namespace GuessTheCodeGame.Core.Interfaces;

public interface IGuessComparer
{
    public GuessFeedback CompareGuessAndGoal(string guess, string goal);
}
