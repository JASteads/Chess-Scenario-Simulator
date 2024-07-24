using System;
using System.Data.Common;
using System.Threading;

public abstract class Piece
{

    protected short information = 0;
    public Piece(ushort location, ushort team)
    {
        SetPosition(location);
        SetTeam(team);
    }


    public int GetPosition()
    {
        return information & 63;
    }
    public int GetType()
    {
        return (information >> 6) & 7;
    }
    public int GetTeam()
    {
        return (information >> 9) & 1;
    }
    public void SetPosition(ushort newPos)
    {
        if (newPos >= 0 && newPos <= 63)
        {
            information = (short)((information & ~63) | newPos);
        }
    }
    public void SetType(int newType)
    {
        if (newType >= 0 && newType <= 5)
        {
            information = (short)((information & ~(7 << 6)) | ((short)newType << 6));
        }
    }
    public void SetTeam(int newTeam)
    {   
        if(newTeam >= 0 && newTeam <= 1)
        {
            information = (short)((information & ~(1 << 9)) | ((short)newTeam << 9));
        }
    }
    public void SetInterception(ushort num)
    {
        if (num >= 0 && num <= 63)
        {
            information = (short)(num);
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

     public abstract bool CheckMoves();
  
    public void Move()
    {

    }

    public void OnCapture()
    {

    }
    
}