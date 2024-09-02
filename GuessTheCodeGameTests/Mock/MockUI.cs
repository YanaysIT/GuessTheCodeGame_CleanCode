using GuessTheCodeGame.Core.Utilities;

namespace GuessTheCodeGameTests.Mock;

public class MockUI : IUI
{
    private readonly Queue<string> _inputs;
    private readonly List<string> _messages = new ();

    public MockUI(Queue<string> inputs)
    {
        _inputs = inputs;
    }

    public string? GetUserInput()
    {
        return _inputs.Count > 0 ? _inputs.Dequeue() : null;
    }

    public void ShowMessage(string message)
    {
        _messages.Add(message);
    }

    public string? GetPlayerName()
    {
        return GetUserInput();
    }

    public void ShowGameStartMessage(string goal)
    {
        //nothing
    }

    public void ShowRoundOutcome(int numberOfGuesses)
    {
       //nothing
    }

    public bool ShouldContinuePlaying()
    {
        return _inputs.Dequeue().ToLower() == "y";
    }

    public void ShowLeaderBoard(IEnumerable<IPlayer> leaderBoard)
    {
        _messages.Add(leaderBoard.GetFormattedLeaderboard());
    }

    public void Clear()
    {
        //Nothing
    }

    public void Exit()
    {
        //Nothing
    }
}