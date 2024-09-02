namespace GuessTheCodeGame.Core.Domain.Tests
{
    [TestClass()]
    public class MooGameTests
    {
        private const int ExpectedGoalLength = 4;
        private const string ExpectedGoal = "1234";
        private string _actualGoal;
        private MooGameLogic _mooGame;
        private MockRandomNumberGenerator _mockRandomGenerator;

        [TestInitialize]
        public void Initialize()
        {
            var predefinedRandomNumbers = new List<int> { 1, 1, 2, 2, 3, 1, 4 };
            _mockRandomGenerator = new MockRandomNumberGenerator(predefinedRandomNumbers);
            _mooGame = new MooGameLogic(_mockRandomGenerator, new GuessComparer());
            _actualGoal = _mooGame.GenerateGoal();
        }

        [TestMethod]
        public void GenerateGoal_ShouldReturnUniqueDigitsGoal()
        {
            int expectedNumberOfUniqueDigits = ExpectedGoalLength;

            int actualNumberOfUniqueDigits = _actualGoal.Distinct().Count();

            Assert.AreEqual(expectedNumberOfUniqueDigits, actualNumberOfUniqueDigits, $"The goal {_actualGoal} contains duplicate items.");
        }

        [TestMethod]
        public void GenerateGoal_ShouldReturnExpectedGoalLength()
        {
            int actualGoalLength = _actualGoal.Length;

            Assert.AreEqual(ExpectedGoalLength, actualGoalLength, $"The goal length should be {ExpectedGoalLength}, but got {actualGoalLength}");
        }

        [TestMethod]
        public void GenerateGoal_ShouldReturnCorrectGoal()
        {
            Assert.AreEqual(ExpectedGoal, _actualGoal, $"The goal length should be {ExpectedGoal}, but got {_actualGoal}");
        }

        [TestMethod]
        [DataRow("3412", ",CCCC")]
        [DataRow("1234", "BBBB,")]
        [DataRow("1342", "B,CCC")]
        [DataRow("5678", ",")]
        public void CompareGuessAndGoal_ShouldReturnCorrectBullsAndCows(string guess, string expectedGuessFeedback)
        {
            var actualGuessFeedback = _mooGame.CompareGuessAndGoal(guess);

            Assert.AreEqual(expectedGuessFeedback, actualGuessFeedback, $"The expected guess feedback was {expectedGuessFeedback}, but got {actualGuessFeedback}.");
        }

        [TestMethod]
        [DataRow("34", ",CC")]
        [DataRow("1234567", "BBBB,")]
        [DataRow("56781234", ",")]
        [DataRow("", ",")]

        public void CompareGuessAnGoal_ShouldHandleIncorrectGuessFormat(string guess, string expectedGuessFeedback)
        {
            var actualGuessFeedback = _mooGame.CompareGuessAndGoal(guess);

            Assert.AreEqual(expectedGuessFeedback, actualGuessFeedback, $"The expected guess feedback was {expectedGuessFeedback}, but got {actualGuessFeedback}.");
        }
    }
}