namespace GuessTheCodeGame.Core.Interfaces;

internal interface IScoresRepository
{
    public IEnumerable<IPlayerData> GetAllPlayerScores();
    public IEnumerable<IPlayerData> GetLeaderboard();
    public void SavePlayerScore(string playerName, int numberOfGuesses);
}
