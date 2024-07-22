namespace GuessTheCodeGame.Interfaces;

internal interface IScoresRepository
{
    IEnumerable<PlayerData> GetAllPlayerScores();
    IEnumerable<PlayerData> GetLeaderboard();
    void SavePlayerScore(string playerName, int numberOfGuesses);
}
