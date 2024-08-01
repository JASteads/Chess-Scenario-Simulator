using System.Collections.Generic;

public class Queen:Piece
{
    public Queen(short location, short team) : base(location, team)
    {
        SetKind(4);
    }

    public override List<List<short>> CheckMoves()
    {
        return null;
    }

}