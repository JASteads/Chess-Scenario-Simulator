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
}