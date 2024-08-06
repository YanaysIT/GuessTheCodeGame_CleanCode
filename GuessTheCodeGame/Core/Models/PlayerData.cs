using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Core.Models;

public class PlayerData : IPlayerData
{
    public string PlayerName { get; }
    public int GamesPlayed { get; private set; }
    public int TotalOfGuesses { get; private set; }

    public PlayerData(string playerName, int numberOfGuesses)
    {
        PlayerName = playerName;
        GamesPlayed = 1;
        TotalOfGuesses = numberOfGuesses;
    }

    public void UpdatePlayerScores(int numberOfGuesses)
    {
        TotalOfGuesses += numberOfGuesses;
        GamesPlayed++;
    }

    public double CalculateAverageGuesses()
    {
        return GamesPlayed == 0 ? 0 : (double)TotalOfGuesses / GamesPlayed;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || (obj is not PlayerData))
        {  
            return false; 
        }

        return PlayerName == ((PlayerData) obj).PlayerName;
    }

    public override int GetHashCode()
    {
        return PlayerName.GetHashCode();
    }
}