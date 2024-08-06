using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Core.Utilities;

public class RandomNumberGenerator : Random, IRandomNumberGenerator
{
    public RandomNumberGenerator() : base(){ }
    //Created only for testing reasons
}
