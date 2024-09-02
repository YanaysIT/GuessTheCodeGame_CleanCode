using GuessTheCodeGame.Core.Utilities;

namespace GuessTheCodeGame.UI;

public class ConsoleIO : IUI
{
    public string? GetUserInput()
    {
        return Console.ReadLine();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public string? GetPlayerName()
    {
        Console.WriteLine("Enter your user name:\n");

        return Console.ReadLine();
    }

    public void ShowGameStartMessage(string goal)
    {
        Console.WriteLine("\nNew game:");

        //comment out or remove next line to play real games!
        Console.WriteLine($"For practice, number is: {goal}\n");
    }

    public void ShowRoundOutcome(int numberOfGuesses)
    {
        Console.WriteLine($"Correct, it took {numberOfGuesses} guesses\n");
    }
        
    public bool ShouldContinuePlaying()
    {
        Console.WriteLine($"Continue? (y/n) \n");
        string? wantsToContinue = Console.ReadLine()?.ToLower();

        return wantsToContinue?.FirstOrDefault() != 'n' ;
    }

    public void ShowLeaderBoard(IEnumerable<IPlayer> leaderBoard)
    {
        string formattedLeaderboard = leaderBoard.GetFormattedLeaderboard();
        Console.WriteLine(formattedLeaderboard);
    }

    public void Clear()
    { 
        Console.Clear();    
    }

    public void Exit()
    {
        Environment.Exit(0);
    }
}