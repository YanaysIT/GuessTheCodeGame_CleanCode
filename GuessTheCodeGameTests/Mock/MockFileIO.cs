using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGameTests.Mock;

public class MockFileIO : IFileIO
{
    private readonly Queue<string> _linesToRead;
    private readonly List<string> _linesWritten;
    public List<string> LinesWritten => _linesWritten;

    public MockFileIO(IEnumerable<string> linesToRead)
    {
        _linesToRead = new Queue<string>(linesToRead);
        _linesWritten = new List<string>();
    }

    public IEnumerable<string> ReadLines(string filePath)
    {
        while (_linesToRead.Count > 0) 
        {
            yield return _linesToRead.Dequeue();
        }
    }

    public void WriteLine(string filePath, string line)
    {
        _linesWritten.Add(line);
    }
}
