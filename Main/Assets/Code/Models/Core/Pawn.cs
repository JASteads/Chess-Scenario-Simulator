public class Pawn:Piece
{
    public Pawn(ushort location, ushort team) : base(location, team)
    {
        SetType(0);
    }
    public override bool CheckMoves()
    {
        return true;
    }
    public void Promote() { }
    public void CanEnPassant() { }
}