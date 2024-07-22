using GuessTheCodeGame.Interfaces;

namespace GuessTheCodeGame;

internal class GameController
{
    private readonly IGoalHandler _goalHandler;
    private readonly IUI _userInterface;
    private readonly IScoresRepository _scoresRepository;
    
    public GameController(IGoalHandler goalHandler, IUI userInterface, IScoresRepository scoresRepository)
    {
        _goalHandler = goalHandler;
        _userInterface = userInterface;
        _scoresRepository = scoresRepository;
    }


    public void Run()
    {
        var playerName = GetPlayerName();

        do
        {
            var numberOfGuesses = RunNewGame();
            SavePlayerScore(playerName, numberOfGuesses);
            DisplayLeaderboard();
        }
        while (IsContinuePlaying());
    }

    private string GetPlayerName()
    {
        var playerName = string.Empty;

        _userInterface.DisplayMessage("Enter your user playerName:\n");
        while (playerName == string.Empty)
        {
            playerName = _userInterface.GetUserInput()?.Trim() ?? "";
            if (playerName == string.Empty)
            {
                _userInterface.DisplayMessage($"\nThe player name cannot be empty. Please, enter a name: ");
            }
        }
        return playerName;
    }

    private bool IsContinuePlaying()
    {
        _userInterface.DisplayMessage($"\nContinue? yes/no: (any key)/n\n");
        string? input = _userInterface.GetUserInput()?.Trim().ToLower();
        return input != "n";
    }

    private int RunNewGame()
    {
        var goal = _goalHandler.GenerateGoal();

        _userInterface.DisplayMessage("New game:\n");
        //comment out or remove next line to play real games!
        _userInterface.DisplayMessage($"For practice, number is: {goal}\n");
        var numberOfGuesses = GuessUntilGoalIsReached(goal);
        _userInterface.DisplayMessage($"Correct, it took {numberOfGuesses} guesses\n");
        return numberOfGuesses;
    }

    private int GuessUntilGoalIsReached(string goal)
    {
        var guess = string.Empty;
        var numberOfGuesses = 0;

        while (guess != goal)
        {
            guess = _userInterface.GetUserInput();
            _userInterface.DisplayMessage($"{guess}\n");
            var result = _goalHandler.CompareGuessAndGoal(guess, goal);
            _userInterface.DisplayMessage($"{result}\n");
            numberOfGuesses++;
        }
        return numberOfGuesses;
    }

    private void SavePlayerScore(string playerName, int numberOfGuesses)
    {
        try
        {
            _scoresRepository.SavePlayerScore(playerName, numberOfGuesses);
        }
        catch (Exception ex)
        {
            _userInterface.DisplayMessage($"Failed to save your results:\n{ex.Message}");
        }
    }

    private void DisplayLeaderboard()
    {
        var leaderboard = _scoresRepository.GetLeaderboard();
        _userInterface.DisplayLeaderBoard(leaderboard);
    }

}

