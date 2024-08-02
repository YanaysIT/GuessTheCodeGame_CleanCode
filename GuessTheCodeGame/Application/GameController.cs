using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Application;

public class GameController
{
    private readonly IGameService<string> _gameService;
    private readonly IScoresRepository _scoresRepository;
    private readonly IUI _ui;

    public GameController(IGameService<string> gameService, IScoresRepository scoresRepository, IUI ui)
    {
        _gameService = gameService;
        _scoresRepository = scoresRepository;
        _ui = ui;
    }

    public void Run()
    {
        var playerName = _ui.GetPlayerName();

        do
        {
            var numberOfGuesses = PlayRound();
            _scoresRepository.SavePlayerScore(playerName, numberOfGuesses);
            _ui.DisplayLeaderBoard(_scoresRepository.GetLeaderboard());
        }
        while (_ui.ShouldContinuePlaying());
    }

    private int PlayRound()
    {
        var goal = _gameService.GenerateGoal();
        _ui.DisplayGameStartMessage(goal.ToString());
        var numberOfGuesses = GuessUntilGoalIsReached(goal);

        return numberOfGuesses;
    }

    private int GuessUntilGoalIsReached(string goal)
    {
        string guess = string.Empty;
        var numberOfGuesses = 0;

        while (!guess.Equals(goal))
        {
            guess = _ui.GetUserInput();
            var result = _gameService.CompareGuessAndGoal(guess, goal);
            _ui.DisplayMessage($"{result}\n");
            numberOfGuesses++;
        }

        _ui.DisplayWinningResult(numberOfGuesses);

        return numberOfGuesses;
    }
}