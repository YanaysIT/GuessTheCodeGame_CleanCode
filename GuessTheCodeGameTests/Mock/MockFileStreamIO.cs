using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGameTests.Mock;

public class MockFileStreamIO : IFileStreamIO
{
    private readonly List<string> _fileContent;
    public List<string> LinesWritten { get; } = new();

    public MockFileStreamIO(List<string> initialContent)
    {
        _fileContent = initialContent ?? new ();
    }

    public IEnumerable<string> ReadLines(string filePath)
    {
        foreach (var line in _fileContent)
        {
            yield return line;
        }
    }

    public void WriteLine(string filePath, string line)
    {
        LinesWritten.Add(line);
        _fileContent.Add(line);
    }
}
