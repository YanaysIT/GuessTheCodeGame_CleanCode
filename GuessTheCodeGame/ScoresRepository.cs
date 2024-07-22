using GuessTheCodeGame.Interfaces;

namespace GuessTheCodeGame;

internal class ScoresRepository : IScoresRepository
{
    private readonly string _scoresFilePath = "result.txt";
    private readonly string _dataSeparator = "#&#";

    public IEnumerable<PlayerData> GetAllPlayerScores()
    {
        var playerScores = new List<PlayerData>();

        using var scoresFileReader = new StreamReader(_scoresFilePath);
        string? line;
        while ((line = scoresFileReader.ReadLine()) != null)
        {
            string[] nameAndGuesses = line.Split(new string[] { _dataSeparator }, StringSplitOptions.None);
            string playerName = nameAndGuesses[0];
            int numberOfGuesses = Convert.ToInt32(nameAndGuesses[1]);

            var playerData = new PlayerData(playerName, numberOfGuesses);
            var existingPlayer = playerScores.FirstOrDefault(p => p.Equals(playerData));
            if (existingPlayer is null)
            {
                playerScores.Add(playerData);
            }
            else
            {
                existingPlayer.RecordGame(numberOfGuesses);
            }
        }
        return playerScores;
    }

    public IEnumerable<PlayerData> GetLeaderboard()
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
