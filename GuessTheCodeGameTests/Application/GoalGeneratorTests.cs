namespace GuessTheCodeGame.Application.Tests;

[TestClass()]
public class GoalGeneratorTests
{ 

    [TestMethod()]
    [DataRow(4, 6)]
    [DataRow(6, 8)]
    [DataRow(8, 9)]
    public void GoalGenerator_ShouldReturnGoalOfSpecifiedLength(int expectedGoalLength, int maxDigitValue)
    {
        //Arrange
        var goalGenerator = new MooGoalGenerator(expectedGoalLength, maxDigitValue);

        //Act
        var goal = goalGenerator.GenerateGoal();
        var actualGoalLength = goal.Length;
        
        //Assert
        Assert.AreEqual(expectedGoalLength, actualGoalLength, $"Goal length should be {expectedGoalLength}, but it's {actualGoalLength}");
    }

    [TestMethod()]
    public void GoalGenerator_ShouldReturnGoalWithDigitsWithinRange()
    {
        //Arrange
        var goalLength = 10;
        var expectedMaxDigitValue = 9;
        var goalGenerator = new MooGoalGenerator(goalLength, expectedMaxDigitValue);

        //Act
        var goal = goalGenerator.GenerateGoal();

        //Assert
        foreach (var d in goal) 
        {
            var digit = int.Parse(d.ToString());
            Assert.IsTrue(digit <= expectedMaxDigitValue, $"Digit {digit} is out of range [0:{expectedMaxDigitValue}].");
        }
    }

    [TestMethod()]
    [DataRow(4, 7)]
    [DataRow(5, 5)]
    [DataRow(10, 9)]
    public void GoalGenerator_ShouldReturnUniqueGoalDigits(int goalLength, int maxDigitValue)
    {
        //Arrange
        var goalGenerator = new MooGoalGenerator(goalLength, maxDigitValue);

        //Act
        var goal = goalGenerator.GenerateGoal();
        
        var actualNumberOfUniqueDigits = goal.Distinct().Count();
        var expectedNumberOfUniqueDigits = goalLength;

        //Assert
        Assert.AreEqual(actualNumberOfUniqueDigits, expectedNumberOfUniqueDigits, "Goal contains duplicate items.");   
    }
}