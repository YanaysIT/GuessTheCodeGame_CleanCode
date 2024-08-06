using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Application;

public class GameController
{
    private readonly IGameService _gameService;
    private readonly IScoresRepository _scoresRepository;
    private readonly IUI _userInterface;

    public GameController(IGameService gameService, IScoresRepository scoresRepository, IUI userInterface)
    {
        _gameService = gameService;
        _scoresRepository = scoresRepository;
        _userInterface = userInterface;
    }

    public void Run()
    {
        var playerName = _userInterface.GetPlayerName();

        do
        {
            var numberOfGuesses = PlayRound(); 
            _scoresRepository.SavePlayerScore(new PlayerData(playerName, numberOfGuesses));
            _userInterface.DisplayLeaderBoard(_scoresRepository.GetLeaderboard());
        }
        while (_userInterface.ShouldContinuePlaying());
    }

    private int PlayRound()
    {
        var goal = _gameService.GenerateGoal();
        _userInterface.DisplayGameStartMessage(goal);
        var numberOfGuesses = GuessUntilGoalIsReached(goal);

        return numberOfGuesses;
    }

    private int GuessUntilGoalIsReached(string goal)
    {
        var numberOfGuesses = 0;
        var guess = string.Empty;

        do
        {
            guess = _userInterface.GetUserInput();
            var result = _gameService.CompareGuessAndGoal(goal, guess);
            _userInterface.DisplayMessage($"{result}\n");
            numberOfGuesses++;
        } while (goal != guess);

        _userInterface.DisplayWinningResult(numberOfGuesses);

        return numberOfGuesses;
    }
}