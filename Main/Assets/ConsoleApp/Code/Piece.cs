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

    public class King:Piece
    {
        public King(ushort location, ushort team) : base(location, team)
        {
            SetType(5);
        }
        public override bool CheckMoves()
        {
            return true;
        }

    }
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
        public class Rook:Piece
    {
        public Rook(ushort location, ushort team) : base(location, team)
        {
            SetType(1);
        }
        public override bool CheckMoves()
        {
            return true;
        }
    }
        public class Pawn:Piece
    {
        public Pawn(ushort location, ushort team) : base(location, team)
        {
            SetType(0);
        }
        public override bool CheckMoves()
        {
            return true;
        }
    }

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