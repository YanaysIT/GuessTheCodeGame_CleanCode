using GuessTheCodeGame.Core.Interfaces;

namespace GuessTheCodeGame.Data;

public class StreamFileIO : IFileIO
{
    public IEnumerable<string> ReadLines(string filePath)
    {
        using var reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            yield return line!; 
        }
    }

    public void WriteLine(string filePath, string line)
    {
       using var writer = new StreamWriter(filePath, append: true);
        writer.WriteLine(line);
    }
}
