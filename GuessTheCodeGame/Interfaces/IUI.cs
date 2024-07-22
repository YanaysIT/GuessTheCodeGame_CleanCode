using System.Numerics;

namespace GuessTheCodeGame.Interfaces;

internal interface IUI
{
    string GetUserInput();
    void DisplayMessage(string message);  
    void DisplayLeaderBoard(IEnumerable<PlayerData> leaderBoard);
}
