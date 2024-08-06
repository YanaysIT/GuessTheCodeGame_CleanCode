using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGameTests.Mock;

namespace GuessTheCodeGame.Application.Tests
{
    [TestClass]
    public class MooGameServiceTests
    {
        private IGoalGenerator _goalGenerator;
        private MooGameService _gameService;
        private string _expectedGoal = "1234";

        [TestInitialize]
        public void Initialize()
        {
            _goalGenerator = new MockMooGoalGenerator(_expectedGoal);
            _gameService = new MooGameService(_goalGenerator);
        }

        [TestMethod]
        public void GenerateGoal_ShouldReturnExpectedGoal()
        {
            // Act
            string actualGoal = _gameService.GenerateGoal();

            // Assert
            Assert.AreEqual(_expectedGoal, actualGoal, $"Expected goal was {_expectedGoal}, but it's {actualGoal}.");
        }

        private void AssertBullsAndCows(string guess, int expectedBullsCount, int expectedCowsCount)
        {
            // Act
            var comparisonResult = _gameService.CompareGuessAndGoal(_expectedGoal, guess);
            var actualBullsCount = comparisonResult.BullsCount;
            var actualCowsCount = comparisonResult.CowsCount;

            // Assert
            Assert.IsTrue(expectedBullsCount == actualBullsCount && expectedCowsCount == actualCowsCount,
               $"Expected number of Bulls and Cows: [{expectedBullsCount}, {expectedCowsCount}], but it's [{actualBullsCount}, {actualCowsCount}].");
        }

        [TestMethod]
        [DataRow("1234", 4, 0)]
        [DataRow ("12345678", 4, 0)]
        public void CompareGuessAndGoal_ShouldHandleExactMatch(string guess, int expectedBullsCount, int expectedCowsCount)
        {

            AssertBullsAndCows(guess, expectedBullsCount, expectedCowsCount);
        }

        [TestMethod]
        [DataRow("1243", 2, 2)]
        [DataRow("3567", 0, 1)]
        [DataRow("1567243", 1, 0)]
        public void CompareGuessAndGoal_ShouldHandlePartialMatch(string guess, int expectedBullsCount, int expectedCowsCount)
        {
            AssertBullsAndCows(guess, expectedBullsCount, expectedCowsCount);
        }

        [TestMethod]
        [DataRow("5678", 0, 0)]
        public void CompareGuessAndGoal_ShouldHandleNoMatch(string guess, int expectedBullsCount, int expectedCowsCount)
        {
            AssertBullsAndCows(guess, expectedBullsCount, expectedCowsCount);
        }

        [TestMethod]
        [DataRow( "", 0, 0)]
        public void CompareGuessAndGoal_ShouldHandleEmptyGuess(string guess, int expectedBullsCount, int expectedCowsCount)
        {
            AssertBullsAndCows(guess, expectedBullsCount, expectedCowsCount);
        }

        [TestMethod]
        [DataRow("12345678", 4, 0)]
        [DataRow("1299345678", 2, 0)]
        public void CompareGuessAndGoal_ShouldHandleLongerGuess(string guess, int expectedBullsCount, int expectedCowsCount)
        {
            AssertBullsAndCows(guess, expectedBullsCount, expectedCowsCount);
        }

        [TestMethod]
        [DataRow("1212", 2, 0)]
        [DataRow("2121", 0, 2)]
        public void CompareGuessAndGoal_ShouldHandleRepeatedCharactersInGuess(string guess, int expectedBullsCount, int expectedCowsCount)
        {
            AssertBullsAndCows(guess, expectedBullsCount, expectedCowsCount);
        }

        //[TestMethod]
        //[DataRow( "1234", true)]
        //[DataRow( "1234567", true)]
        //[DataRow( "4321", false)]
        //[DataRow( "", false)]
        //public void IsCorrectGuess_ShouldReturnExpectedResult(string guess, bool expectedResult)
        //{
        //    // Act
        //    var actualResult = _gameService.IsCorrectGuess(_goal, guess);

        //    // Assert
        //    Assert.AreEqual(expectedResult, actualResult, 
        //        $"Expected result of the comparison between goal: {_goal} and guess: {guess} was {expectedResult} but is {actualResult}.");
        //}
    }
}