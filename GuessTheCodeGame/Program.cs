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
        gameMenu.Run();
    }
}