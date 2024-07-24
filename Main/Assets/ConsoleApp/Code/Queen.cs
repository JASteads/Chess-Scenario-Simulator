public class Queen:Piece
{
    public Queen(ushort location, ushort team) : base(location, team)
    {
        SetType(4);
    }
    public override bool CheckMoves()
    {
        return true;
    }

}