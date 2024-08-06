﻿using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.UI;

public class ConsoleIO : IUI
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

    public void DisplayWinningResult(int numberOfGuesses)
    {
        DisplayMessage($"Correct, it took {numberOfGuesses} guesses\n");
    }
        
    public bool ShouldContinuePlaying()
    {
        DisplayMessage("\nContinue? Yes(any key) / No(n):\n");
        string userInput = GetUserInput().ToLower();

        return userInput != "n";
    }

    public void DisplayLeaderBoard(IEnumerable<IPlayerData> leaderBoard)
    {
        var playerDataFormat = "{0,-9}{1,5}{2,9:F2}";
        DisplayMessage(String.Format(playerDataFormat,"Player", "Games", "Average"));
        foreach (var player in leaderBoard)
        {
            DisplayMessage(String.Format(playerDataFormat, player.PlayerName, player.GamesPlayed, player.CalculateAverageGuesses()));
        }
    }
}
