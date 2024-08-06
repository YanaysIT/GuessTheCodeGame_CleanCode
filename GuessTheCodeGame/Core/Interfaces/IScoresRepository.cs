﻿namespace GuessTheCodeGame.Core.Interfaces;

public interface IScoresRepository
{
    public IEnumerable<IPlayerData> GetAllPlayerScores();
    public IEnumerable<IPlayerData> GetLeaderboard();
    public void SavePlayerScore(IPlayerData playerData);
}
