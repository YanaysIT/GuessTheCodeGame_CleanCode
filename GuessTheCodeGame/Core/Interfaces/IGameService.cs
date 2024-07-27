using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Core.Interfaces;

internal interface IGameService
{
    public string GenerateGoal();
    public BullsAndCows CompareGuessAndGoal(string guess, string goal);
}
