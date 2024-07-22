using GuessTheCodeGame.Interfaces;

namespace GuessTheCodeGame;

internal class ConsoleIO : IUI
{
    public string GetUserInput()
    {
        return Console.ReadLine() ?? "";
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayLeaderBoard(IEnumerable<PlayerData> leaderBoard)
    {
        DisplayMessage($"{"Player",-9}{"games",5}{"average",9}");
        foreach (var player in leaderBoard)
        {
            DisplayMessage($"{player.PlayerName,-9}{player.GamesPlayed,5}{player.CalculateAverageGuesses(),9:F2}");
        }
    }
}
