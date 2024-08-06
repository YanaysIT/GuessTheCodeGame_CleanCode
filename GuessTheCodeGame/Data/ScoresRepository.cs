using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Data;

public class ScoresRepository : IScoresRepository
{
    private readonly string _scoresFilePath;
    private readonly IFileIO _fileManager;
    private const string _dataSeparator = "#&#";

    public ScoresRepository(string scoresFilePath, IFileIO fileManager)
    {
        _scoresFilePath = scoresFilePath;
        _fileManager = fileManager;
    }

    public IEnumerable<IPlayerData> GetAllPlayerScores()
    {
        var playerScores = new List<IPlayerData>();
        var fileLines = _fileManager.ReadLines(_scoresFilePath);
       
        foreach (var line in fileLines)
        {
            var playerData = ParsePlayerDataFromTxtFile(line);

            if (playerData is NullPlayerData)
            {
                continue;
            }

            var existingPlayer = playerScores.FirstOrDefault(p => p.Equals(playerData));

            if (existingPlayer == null)
            {
                playerScores.Add(playerData);
            }
            else
            {
                existingPlayer.UpdatePlayerScores(playerData.TotalOfGuesses);
            }
        }

        return playerScores;
    }

    private IPlayerData ParsePlayerDataFromTxtFile(string line)
    {
        try
        {
            var playerNameAndGuesses = line.Split(_dataSeparator);
            var playerName = playerNameAndGuesses[0];
            var numberOfGuesses = Convert.ToInt32(playerNameAndGuesses[1]);

            return new PlayerData(playerName, numberOfGuesses);
        }
        catch
        {
            return new NullPlayerData();
        }
    }

    public IEnumerable<IPlayerData> GetLeaderboard()
    {
        var playerScores = GetAllPlayerScores();

        return playerScores.OrderBy(p => p.CalculateAverageGuesses());
    }

    public void SavePlayerScore(IPlayerData playerData)
    {
        _fileManager.WriteLine(_scoresFilePath,$"{playerData.PlayerName}{_dataSeparator}{playerData.TotalOfGuesses}");
    }
}