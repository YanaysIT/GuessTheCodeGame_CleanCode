namespace GuessTheCodeGame.Core.Domain
{
    public class MooGameLogic : IGameLogic
    {
        public GameTypes GameName => GameTypes.Moo;
        public bool IsCorrectGuess => _guessFeedback.ExactMatches == GoalLength;

        private const int GoalLength = 4;
        private const int MaxDigitValue = 9;

        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IGuessComparer _guessComparer;

        private string _goal;
        private GuessFeedback _guessFeedback;

        public MooGameLogic(IRandomNumberGenerator randomNumberGenerator, IGuessComparer guessComparer)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _guessComparer = guessComparer;
        }

        public string GenerateGoal()
        {
            HashSet<int> goal = new();

            while (goal.Count < GoalLength)
            {
                int randomDigit = _randomNumberGenerator.Next(MaxDigitValue + 1);
                goal.Add(randomDigit);           
            }

            return _goal = string.Join("", goal);
        }

        public string CompareGuessAndGoal(string guess)
        {
            guess = (guess ?? "").PadRight(GoalLength);
            _guessFeedback = _guessComparer.CompareGuessAndGoal(guess, _goal);

            return $"{new string('B', _guessFeedback.ExactMatches)},{new string('C', _guessFeedback.PartialMatches)}";
        }
    }
}