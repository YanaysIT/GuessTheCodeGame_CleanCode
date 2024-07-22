using GuessTheCodeGame.Interfaces;

namespace GuessTheCodeGame;

public class Program
{

	public static void Main(string[] args)
	{
        IGoalHandler goalHandler = new MooGoalHandler();
        IUI userInerface = new ConsoleIO();
        IScoresRepository scoresRepository = new ScoresRepository();
        var gameController = new GameController(goalHandler, userInerface, scoresRepository);
		gameController.Run();		
	}
}