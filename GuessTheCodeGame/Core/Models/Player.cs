namespace GuessTheCodeGame.Core.Models;

public class Player : IPlayer
{
    public string PlayerName { get; }
    public int GamesPlayed { get; private set; }
    public int TotalOfGuesses { get; private set; }

    public Player(string playerName, int numberOfGuesses)
    {
        PlayerName = playerName;
        GamesPlayed = 1;
        TotalOfGuesses = numberOfGuesses;
    }

    public void AddGameScore(int numberOfGuesses)
    {
        TotalOfGuesses += numberOfGuesses;
        GamesPlayed++;
    }

    public double CalculateAverageGuesses() => (double)TotalOfGuesses / GamesPlayed;
 

    public override bool Equals(object? otherPlayer)
    {
        if (otherPlayer == null || (otherPlayer is not Player))
        {  
            return false; 
        }

        return PlayerName == ((Player) otherPlayer).PlayerName;
    }

    public override int GetHashCode() => PlayerName.GetHashCode();
}