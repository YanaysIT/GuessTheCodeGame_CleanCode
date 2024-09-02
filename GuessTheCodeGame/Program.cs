using GuessTheCodeGame.Application;
using GuessTheCodeGame.Infrastructure;
using GuessTheCodeGame.UI;

namespace GuessTheCodeGame;

public class Program
{
	public static void Main(string[] args)
	{

        IUI userInterface = new ConsoleIO();
        IPlayerScoresRepository scoresRepository = new PlayerScoresRepository("results.txt",new FileStreamIO());
        IGameController gameController = new GameController(scoresRepository, userInterface);
        IGameMenuService gameMenu = new GameMenuService(userInterface, gameController);
        gameMenu.SelectGame();

        //The controller can also be used without menu, and the strategy setted via constuctor injection or SetGameLogic method
        //IGameLogic moo = GameFactory.CreateGame(GameTypes.Moo);
        //gameController.SetGameLogic(moo);
        //gameController.Play();
    }
}