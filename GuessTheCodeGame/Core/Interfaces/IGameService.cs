using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Core.Interfaces;

public interface IGameService
{
    public string GenerateGoal();
    public BullsAndCows CompareGuessAndGoal(string goal, string gues);
}
