namespace GuessTheCodeGame.Core.Interfaces;

public interface IUI
{
    public string GetUserInput();
    public void DisplayMessage(string message);
    public string GetPlayerName();
    public void DisplayGameStartMessage(string goal, bool isPracticeMode = true);
    public void DisplayWinningResult(int numberOfGuesses);
    public bool ShouldContinuePlaying();
    public void DisplayLeaderBoard(IEnumerable<IPlayerData> leaderBoard);
}
