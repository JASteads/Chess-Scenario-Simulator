public class Rook:Piece
{
    public bool HasMoved;
    public bool IsLeft;

    public Rook(ushort location, ushort team) : base(location, team)
    {
        SetType(1);
    }
    public override bool CheckMoves()
    {
        return true;
    }
  
    public void TryCastle() { }
}