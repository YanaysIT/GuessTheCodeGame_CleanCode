using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Application;

internal class GameController
{
    private readonly IGameService _gameStrategy;
    private readonly IScoresRepository _scoresRepository;
    private readonly IUI _ui;

    public GameController(IGameService gameStrategy, IScoresRepository scoresRepository, IUI ui)
    {
        _gameStrategy = gameStrategy;
        _scoresRepository = scoresRepository;
        _ui = ui;
    }

    public void Run()
    {
        var isPracticeMode = true;
        var playerName = _ui.GetPlayerName();

        do
        {
            var numberOfGuesses = PlayRound(isPracticeMode);
            _scoresRepository.SavePlayerScore(playerName, numberOfGuesses);
            _ui.DisplayLeaderBoard(_scoresRepository.GetLeaderboard());
        }
        while (_ui.ShouldContinuePlaying());
    }

    private int PlayRound(bool isPracticeMode)
    {
        var goal = _gameStrategy.GenerateGoal();
        _ui.DisplayGameStartMessage(goal, isPracticeMode);
        var numberOfGuesses = GuessUntilGoalIsReached(goal);

        return numberOfGuesses;
    }

    private int GuessUntilGoalIsReached(string goal)
    {
        var guess = string.Empty;
        var numberOfGuesses = 0;

        while (guess != goal)
        {
            guess = _ui.GetUserInput();
            var result = _gameStrategy.CompareGuessAndGoal(guess, goal);
            _ui.DisplayMessage($"{result}\n");
            numberOfGuesses++;
        }

        _ui.DisplayMessage($"Correct, it took {numberOfGuesses} guesses\n");

        return numberOfGuesses;
    }
}