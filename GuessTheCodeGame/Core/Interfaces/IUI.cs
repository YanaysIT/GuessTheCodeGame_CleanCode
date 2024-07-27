namespace GuessTheCodeGame.Core.Interfaces;

internal interface IUI
{
    public string GetUserInput();
    public void DisplayMessage(string message);
    public string GetPlayerName();
    public void DisplayGameStartMessage(string goal, bool isPracticeMode = true);
    public bool ShouldContinuePlaying();
    public void DisplayLeaderBoard(IEnumerable<IPlayerData> leaderBoard);
}
