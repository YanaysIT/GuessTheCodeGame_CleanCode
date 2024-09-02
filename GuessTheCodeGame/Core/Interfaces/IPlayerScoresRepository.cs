namespace GuessTheCodeGame.Core.Interfaces;

public interface IPlayerScoresRepository
{
    public IEnumerable<IPlayer> GetPlayerScoresByGame(GameTypes gameType);
    public IEnumerable<IPlayer> GetLeaderboard(GameTypes gameType);
    public void SavePlayerScore(IPlayer playerData, GameTypes gameType);
}
