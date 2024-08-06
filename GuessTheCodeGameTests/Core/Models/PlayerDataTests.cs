using System.Numerics;
using System.Xml.Linq;

namespace GuessTheCodeGame.Core.Models.Tests
{
    [TestClass()]
    public class PlayerDataTests
    {
        PlayerData player1;
        PlayerData player2;
        PlayerData player3;

        [TestInitialize]
        public void Initialize()
        {
            player1 = new PlayerData("Player1", 5);
            player2 = new PlayerData("Player1", 10);
            player3 = new PlayerData("Player2", 10);
        }
        
        [TestMethod()]
        public void UpdatePlayerScores_ShouldUpdatePlayerScoresCorrectly()
        {
            // Arrange
            var additionalGuesses = 4;

            //Act
            player2.UpdatePlayerScores(additionalGuesses);
            var actualGamesPlayed = player2.GamesPlayed;
            var expectedGamesPlayed = 2;
            var actualTotalOfGuesses = player2.TotalOfGuesses;
            var expectedTotalOfGuesses = 14;

            //Assert
            Assert.IsTrue(expectedGamesPlayed == actualGamesPlayed && expectedTotalOfGuesses == actualTotalOfGuesses,
                $"Expected games played and total of guesses was [{expectedGamesPlayed}, {expectedTotalOfGuesses}], " +
                $"but it's [{actualGamesPlayed}, {actualTotalOfGuesses}.");
        }

        [TestMethod]
        public void CalculateAverageGuesses_ShouldReturnCorrectAverage()
        {
            // Arrange
            player2.UpdatePlayerScores(5);
            double expectedAverage = (10 + 5) / 2.0;

            // Act
            double actualAverage = player2.CalculateAverageGuesses();

            // Assert
            Assert.AreEqual(expectedAverage, actualAverage, $"Expected average was {expectedAverage}, but it's {actualAverage}");
        }

        [TestMethod()]
        public void Equals_ShouldReturnTrueForSamePlayerNames()
        {
            // Act
            bool areSamePlayer = player1.Equals(player2);

            //Assert
            Assert.IsTrue(areSamePlayer, $"Player {player1.PlayerName} and {player2.PlayerName} are equal.");
        }

        [TestMethod()]
        public void Equals_ShouldReturnFalseForDifferentPlayerNames()
        {
            //Act
            bool areSamePlayer = player1.Equals(player3);

            //Assert
            Assert.IsFalse(areSamePlayer, "Players with different names should not be considered equal.");
        }

        [TestMethod()]
        public void GetHashCode_ShouldReturnSameHashForSamePlayerName()
        {
            // Act
            var player1HashCode = player1.GetHashCode();
            var player2HashCode = player2.GetHashCode();

            // Act & Assert
            Assert.AreEqual(player1HashCode, player2HashCode, "Players with the same name should have the same hash code.");
        }

        [TestMethod()]
        public void GetHashCode_ShouldReturnDifferentHashForDifferentPlayerNames()
        {
            // Act
            var player1HashCode = player1.GetHashCode();
            var player3HashCode = player3.GetHashCode();

            // Act & Assert
            Assert.AreNotEqual(player1HashCode, player3HashCode, "Different names should return different HashCode.");
        }
    }
}