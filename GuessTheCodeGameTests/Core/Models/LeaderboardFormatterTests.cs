using GuessTheCodeGame.Core.Utilities;

namespace GuessTheCodeGame.Core.Models.Tests;

[TestClass()]
public class LeaderboardFormatterTests
{
    [TestMethod()]
    public void GetFormattedLeaderboard_ShoulReturnLeaderboardInACorrectFormat()
    {
        List<Player> leaderboard = new();

        leaderboard.Add(new Player("Lola", 5));
        leaderboard.Add(new Player("Jose", 10));

        var actualFormattedLeaderboard = LeaderboardFormatter.GetFormattedLeaderboard(leaderboard);

        string expectedFormattedLeaderboard = "Player   Games  Average\r\n" +
                                              "Lola         1     5,00\r\n" +
                                              "Jose         1    10,00\r\n";
        Assert.AreEqual(expectedFormattedLeaderboard, actualFormattedLeaderboard, 
            $"Expected formatted leaderboard was \n{expectedFormattedLeaderboard}, but got \n{actualFormattedLeaderboard}.");
    }
}