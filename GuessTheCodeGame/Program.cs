using GuessTheCodeGame.Application;
using GuessTheCodeGame.Core.Interfaces;
using GuessTheCodeGame.Core.Utilities;
using GuessTheCodeGame.Data;
using GuessTheCodeGame.UI;

namespace GuessTheCodeGame;

public class Program
{
	public static void Main(string[] args)
	{
        IRandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
        IGoalGenerator mooGoalGenerator = new MooGoalGenerator(randomNumberGenerator);
        IGameService mooGameService = new MooGameService(mooGoalGenerator);
        IUI userInterface = new ConsoleIO();
        IFileIO streamFileIO = new StreamFileIO();
        IScoresRepository scoresRepository = new ScoresRepository(scoresFilePath: "results.txt", streamFileIO);
        var gameController = new GameController(mooGameService, scoresRepository, userInterface);
		
        gameController.Run();		
	}
}