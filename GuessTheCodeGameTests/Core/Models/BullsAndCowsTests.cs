namespace GuessTheCodeGame.Core.Models.Tests
{
    [TestClass()]
    public class BullsAndCowsTests
    {
        [TestMethod]
        [DataRow(0, 4,",CCCC")]
        [DataRow(4, 0, "BBBB,")]
        [DataRow(1, 3, "B,CCC")]
        [DataRow(0, 0, ",")]
        public void ToString_ShouldReturnCorrectFormat(int bullsCount, int cowsCount, string expectedString)
        {
            // Arrange
            var bullsAndCows = new BullsAndCows(bullsCount, cowsCount);

            // Act
            var actualString = bullsAndCows.ToString();

            // Assert
            Assert.AreEqual(expectedString, actualString, $"The expexted string was {expectedString} but it's {actualString}.");
        }
    }
}