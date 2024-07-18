using System;
using System.Data.Common;
using System.Threading;

public abstract class Piece
{
    // public Graphic image;

    private int pos = 0;
    private int type = 0;
    private int team = 0;
    public short information = 0;

    int GetPosition(Piece p)
    {
        return p.information & 63;
    }
    int GetType(Piece p)
    {
        return (p.information >> 6) & 7;
    }
    int GetTeam(Piece p)
    {
        return (p.information >> 9) & 1;
    }
    void SetPosition(int newPos)
    {
        if (newPos >= 0 && newPos <= 63)
        {
            pos = newPos;
        }
    }
    void SetType(int newType)
    {
        if (newType >= 0 && newType <= 5)
        {
            type = newType;
        }
    }
    void SetTeam(int newTeam)
    {   
        if(newTeam >= 0 && newTeam <= 1)
        {
            team = newTeam;
        }
    }

    /* public abstract bool CheckMoves()
    {
        return true;
    }


    public void Move()
    {

    }

    public void OnCapture()
    {

    }
    */
}