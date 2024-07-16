public abstract class Piece
{
    // public Graphic image;
    public short information;

    public abstract bool CheckMoves()
    {
        return true;
    }


    public void Move()
    {

    }

    public void OnCapture()
    {

    }

public class King:Piece
{
    public bool HasMoved;
    public bool IsChecked;

    public void TryCastle() { }

}

public class Rook:Piece
{
    public bool HasMoved;
    public bool IsLeft;

    public void TryCastle() { }
}

public class Pawn:Piece
{
    public void Promote() { }
    public void CanEnPassant() { }
}

}