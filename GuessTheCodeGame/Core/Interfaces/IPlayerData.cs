namespace GuessTheCodeGame.Core.Interfaces
{
    public interface IPlayerData
    {
        public int GamesPlayed { get; }
        public string PlayerName { get; }
        public int TotalOfGuesses { get; }

        public double CalculateAverageGuesses();
        public bool Equals(object? playerData);
        public int GetHashCode();
        public void UpdatePlayerScores(int numberOfGuesses);
    }
}