namespace GuessTheCodeGame.Application;

public class GameController : IGameController
{
    private readonly IPlayerScoresRepository _scoresRepository;
    private readonly IUI _ui;
    private IGameLogic _gameLogic;
    private string _playerName;
    
    public GameController(IPlayerScoresRepository scoresRepository, IUI userInterface)
    {
        _scoresRepository = scoresRepository;
        _ui = userInterface;
        _playerName = GetPlayerName();
    }

    public GameController(IPlayerScoresRepository scoresRepository, IUI userInterface, IGameLogic gameLogic) : this (scoresRepository, userInterface)
    {
        _gameLogic = gameLogic;
    }

    public void SetGameLogic(IGameLogic gameLogic)
    {
        _gameLogic = gameLogic ?? throw new ArgumentNullException(nameof(gameLogic));
    }

    private string GetPlayerName()
    {
        string playerName;
        do
        {
            playerName = _ui.GetPlayerName();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                _ui.ShowMessage("Player name cannot be empty. Please, try again.");
            }
        } while (string.IsNullOrWhiteSpace(playerName));

        return playerName;
    }

    public void Play()
    {
        do
        {
            _ui.Clear();
            _ui.ShowMessage($"Playing {_gameLogic.GameName}...");
            GenerateRoundGoal();
            int numberOfGuesses = GuessLoop();
            _ui.ShowRoundOutcome(numberOfGuesses);
            SavePlayerScore(numberOfGuesses);
            ShowLeaderboard();
        }
        while (_ui.ShouldContinuePlaying());
    }

    private void GenerateRoundGoal()
    {
        string goal = _gameLogic.GenerateGoal();
        _ui.ShowGameStartMessage(goal);
    }

    private int GuessLoop()
    {
        int numberOfGuesses = 0;
        do
        {
            string guess = _ui.GetUserInput()!;
            string guessFeedback = _gameLogic.CompareGuessAndGoal(guess);
            _ui.ShowMessage($"{guessFeedback}\n");
            numberOfGuesses ++;

        } while (!_gameLogic.IsCorrectGuess);

        return numberOfGuesses;
    }

    private void SavePlayerScore(int numberOfGuesses)
    {
        IPlayer player = new Player(_playerName, numberOfGuesses);
        _scoresRepository.SavePlayerScore(player, _gameLogic.GameName);
    }

    private void ShowLeaderboard()
    {
        IEnumerable<IPlayer> leaderboard = _scoresRepository.GetLeaderboard(_gameLogic.GameName);
        _ui.ShowLeaderBoard(leaderboard);
    }
}