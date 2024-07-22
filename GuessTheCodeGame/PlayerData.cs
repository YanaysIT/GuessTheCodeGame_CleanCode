namespace GuessTheCodeGame;

public class PlayerData
{
	public string PlayerName { get; private set; }
    public int GamesPlayed { get; private set; }
	private int _totalOfGuesses;

	public PlayerData(string playerName, int numberOfGuesses)
	{
		this.PlayerName = playerName;
        GamesPlayed = 1;
        _totalOfGuesses = numberOfGuesses;
	}

	public void RecordGame(int numberOfGuesses)
	{
        _totalOfGuesses += numberOfGuesses;
        GamesPlayed++;
	}

	public double CalculateAverageGuesses()
	{
		return (double)_totalOfGuesses / GamesPlayed;
	}

    public override bool Equals(Object? playerData)
	{
		return playerData is PlayerData data && PlayerName.Equals(data.PlayerName);
	}

    public override int GetHashCode()
    {
		return PlayerName.GetHashCode();
	}
}