using System.Collections.Generic;

public class Knight:Piece
{
    public Knight(short location, short team) : base(location, team)
    {
        SetKind(2);
    }

    public override List<short> CheckMoves()
    {
        return null;
    }
}