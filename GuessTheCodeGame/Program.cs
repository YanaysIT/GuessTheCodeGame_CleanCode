using GuessTheCodeGame.Application;
using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Data;
using GuessTheCodeGame.UI;

namespace GuessTheCodeGame;

public class Program
{

	public static void Main(string[] args)
	{
        IGoalGenerator mooGoalGenerator = new GoalGenerator(4, 9);
        IGameService mooGameService = new MooGameService(mooGoalGenerator);
        IScoresRepository scoresRepository = new ScoresRepository("results.txt");
        IUI ui = new ConsoleIO();
        var gameController = new GameController(mooGameService, scoresRepository, ui);
		
        gameController.Run();		
	}
}