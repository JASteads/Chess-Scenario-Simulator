using System.Collections.Generic;

public class King:Piece
{
    public bool HasMoved, IsChecked;

    public King(short location, short team) : base(location, team)
    {
        SetKind(5);
    }

    public override List<List<short>> CheckMoves()
    {
        return null;
    }

    public void TryCastle() { }
}