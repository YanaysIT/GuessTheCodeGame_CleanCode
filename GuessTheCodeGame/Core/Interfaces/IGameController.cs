namespace GuessTheCodeGame.Core.Interfaces;

public interface IGameController
{
    public void SetGameLogic(IGameLogic gameLogik);
    public void Play();
}
