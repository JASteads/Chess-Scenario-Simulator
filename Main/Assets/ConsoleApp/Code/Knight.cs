public class Knight:Piece
{
    public Knight(ushort location, ushort team) : base(location, team)
    {
        SetType(2);
    }
    public override bool CheckMoves()
    {
        return true;
    }

}