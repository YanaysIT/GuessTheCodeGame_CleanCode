using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;
using GuessTheCodeGameTests.Mock;

namespace GuessTheCodeGame.Data.Tests;

[TestClass()]
public class ScoresRepositoryTests
{
    private readonly string _testFilePath = "test_results.txt";
    private readonly string _dataSeparator = "#&#";
    private IScoresRepository _scoresRepository;
    private MockFileIO _fileIOMock;

    [TestInitialize]
    public void TestInitialize()
    {
        var initialPlayerDataLines = new List<string>
            {
                $"Player1{_dataSeparator}10",
                $"Player2{_dataSeparator}6",
                $"Player1{_dataSeparator}5"
            };
        _fileIOMock = new MockFileIO(initialPlayerDataLines);
        _scoresRepository = new ScoresRepository(_testFilePath, _fileIOMock);
    }

    [TestMethod]
    public void GetAllPlayerScores_ShouldReturnDistinctPlayersLines()
    {
        // Act
        var actualDistinctPlayersCount = _scoresRepository.GetAllPlayerScores().Count();
        var expectedDistinctPlayersCount = 2;

        // Assert
        Assert.AreEqual(expectedDistinctPlayersCount, actualDistinctPlayersCount, 
            $"Expected amount of distinct players was {expectedDistinctPlayersCount}, but it's {actualDistinctPlayersCount}.");
    }

    [TestMethod]
    public void GetAllPlayerScores_ShouldReturnCorrectDistinctPlayerScores()
    {
        // Act
        var actualDistinctPlayerScores = _scoresRepository.GetAllPlayerScores().ToList();

        // Assert
        Assert.AreEqual("Player1", actualDistinctPlayerScores[0].PlayerName, "PlayerName mismatch for Player1.");
        Assert.AreEqual(2, actualDistinctPlayerScores[0].GamesPlayed, "GamesPlayed mismatch for Player1.");
        Assert.AreEqual(15, actualDistinctPlayerScores[0].TotalOfGuesses, "TotalOfGuesses mismatch for Player1.");
        Assert.AreEqual("Player2", actualDistinctPlayerScores[1].PlayerName, "PlayerName mismatch for Player2.");
        Assert.AreEqual(1, actualDistinctPlayerScores[1].GamesPlayed, "GamesPlayed mismatch for Player2.");
        Assert.AreEqual(6, actualDistinctPlayerScores[1].TotalOfGuesses, "TotalOfGuesses mismatch for Player2.");
    }

    [TestMethod]
    public void GetAllPlayerScores_ShouldHandleCorruptedDataLines()
    {
        // Arrange
        var corruptedData = new List<string> { $"Player1{_dataSeparator}10", "CorruptedData", $"Player2{_dataSeparator}20" };
        var mockFileIO = new MockFileIO(corruptedData);
        var scoresRepository = new ScoresRepository(_testFilePath, mockFileIO);
        var expectedValidLinesCount = 2;

        // Act
        var actualValidLinesCount = scoresRepository.GetAllPlayerScores().Count();

        // Assert
        Assert.AreEqual(expectedValidLinesCount, actualValidLinesCount, $"There should be {expectedValidLinesCount} valid player scores despite one corrupted line.");
    }

    [TestMethod]
    public void GetAllPlayerScores_ShouldHandleNonExistentFile()
    {
        // Arrange
        var fileIOMock = new MockFileIO(new List<string>());
        var scoresRepository = new ScoresRepository("nonexistentfile.txt", fileIOMock);

        // Act
        var playerScores = scoresRepository.GetAllPlayerScores();

        // Assert
        Assert.IsFalse(playerScores.Any(), "The playerScores should be an empty list when the file does not exist.");
    }

    [TestMethod()]
    public void GetLeaderboard_ShouldReturnPlayersInCorrectOrder()
    {
        // Act
        var actualLeaderboard = _scoresRepository.GetLeaderboard().ToList();

        // Assert
        Assert.AreEqual("Player2", actualLeaderboard[0].PlayerName, "Player2 should be the leader.");
        Assert.AreEqual("Player1", actualLeaderboard[1].PlayerName, "Player1 should be the second.");
    }

    [TestMethod]
    public void SavePlayerScore_ShouldAddDataToFile()
    {
        // Arrange
        var playerData = new PlayerData("Player3", 10);
        var expectedWrittenContent = $"{playerData.PlayerName}{_dataSeparator}{playerData.TotalOfGuesses}";

        // Act
        _scoresRepository.SavePlayerScore(playerData);
        var actualWrittenContent = _fileIOMock.LinesWritten[0];

        // Assert
        Assert.AreEqual(expectedWrittenContent, actualWrittenContent, $"Expected file content was {expectedWrittenContent}, but it's {actualWrittenContent}.");
    }

    [TestMethod]
    public void SavePlayerScore_ShouldHandleNonExistentFile()
    {
        // Arrange
        var fileIOMock = new MockFileIO(new List<string>());
        var scoresRepository = new ScoresRepository("nonexistentfile.txt", fileIOMock);
        var playerData = new PlayerData("Player3", 10);
        var expectedWrittenContent = $"{playerData.PlayerName}{_dataSeparator}{playerData.TotalOfGuesses}";

        // Act
        scoresRepository.SavePlayerScore(playerData);
        var actualWrittenContent = fileIOMock.LinesWritten[0];

        // Assert
        Assert.AreEqual(expectedWrittenContent, actualWrittenContent, $"Couldn't save player score to inexistent file.");
    }
}