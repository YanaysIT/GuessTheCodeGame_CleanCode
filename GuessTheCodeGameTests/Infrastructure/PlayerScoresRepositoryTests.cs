namespace GuessTheCodeGame.Data.Tests;

[TestClass()]
public class PlayerScoresRepositoryTests
{
    private const string DataSeparator = "#&#";
    private const string TestFilePath = "test_results.txt";
    private const GameTypes gameType = GameTypes.Moo;
    private IPlayerScoresRepository _playerScoresRepository;
    private MockFileStreamIO _fileStreamIOMock;
    

    [TestInitialize]
    public void TestInitialize()
    {
        var initialPlayerDataLines = new List<string>
        {
            $"Player1{DataSeparator}10",
            $"Player2{DataSeparator}6",
            $"Player1{DataSeparator}5"
        };

        _fileStreamIOMock = new MockFileStreamIO(initialPlayerDataLines);
        _playerScoresRepository = new PlayerScoresRepository(TestFilePath, _fileStreamIOMock);
    }

    [TestMethod]
    public void GetPlayerScoresByGame_ShouldReturnDistinctPlayersLines()
    {
        int actualPlayerCount = _playerScoresRepository.GetPlayerScoresByGame(gameType).Count();
        int expectedPlayersCount = 2;

        Assert.AreEqual(expectedPlayersCount, actualPlayerCount,
            $"Expected amount of distinct players was {expectedPlayersCount}, but it's {actualPlayerCount}.");
    }

    [TestMethod]
    public void GetPlayerScoresByGame_ShouldReturnCorrectDistinctPlayerScores()
    {
        var actualPlayerScores = _playerScoresRepository.GetPlayerScoresByGame(gameType).ToList();

        Assert.AreEqual("Player1", actualPlayerScores[0].PlayerName, "ExpectedPlayerName mismatch for Player1.");
        Assert.AreEqual(2, actualPlayerScores[0].GamesPlayed, "GamesPlayed mismatch for Player1.");
        Assert.AreEqual(15, actualPlayerScores[0].TotalOfGuesses, "TotalOfGuesses mismatch for Player1.");
        Assert.AreEqual("Player2", actualPlayerScores[1].PlayerName, "ExpectedPlayerName mismatch for Player2.");
        Assert.AreEqual(1, actualPlayerScores[1].GamesPlayed, "GamesPlayed mismatch for Player2.");
        Assert.AreEqual(6, actualPlayerScores[1].TotalOfGuesses, "TotalOfGuesses mismatch for Player2.");
    }

    [TestMethod]
    public void GetPlayerScoresByGame_ShouldHandleCorruptedDataLines()
    {
        var corruptedData = new List<string> 
        { 
            $"Player1{DataSeparator}10", 
            "CorruptedData", 
            $"Player2{DataSeparator}20" 
        };
        var mockFileStreamIO = new MockFileStreamIO(corruptedData);
        var scoresRepository = new PlayerScoresRepository(TestFilePath, mockFileStreamIO);
        int expectedValidLinesCount = 2;

        int actualValidLinesCount = scoresRepository.GetPlayerScoresByGame(gameType).Count();

        Assert.AreEqual(expectedValidLinesCount, actualValidLinesCount, $"There should be {expectedValidLinesCount} valid player scores despite one corrupted line.");
    }

    [TestMethod]
    public void GetPlayerScoresByGame_ShouldHandleNonExistentFile()
    {
        var mockFileStreamIO = new MockFileStreamIO(new List<string>());
        var scoresRepository = new PlayerScoresRepository("nonexistentfile.txt", mockFileStreamIO);

        IEnumerable<IPlayer> playerScores = scoresRepository.GetPlayerScoresByGame(gameType);

        Assert.IsFalse(playerScores.Any(), "The players should be an empty list when the file does not exist.");
    }

    [TestMethod]
    public void GetPlayerScoresByGame_ShouldReturnEmptyList_WhenFileIsEmpty()
    {
        var emptyData = new List<string>();
        var mockFileStreamIO = new MockFileStreamIO(emptyData);
        var scoresRepository = new PlayerScoresRepository(TestFilePath, mockFileStreamIO);

        IEnumerable<IPlayer> players = scoresRepository.GetPlayerScoresByGame(gameType);

        Assert.IsFalse(players.Any(), "The players should be an empty list when the file is empty.");
    }

    [TestMethod()]
    public void GetLeaderboard_ShouldReturnPlayersInCorrectOrder()
    {
        List<IPlayer> actualLeaderboard = _playerScoresRepository.GetLeaderboard(gameType).ToList();

        Assert.AreEqual("Player2", actualLeaderboard[0].PlayerName, "Player2 should be the leader.");
        Assert.AreEqual("Player1", actualLeaderboard[1].PlayerName, "Player1 should be the second.");
    }

    [TestMethod]
    public void SavePlayerScore_ShouldAddDataToFile()
    {
        var player = new Player("Player3", 10);
        string expectedWrittenContent = $"{player.PlayerName}{DataSeparator}{player.TotalOfGuesses}";

        _playerScoresRepository.SavePlayerScore(player, gameType);
        string actualWrittenContent = _fileStreamIOMock.LinesWritten[0];

        Assert.AreEqual(expectedWrittenContent, actualWrittenContent, $"Expected file content was {expectedWrittenContent}, but it's {actualWrittenContent}.");
    }

    [TestMethod]
    public void SavePlayerScore_ShouldHandleNonExistentFile()
    {
        var mockFileStreamIO = new MockFileStreamIO(new List<string>());
        var scoresRepository = new PlayerScoresRepository("nonexistentfile.txt", mockFileStreamIO);
        var player = new Player("Player3", 10);
        string expectedWrittenContent = $"{player.PlayerName}{DataSeparator}{player.TotalOfGuesses}";

        scoresRepository.SavePlayerScore(player, gameType);
        string actualWrittenContent = mockFileStreamIO.LinesWritten[0];

        Assert.AreEqual(expectedWrittenContent, actualWrittenContent, $"Couldn't save player score to inexistent file.");
    }
}