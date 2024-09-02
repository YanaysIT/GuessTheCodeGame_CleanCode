namespace GuessTheCodeGame.Application;

public class GameMenuService : IGameMenuService
{
    private readonly IUI _ui;
    private readonly IGameController _gameController;

    public GameMenuService(IUI userInterface, IGameController gameController)
    {
        _ui = userInterface;
        _gameController = gameController;
    }

    public void SelectGame()
    {
        while (true)
        {
            ShowGameMenu();

            string userInput = _ui.GetUserInput()?.Trim().ToLower() ?? "";

            CheckForQuit(userInput);
            GameTypes selectedGameType = ParseToGameType(userInput);
            LaunchGame(selectedGameType);
        }
    }

    private void ShowGameMenu()
    {
        _ui.Clear();
        _ui.ShowMessage("Select an option from the list:\n");

        foreach (var gameValue in Enum.GetValues<GameTypes>())
        {
            if (gameValue != GameTypes.None)
            {
                _ui.ShowMessage($"[{(int)gameValue}]: {gameValue}");
            }
        }
        _ui.ShowMessage($"[q]: Quit\n");
    }

    private void CheckForQuit(string userInput)
    {
        if (userInput.StartsWith('q'))
        {
            _ui.Exit();
        }
    }

    private GameTypes ParseToGameType(string userInput)
    {
        bool isValidGameType = int.TryParse(userInput, out int gameValue) && Enum.IsDefined(typeof(GameTypes), gameValue);

        return isValidGameType ? (GameTypes)gameValue : GameTypes.None;
    }

    private void LaunchGame(GameTypes selectedGameType)
    {
        if (selectedGameType != GameTypes.None)
        {
            IGameLogic gameLogic = GameFactory.CreateGame(selectedGameType);
            _gameController.SetGameLogic(gameLogic);
            _gameController.Play();
        }
        else
        {
            _ui.ShowMessage("Invalid selection. Please try again.\n");
        }
    }
}