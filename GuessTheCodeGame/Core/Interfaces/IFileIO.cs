namespace GuessTheCodeGame.Core.Interfaces;

public interface IFileIO
{
    public IEnumerable<string> ReadLines(string filePath);
    public void WriteLine(string filePath, string line);
}
