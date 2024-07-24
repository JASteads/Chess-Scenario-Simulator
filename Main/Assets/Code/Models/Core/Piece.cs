using System.Collections.Generic;

public abstract class Piece
{
    protected short information;

    public Piece(short location, short team)
    {
        information = 0; // Set a default value to be specified by setters
        SetPosition(location);
        SetTeam(team);
    }

    public int GetPosition()
    {
        return information & 63;
    }
    public int GetKind()
    {
        return (information >> 6) & 7;
    }
    public int GetTeam()
    {
        return (information >> 9) & 1;
    }

    public void SetPosition(int newPos)
    {
        if (newPos >= 0 && newPos <= 63)
        {
            information = (short)((information & ~63) | (ushort)newPos);
        }
    }
    public void SetKind(int newType)
    {
        if (newType >= 0 && newType <= 5)
        {
            information = (short)((information & ~(7 << 6)) | ((short)newType << 6));
        }
    }
    public void SetTeam(int newTeam)
    {   
        if (newTeam >= 0 && newTeam <= 1)
        {
            information = (short)((information & ~(1 << 9)) | ((short)newTeam << 9));
        }
    }

    public string NotateLocation()
    {
        char[] result = new char[2];
        
        int pos = GetPosition() + 1,
            letterNum = (pos / 8) + 1,
            valueNum = (pos % 8) + 1;
        
        result[0] = (char)('A' + letterNum);   
        result[1] = (char)('1' + valueNum);

        return result.ToString();
    }
    
    public abstract List<List<short>> CheckMoves();
  
    public void Move()
    {

    }

    public void OnCapture()
    {

    }
    
}