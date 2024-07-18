public class Bishop:Piece
{
    public Bishop(ushort location, ushort team) : base(location, team)
    {
        SetType(3);
    }
    public override bool CheckMoves()
    {
        return true;
    }

}
    
