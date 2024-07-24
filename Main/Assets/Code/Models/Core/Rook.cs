using System.Collections.Generic;

public class Rook:Piece
{
    public bool HasMoved;

    public Rook(short location, short team) : base(location, team)
    {
        SetKind(1);
    }

    public override List<short> CheckMoves()
    {
        return null;
    }

    public void TryCastle() { }
}