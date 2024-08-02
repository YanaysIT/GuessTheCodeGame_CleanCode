using GuessTheCodeGame.Application;
using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Models;
using GuessTheCodeGame.Data;
using GuessTheCodeGame.UI;

namespace GuessTheCodeGame;

public class Program
{
	public static void Main(string[] args)
	{
        IUI ui = new ConsoleIO();
        IGoalGenerator<string> mooGoalGenerator = new MooGoalGenerator(4, 9);
        IGameService<string> mooGameService = new MooGameService(mooGoalGenerator);
        IScoresRepository scoresRepository = new ScoresRepository("results.txt");
        var gameController = new GameController(mooGameService, scoresRepository, ui);
		
        gameController.Run();		
	}
}