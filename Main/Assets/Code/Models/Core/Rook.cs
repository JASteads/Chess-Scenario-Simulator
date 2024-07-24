public class Rook:Piece
{
    public Rook(ushort location, ushort team) : base(location, team)
    {
        SetType(1);
    }
    public override bool CheckMoves()
    {
        return true;
    }
    public bool HasMoved;
    public bool IsLeft;

    public void TryCastle() { }
}