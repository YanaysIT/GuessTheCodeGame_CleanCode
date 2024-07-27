using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.UI;

internal class ConsoleIO : IUI
{
    public string GetUserInput()
    {
        return Console.ReadLine()?.Trim() ?? "";
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string GetPlayerName()
    {
        string playerName;
        DisplayMessage("Enter your user name:\n");

        do
        {
            playerName = GetUserInput();

            if (playerName.Equals(string.Empty))
            {
                DisplayMessage("The player name cannot be empty. Please, enter a name: ");
            }
        } while (playerName.Equals(string.Empty));

        return playerName;
    }

    public void DisplayGameStartMessage(string goal, bool isPracticeMode = true)
    {
        DisplayMessage("\nNew game:\n");

        if (isPracticeMode)
        {
            DisplayMessage($"For practice, number is: {goal}\n");
        }
    }

    public bool ShouldContinuePlaying()
    {
        DisplayMessage("\nContinue? Yes(any key) / No(n):\n");
        string userInput = GetUserInput().ToLower();

        return userInput != "n";
    }

    public void DisplayLeaderBoard(IEnumerable<IPlayerData> leaderBoard)
    {
        DisplayMessage($"{"Player",-9}{"games",5}{"average",9}");
        foreach (var player in leaderBoard)
        {
            DisplayMessage($"{player.PlayerName,-9}{player.GamesPlayed,5}{player.CalculateAverageGuesses(),9:F2}");
        }
    }
}
