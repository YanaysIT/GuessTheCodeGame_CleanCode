using System.Diagnostics;

namespace GuessTheCodeGame.Infrastructure;

public class PlayerScoresRepository : IPlayerScoresRepository
{
    private const string DataSeparator = "#&#";
    private readonly string _scoresFilePath;
    private readonly IFileStreamIO _fileIO;

    public PlayerScoresRepository(string scoresFilePath, IFileStreamIO fileIO)
    {
        _scoresFilePath = scoresFilePath;
        _fileIO = fileIO;
    }

    public IEnumerable<IPlayer> GetPlayerScoresByGame(GameTypes gameName)
    {
        var players = new List<IPlayer>();

        foreach (var line in _fileIO.ReadLines($"{gameName}_{_scoresFilePath}"))
        {
            IPlayer? parsedPlayer = ParsePlayerDataFromTxtFile(line);

            if (parsedPlayer == null)
            {
                continue;
            }

            IPlayer? existingPlayer = players.FirstOrDefault(p => p.Equals(parsedPlayer));

            if (existingPlayer == null)
            {
                players.Add(parsedPlayer);
            }
            else
            {
                existingPlayer.AddGameScore(parsedPlayer.TotalOfGuesses);
            }
        }

        return players;
    }

    private IPlayer? ParsePlayerDataFromTxtFile(string line)
    {
        try
        {
            string[] playerNameAndGuesses = line.Split(DataSeparator);
            string playerName = playerNameAndGuesses[0];
            int numberOfGuesses = Convert.ToInt32(playerNameAndGuesses[1]);

            return new Player(playerName, numberOfGuesses);
        }
        catch
        {
            Debug.WriteLine($"Unable to parse data in line: {line}.");
            return null;
        }
    }

    public IEnumerable<IPlayer> GetLeaderboard(GameTypes gameType)
    {
        IEnumerable<IPlayer> playerScores = GetPlayerScoresByGame(gameType);

        return playerScores.OrderBy(p => p.CalculateAverageGuesses());
    }

    public void SavePlayerScore(IPlayer player, GameTypes gameType)
    {
        string playerAsTextLine = $"{player.PlayerName}{DataSeparator}{player.TotalOfGuesses}";
        _fileIO.WriteLine($"{gameType}_{_scoresFilePath}", playerAsTextLine);
    }
}