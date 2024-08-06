using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGameTests.Mock;

public class MockRandomNumberGenerator : IRandomNumberGenerator
{
    private readonly Queue<int> _predefinedRandomNumbers;

    public MockRandomNumberGenerator(IEnumerable<int> predefinedRandomNumbers)
    {
        _predefinedRandomNumbers = new Queue<int>(predefinedRandomNumbers);
    }

    public int Next(int maxValue)
    {
        return _predefinedRandomNumbers.Count > 0 ? _predefinedRandomNumbers.Dequeue() : maxValue - 1;
    }
}