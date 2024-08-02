using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;
using Moq;

namespace GuessTheCodeGame.Application.Tests
{
    [TestClass()]
    public class MooGameServiceTests
    {
        private Mock<IGoalGenerator<string>> _mockGoalGenerator;
        private MooGameService _mooGameService;

        [TestInitialize]
        public void Initialize()
        {
            _mockGoalGenerator = new Mock<IGoalGenerator<string>>();
            _mooGameService = new MooGameService(_mockGoalGenerator.Object);
        }

        [TestMethod()]
        [DataRow()]
        public void CompareGuessAndGoal_ShouldHandleExactMatch()
        {
            //Arrange
            var goal = "5432";
            var guess = "5432";
            var expectedBullsAndCows = new BullsAndCows(4, 0);

            //Act
            var actualBullsAndCows = _mooGameService.CompareGuessAndGoal(guess, goal);

            //Assert
            Assert.IsTrue(expectedBullsAndCows.BullsCount == actualBullsAndCows.BullsCount && expectedBullsAndCows.CowsCount == actualBullsAndCows.CowsCount,
                $"The expected number of Bulls and Cows was {expectedBullsAndCows.BullsCount} and {expectedBullsAndCows.CowsCount} respectively, but it's " +
                $"{actualBullsAndCows.BullsCount} and {actualBullsAndCows.CowsCount}.");
        }

        [TestMethod()]
        public void CompareGuessAndGoal_ShouldHandlePartialMatch()
        {
            //Arrange
            var goal = "1234";
            var guess = "1431";
            var expectedBullsAndCows = new BullsAndCows(2, 1);

            //Act
            var actualBullsAndCows = _mooGameService.CompareGuessAndGoal(guess, goal);

            //Assert
            Assert.IsTrue(expectedBullsAndCows.BullsCount == actualBullsAndCows.BullsCount && expectedBullsAndCows.CowsCount == actualBullsAndCows.CowsCount,
                $"The expected number of Bulls and cows was {expectedBullsAndCows.BullsCount} and {expectedBullsAndCows.CowsCount} respectively, but it's " +
                $"{actualBullsAndCows.BullsCount} and {actualBullsAndCows.CowsCount}.");

        }

        [TestMethod()]
        public void CompareGuessAndGoal_ShouldHandleNoMatch()
        {
            //Arrange
            var goal = "1234";
            var guess = "5678";
            var expectedBullsAndCows = new BullsAndCows(0, 0);

            //Act
            var actualBullsAndCows = _mooGameService.CompareGuessAndGoal(guess, goal);

            //Assert
            Assert.IsTrue(expectedBullsAndCows.BullsCount == actualBullsAndCows.BullsCount && expectedBullsAndCows.CowsCount == actualBullsAndCows.CowsCount,
                $"The expected number of Bulls and cows was {expectedBullsAndCows.BullsCount} and {expectedBullsAndCows.CowsCount} respectively, but it's " +
                $"{actualBullsAndCows.BullsCount} and {actualBullsAndCows.CowsCount}.");
        }

        [TestMethod]
        public void CompareGuessAndGoal_ShouldHandleLongerGuess()
        {
            // Arrange
            var goal = "1234";
            var guess = "123456";
            var expectedBullsAndCows = new BullsAndCows(4, 0);

            // Act
            var actualBullsAndCows = _mooGameService.CompareGuessAndGoal(guess, goal);

            // Assert
            Assert.IsTrue(expectedBullsAndCows.BullsCount == actualBullsAndCows.BullsCount && expectedBullsAndCows.CowsCount == actualBullsAndCows.CowsCount,
                $"The expected number of Bulls and cows was {expectedBullsAndCows.BullsCount} and {expectedBullsAndCows.CowsCount} respectively, but it's " +
                $"{actualBullsAndCows.BullsCount} and {actualBullsAndCows.CowsCount}.");
        }

        [TestMethod]
        public void CompareGuessAndGoal_ShouldHandleShorterGuess()
        {
            // Arrange
            var goal = "1234";
            var guess = "02";
            var expectedBullsAndCows = new BullsAndCows(1, 0);

            // Act
            var actualBullsAndCows = _mooGameService.CompareGuessAndGoal(guess, goal);

            // Assert
            Assert.IsTrue(expectedBullsAndCows.BullsCount == actualBullsAndCows.BullsCount && expectedBullsAndCows.CowsCount == actualBullsAndCows.CowsCount,
                $"The expected number of Bulls and cows was {expectedBullsAndCows.BullsCount} and {expectedBullsAndCows.CowsCount} respectively, but it's " +
                $"{actualBullsAndCows.BullsCount} and {actualBullsAndCows.CowsCount}.");
        }

        [TestMethod]
        public void CompareGuessAndGoal_ShouldHandleEmptyGuess()
        {
            // Arrange
            var goal = "1234";
            var guess = "";
            var expectedBullsAndCows = new BullsAndCows(0, 0);

            // Act
            var actualBullsAndCows = _mooGameService.CompareGuessAndGoal(guess, goal);

            //Assert
            Assert.IsTrue(expectedBullsAndCows.BullsCount == actualBullsAndCows.BullsCount && expectedBullsAndCows.CowsCount == actualBullsAndCows.CowsCount,
                $"The expected number of Bulls and cows was {expectedBullsAndCows.BullsCount} and {expectedBullsAndCows.CowsCount} respectively, but it's " +
                $"{actualBullsAndCows.BullsCount} and {actualBullsAndCows.CowsCount}.");
        }
    }
}