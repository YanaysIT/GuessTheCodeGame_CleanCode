using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Core.Models;

public class BullsAndCows
{
    public int BullsCount { get; }
    public int CowsCount { get; }

    public BullsAndCows(int bullsCount, int cowsCount)
    {
        BullsCount = bullsCount;
        CowsCount = cowsCount;        
    }
    
    public override string ToString()
    {
        return $"{new string('B', BullsCount)},{new string('C', CowsCount)}";
    }
}
