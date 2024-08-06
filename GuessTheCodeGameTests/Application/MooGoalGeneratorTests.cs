using GuessTheCodeGameTests.Mock;

namespace GuessTheCodeGame.Application.Tests;

[TestClass]
public class MooGoalGeneratorTests
{
    private readonly int _expectedGoalLength = 4;
   
    [TestMethod]
    [DataRow(new[] { 1, 2, 3, 4, 5, 6, 7, 2, 1 })]
    [DataRow(new[] { 5, 6, 7, 8 , 9 , 8})]
    public void GoalGenerator_ShouldReturnGoalOfCorrectLength(int[] predefinedNumbers)
    {
        // Arrange
        var mockRandomGenerator = new MockRandomNumberGenerator(predefinedNumbers);
        var goalGenerator = new MooGoalGenerator(mockRandomGenerator);

        // Act
        var goal = goalGenerator.GenerateGoal();
        var actualGoalLength = goal.Length;

        // Assert
        Assert.AreEqual(_expectedGoalLength, actualGoalLength, $"Goal length should be {_expectedGoalLength}, but it's {actualGoalLength}");
    }

    [TestMethod]
    [DataRow(new[] { 1, 2, 3, 4 })]
    [DataRow(new[] { 1, 1, 2, 2, 3, 1, 4 })]
    public void GoalGenerator_ShouldReturnUniqueGoalDigits(int[] predefinedNumbers)
    {
        // Arrange
        var mockRandomGenerator = new MockRandomNumberGenerator(predefinedNumbers);
        var goalGenerator = new MooGoalGenerator(mockRandomGenerator);
        var expectedNumberOfUniqueDigits = _expectedGoalLength;

        // Act
        var goal = goalGenerator.GenerateGoal();
        var actualNumberOfUniqueDigits = goal.Distinct().Count();

        // Assert
        Assert.AreEqual(expectedNumberOfUniqueDigits, actualNumberOfUniqueDigits, $"Goal {goal} contains duplicate items.");
    }
}