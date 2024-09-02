namespace GuessTheCodeGame.Core.Models.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        private Player player1;
        private Player player2;
        private Player player3;

        [TestInitialize]
        public void Initialize()
        {
            player1 = new Player("Lola", 5);
            player2 = new Player("Lola", 10);
            player3 = new Player("Mario", 10);
        }
        
        [TestMethod()]
        public void UpdatePlayerScores_ShouldUpdateScoresCorrectly()
        {
            int additionalGuesses = 4;
            int expectedGamesPlayed = 2;
            int expectedTotalOfGuesses = 14;

            player2.AddGameScore(additionalGuesses);
            int actualGamesPlayed = player2.GamesPlayed;
            int actualTotalOfGuesses = player2.TotalOfGuesses;

            Assert.AreEqual(expectedGamesPlayed, actualGamesPlayed, $"Expected games played to be {expectedGamesPlayed}, but got {actualGamesPlayed}.");
            Assert.AreEqual(expectedTotalOfGuesses, actualTotalOfGuesses, $"Expected total guesses to be {expectedTotalOfGuesses}, but got {actualTotalOfGuesses}.");
        }

        [TestMethod]
        public void CalculateAverageGuesses_ShouldReturnCorrectAverage()
        {
            player2.AddGameScore(5);
            double expectedAverage = (10 + 5) / 2.0;

            double actualAverage = player2.CalculateAverageGuesses();

            Assert.AreEqual(expectedAverage, actualAverage, $"Expected average was {expectedAverage}, but it's {actualAverage}");
        }

        [TestMethod()]
        public void Equals_ShouldReturnTrueForSamePlayerNames()
        {
            bool areSamePlayer = player1.Equals(player2);

            Assert.IsTrue(areSamePlayer, $"Player {player1.PlayerName} and {player2.PlayerName} are equal.");
        }

        [TestMethod()]
        public void Equals_ShouldReturnFalseForDifferentPlayerNames()
        {
            bool areSamePlayer = player1.Equals(player3);

            Assert.IsFalse(areSamePlayer, "Players with different names should not be considered equal.");
        }

        [TestMethod()]
        public void GetHashCode_ShouldReturnSameHashForSamePlayerName()
        {
            int player1HashCode = player1.GetHashCode();
            int player2HashCode = player2.GetHashCode();

            Assert.AreEqual(player1HashCode, player2HashCode, "Players with the same name should have the same hash code.");
        }

        [TestMethod()]
        public void GetHashCode_ShouldReturnDifferentHashForDifferentPlayerNames()
        {
            int player1HashCode = player1.GetHashCode();
            int player3HashCode = player3.GetHashCode();

            Assert.AreNotEqual(player1HashCode, player3HashCode, "Different names should return different HashCode.");
        }
    }
}