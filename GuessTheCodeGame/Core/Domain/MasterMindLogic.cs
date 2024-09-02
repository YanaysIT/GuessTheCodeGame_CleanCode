namespace GuessTheCodeGame.Core.Domain
{
    public class MasterMindLogic : IGameLogic
    {
        public GameTypes GameName => GameTypes.MasterMind;
        public bool IsCorrectGuess => _guessFeedback.ExactMatches == GoalLength;

        private const int GoalLength = 4;
        private const int MaxDigitValue = 5;

        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IGuessComparer _guessComparer;

        private string _goal;
        private GuessFeedback _guessFeedback;

        public MasterMindLogic(IRandomNumberGenerator randomNumberGenerator, IGuessComparer guessComparer)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _guessComparer = guessComparer;
        }

        public string GenerateGoal()
        {
            List<int> goal = new();

            while (goal.Count < GoalLength)
            {
                int randomDigit = _randomNumberGenerator.Next(MaxDigitValue + 1);
                goal.Add(randomDigit);           
            }

            return _goal = string.Join("", goal);
        }

        public  string CompareGuessAndGoal(string guess)
        {
            guess = (guess ?? "").PadRight(GoalLength);
            _guessFeedback = _guessComparer.CompareGuessAndGoal(guess, _goal);

            return $"{_guessFeedback.ExactMatches}B{_guessFeedback.PartialMatches}W";
        }
    }
}