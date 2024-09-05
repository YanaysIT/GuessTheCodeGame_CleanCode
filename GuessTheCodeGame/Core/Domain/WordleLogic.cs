namespace GuessTheCodeGame.Core.Domain
{
    public class WordleLogic : IGameLogic
    {
        public GameTypes GameName => GameTypes.Wordle;
        public bool IsCorrectGuess => _guessFeedback == _goal;
        
        private const int GoalLength = 5;

        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly List<string> _words =
        [
            "apple", "grape", "lemon", "mango", "peach", "berry", "melon", "plumb", "olive", "bread", "honey", "sugar", "spice", "water", "juice", "cream", "bacon", "pasta", "fruit", 
            "salad", "sauce", "pizza", "tiger", "eagle", "shark", "horse", "zebra", "snake", "whale", "panda", "sheep", "zebra", "frost", "storm", "flame", "blaze", "stone", "earth", 
            "mount", "river", "beach", "ocean", "space", "night", "light", "sound", "color", "watch", "clock", "glass", "blade", "sword", "scale", "smile", "teeth", "heart", "brain"
        ];

        private string _goal;
        private string _guessFeedback;
        
        public WordleLogic(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string GenerateGoal()
        {
            int wordRandomIndex = _randomNumberGenerator.Next(_words.Count);

            return _goal = _words[wordRandomIndex];
        }

        public string CompareGuessAndGoal(string guess)
        {
            guess = (guess ?? "").PadRight(GoalLength);

            char[] feedback = new char[_goal.Length];

            for (int i = 0; i < _goal.Length; i++)
            {
                if (guess[i] == _goal[i])
                {
                    feedback[i] = guess[i];
                }
                else
                {
                    feedback[i] = '.';
                }
            }

            return _guessFeedback = new string(feedback);
        }
    }
}