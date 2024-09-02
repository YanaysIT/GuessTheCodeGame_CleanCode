namespace GuessTheCodeGame.Core.Models;

public class GuessFeedback
{
    public int ExactMatches {  get; set; }
    public int PartialMatches {  get; set; }

    public GuessFeedback(int exactMatches, int partialMatches)
    {
        ExactMatches = exactMatches;
        PartialMatches = partialMatches;
    }
}