public class King:Piece
{
     public bool HasMoved;
     public bool IsChecked;

    public King(ushort location, ushort team) : base(location, team)
    {
        SetType(5);
    }
    public override bool CheckMoves()
    {
        return true;
    }
    public void TryCastle() { }

}