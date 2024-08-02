namespace GuessTheCodeGame.Data.Tests;

[TestClass()]
public class ScoresRepositoryTests
{
    private readonly string testFilePath = "testScores.txt";

    [TestInitialize]
    public void TestInitialize()
    {
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [TestMethod()]
    public void SavePlayerScore_ShouldWriteDataToFile()
    {
        // Arrange
        var repo = new ScoresRepository(testFilePath);
        var playerName = "Lola";
        var numberOfGuesses = 5;

        // Act
        repo.SavePlayerScore(playerName, numberOfGuesses);

        // Assert
        var writtenContent = File.ReadAllLines(testFilePath).FirstOrDefault();
        Assert.AreEqual($"{playerName} #&# {numberOfGuesses}", writtenContent, "The file content does not match the expected format.");
    }

    [TestMethod()]
    public void GetAllPlayerScores_ShouldReadDataCorrectly()
    {
        // Arrange
        File.WriteAllText(testFilePath, "Bob#&#4\nAlice#&#5");
        var repo = new ScoresRepository(testFilePath);

        // Act
        var scores = repo.GetAllPlayerScores().ToList();

        // Assert
        Assert.AreEqual(2, scores.Count, "The number of scores read does not match the expected count.");
        Assert.IsTrue(scores.Any(s => s.PlayerName == "Alice" && s.TotalOfGuesses == 5), "Alice's data is incorrect or missing.");
        Assert.IsTrue(scores.Any(s => s.PlayerName == "Bob" && s.TotalOfGuesses == 4), "Bob's data is incorrect or missing.");
    }

    [TestMethod()]
    public void GetLeaderboard_ShouldReturnPlayersInCorrectOrder()
    {
        // Arrange
        File.WriteAllText(testFilePath, "Bob#&#4\nAlice#&#12\nCharlie#&#6");
        var repo = new ScoresRepository(testFilePath);

        // Act
        var leaderboard = repo.GetLeaderboard().ToList();

        // Assert
        Assert.AreEqual("Bob", leaderboard[0].PlayerName, "Bob should be the leader.");
        Assert.AreEqual("Charlie", leaderboard[1].PlayerName, "Charlie's position is incorrect.");
        Assert.AreEqual("Alice", leaderboard[2].PlayerName, "Alice's position is incorrect.");
    }

    [TestMethod()]
    public void GetAllPlayerScores_ShouldUpdateExistingPlayer()
    {
        // Arrange
        File.WriteAllText(testFilePath, "Alice#&#3\nAlice#&#7");
        var repo = new ScoresRepository(testFilePath);

        // Act
        var scores = repo.GetAllPlayerScores().ToList();

        // Assert
        Assert.AreEqual(1, scores.Count, "Duplicate entries should have been merged.");
        Assert.AreEqual(10, scores[0].TotalOfGuesses, "The total numberOfGuesses for Alice should have been summed.");
    }
}