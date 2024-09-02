namespace GuessTheCodeGame.Core.Interfaces;

public interface IUI
{
    public string? GetUserInput();
    public void ShowMessage(string message);
    public string GetPlayerName();
    public void ShowGameStartMessage(string goal);
    public void ShowRoundOutcome(int numberOfGuesses);
    public bool ShouldContinuePlaying();
    public void ShowLeaderBoard(IEnumerable<IPlayer> leaderBoard);
    public void Clear();
    public void Exit();
}
