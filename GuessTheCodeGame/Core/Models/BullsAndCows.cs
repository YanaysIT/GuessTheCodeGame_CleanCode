namespace GuessTheCodeGame.Core.Models;

internal class BullsAndCows
{
    public int BullsCount { get; private set; }
    public int CowsCount { get; private set; }

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
