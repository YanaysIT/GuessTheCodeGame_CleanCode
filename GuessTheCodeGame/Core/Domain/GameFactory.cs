namespace GuessTheCodeGame.Core.Domain;

public class GameFactory
{
    public static IGameLogic CreateGame(GameTypes gameType)
    {
        return gameType switch
        {
            GameTypes.Moo => new MooGameLogic(new RandomNumberGenerator(), new GuessComparer()),
            GameTypes.MasterMind => new MasterMindLogic(new RandomNumberGenerator(), new GuessComparer()),
            GameTypes.Wordle => new WordleLogic(new RandomNumberGenerator()),
            _ => throw new ArgumentException($"Unsupported game type: {gameType}"),
        };
    }
}