using System;
using System.Data.Common;
using System.Threading;

public abstract class Piece
{

    protected short information = 0;
    public Piece(int location, int team)
    {
        SetPosition(location);
        SetTeam(team);
    }

    public class King:Piece

    public int GetPosition(Piece p)
    {
        return p.information & 63;
    }
    public int GetType(Piece p)
    {
        return (p.information >> 6) & 7;
    }
    public int GetTeam(Piece p)
    {
        return (p.information >> 9) & 1;
    }
    public void SetPosition(int newPos)
    {
        if (newPos >= 0 && newPos <= 63)
        {
            information = (information & ~63) | newPos;
        }
    }
    public void SetType(int newType)
    {
        if (newType >= 0 && newType <= 5)
        {
            
        }
    }
    public void SetTeam(int newTeam)
    {   
        if(newTeam >= 0 && newTeam <= 1)
        {

        }
    }
    /*public char[] Piece:NotateLocation()
    {
        char[] result = new char[2];    
        int pos = GetPosition() + 1; // returns 3

        int letterNum = (pos / 8) + 1; // 1
        int valueNum = pos % 8;  // 3
        
        result[0] = (char)((int)'A' + letterNum);   
        result[1] = (char)((int)'1' + valueNum);

        return result;
    }
    */

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