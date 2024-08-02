namespace GuessTheCodeGame.Core.Models.Tests
{
    [TestClass()]
    public class BullsAndCowsTests
    {
        [TestMethod]
        public void ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var bullsAndCows = new BullsAndCows(1, 3);
            string expectedString = "B,CCC";

            // Act
            var actualString = bullsAndCows.ToString();

            // Assert
            Assert.AreEqual(expectedString, actualString, $"The expexted string was {expectedString} but it's {actualString}.");
        }
    }
}