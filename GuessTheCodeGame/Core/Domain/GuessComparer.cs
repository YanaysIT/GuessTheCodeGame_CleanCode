namespace GuessTheCodeGame.Core.Domain;

public class GuessComparer : IGuessComparer
{
    private int _exactMatchesCount;
    private int _partialMatchesCount;

    private List<char> _unmatchedElementsInGoal = new();
    private List<char> _unmatchedElementsInGuess = new();

    public GuessFeedback CompareGuessAndGoal(string guess, string goal)
    {
        ResetComparerState();
        CountExactMatches(guess, goal);
        CountPartialMatches();

        return new GuessFeedback(_exactMatchesCount, _partialMatchesCount);
    }

    private void ResetComparerState()
    {
        _exactMatchesCount = 0;
        _partialMatchesCount = 0;

        _unmatchedElementsInGoal = new();
        _unmatchedElementsInGuess = new();
    }

    private void CountExactMatches(string guess, string goal)
    {
        for (int i = 0; i < goal.Length; i++)
        {
            if (guess[i] == goal[i])
            {
                _exactMatchesCount++;
            }
            else
            {
                _unmatchedElementsInGoal.Add(goal[i]);
                _unmatchedElementsInGuess.Add(guess[i]);
            }
        }
    }

    private void CountPartialMatches()
    {
        foreach (char element in _unmatchedElementsInGuess)
        {
            if (_unmatchedElementsInGoal.Contains(element))
            {
                _partialMatchesCount++;
                _unmatchedElementsInGoal.Remove(element); // Prevent double-counting
            }
        }
    }
}