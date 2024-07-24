using System.Collections.Generic;

public class Pawn:Piece
{
    public Pawn(short location, short team) : base(location, team)
    {
        SetKind(0);
    }

    public override List<short> CheckMoves()
    {
        return null;
    }

    public void Promote() { }
    public void CanEnPassant() { }
}