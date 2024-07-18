public class Knight:Piece
{
    public Bishop(ushort location, ushort team) : base(location, team)
    {
        SetType(2);
    }
    public override bool CheckMoves()
    {
        return true;
    }

}