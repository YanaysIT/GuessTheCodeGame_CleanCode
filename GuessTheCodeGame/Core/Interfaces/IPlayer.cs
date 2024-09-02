namespace GuessTheCodeGame.Core.Interfaces;

public interface IPlayer
{
    public string PlayerName { get; }
    public int GamesPlayed { get; }
    public int TotalOfGuesses { get; }

    public double CalculateAverageGuesses();
    public void AddGameScore(int numberOfGuesses);
}