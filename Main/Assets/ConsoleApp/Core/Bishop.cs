using System.Collections.Generic;

public class Bishop:Piece
{
    public Bishop(short location, short team) : base(location, team)
    {
        SetKind(3);
    }

    public override List<List<short>> CheckMoves()
    {
        return null;
    }
}
    
