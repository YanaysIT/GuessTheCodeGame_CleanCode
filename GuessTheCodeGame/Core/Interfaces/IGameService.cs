using GuessTheCodeGame.Core.Models;

namespace GuessTheCodeGame.Core.Interfaces;

public interface IGameService<T>
{
    public T GenerateGoal();
    public BullsAndCows CompareGuessAndGoal(T guess, T goal);

}
