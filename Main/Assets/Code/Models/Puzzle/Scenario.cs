using System.Collections.Generic;

public class Scenario
{
    public int Rating;
    public List<Piece> StartingPieces;
    public List<short> CorrectMoves;
    public List<short> CounterMoves;

    public Scenario()
    {
        StartingPieces = new List<Piece>();
        CorrectMoves = new List<short>();
        CounterMoves = new List<short>();
    }
}