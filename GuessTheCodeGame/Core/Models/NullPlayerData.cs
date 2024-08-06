using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Core.Models;

public class NullPlayerData : IPlayerData
{
    public string PlayerName => "Unknown";

    public int GamesPlayed => 0;

    public int TotalOfGuesses => 0;

    public double CalculateAverageGuesses() => 0;

    public void UpdatePlayerScores(int numberOfGuesses) { }
}
