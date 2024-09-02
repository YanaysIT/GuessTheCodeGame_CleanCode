using System.Text;

namespace GuessTheCodeGame.Core.Utilities;

public static class LeaderboardFormatter
{
    public static string GetFormattedLeaderboard(this IEnumerable<IPlayer> leaderBoard)
    {
        string headerFormat = "{0,-9}{1,5}{2,9}";
        string scoresFormat = "{0,-9}{1,5}{2,9:F2}";
        string[] leaderboardHeader = ["Player", "Games", "Average"];

        var leaderboardString = new StringBuilder();

        leaderboardString.AppendLine(string.Format(headerFormat, leaderboardHeader));

        foreach (var player in leaderBoard)
        {
            leaderboardString.AppendLine(string.Format(scoresFormat, player.PlayerName, player.GamesPlayed, player.CalculateAverageGuesses()));
        }

        return leaderboardString.ToString();
    }
}
