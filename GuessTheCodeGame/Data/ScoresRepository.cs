using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Data;

internal class ScoresRepository : IScoresRepository
{
    private readonly string _scoresFilePath;
    private const string _dataSeparator = "#&#";

    public ScoresRepository(string scoresFilePath)
    {
        _scoresFilePath = scoresFilePath;
    }

    public IEnumerable<IPlayerData> GetAllPlayerScores()
    {
        var playerScores = new List<IPlayerData>();
        using var scoresFileReader = new StreamReader(_scoresFilePath);

        while (!scoresFileReader.EndOfStream)
        {
            var line = scoresFileReader.ReadLine()!;
            var playerData = ParsePlayerDataFromTxtFile(line);
            var existingPlayer = playerScores.FirstOrDefault(p => p.Equals(playerData));
            
            if (existingPlayer is null)
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

    private PlayerData ParsePlayerDataFromTxtFile(string line)
    {
        var playerNameAndGuesses = line.Split(new string[] { _dataSeparator }, StringSplitOptions.None);
        var playerName = playerNameAndGuesses[0];
        var numberOfGuesses = Convert.ToInt32(playerNameAndGuesses[1]);

        return new PlayerData(playerName, numberOfGuesses);
    }

    public IEnumerable<IPlayerData> GetLeaderboard()
    {
        var playerScores = GetAllPlayerScores();

        return playerScores.OrderBy(p => p.CalculateAverageGuesses());
    }

    public void SavePlayerScore(string playerName, int numberOfGuesses)
    {
        using var scoresFileWriter = new StreamWriter(_scoresFilePath, append: true);
        scoresFileWriter.WriteLine($"{playerName} {_dataSeparator} {numberOfGuesses}");
    }
}
