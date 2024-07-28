using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class Queen:Piece
{
    public Queen(short location, short team) : base(location, team)
    {
        SetKind(4);
    }

    public override List<List<short>> CheckMoves()
    {
        List<List<short>> lines = new List<List<short>>();

        int column = GetPosition() % 8,
            row = GetPosition() / 8,
            pos, next;

        for (int i = 0; i < 8; i++)
            lines.Add(new List<short>());

        // Left line
        for (int i = 0; i < column; i++)
        {
            lines[0].Add((short)(GetPosition() - (i + 1)));
        }

        // Right line
        for (int i = 0; i < (7 - column); i++)
        {
            lines[1].Add((short)(GetPosition() + (i + 1)));
        }

        // Bottom line
        for (int i = 0; i < row; i++)
        {
            lines[2].Add((short)(GetPosition() - ((i + 1) * 8)));
        }

        // Top line
        for (int i = 0; i < (7 - row); i++)
        {
            lines[3].Add((short)(GetPosition() + ((i + 1) * 8)));
        }

        // Top-left diagonal
        pos = (short)GetPosition();
        next = pos + 7;
        while (next < 64 && (pos / 8) != (next / 8))
        {
            lines[4].Add((short)next);
            pos = next;
            next = (short)(pos + 7);
        }

        // Top-right diagonal
        pos = (short)GetPosition();
        next = pos + 9;
        while (next < 64 && (pos / 8) + 2 != (next / 8))
        {
            lines[5].Add((short)next);
            pos = next;
            next = (short)(pos + 9);
        }

        // Bottom-right diagonal
        pos = (short)GetPosition();
        next = pos - 7;
        while (next < 64 && (pos / 8) != (next / 8))
        {
            lines[6].Add((short)next);
            pos = next;
            next = (short)(pos - 7);
        }

        // Bottom-left diagonal
        pos = (short)GetPosition();
        next = pos - 9;
        while (next >= 0 && (pos / 8) - 2 != (next / 8))
        {
            lines[7].Add((short)next);
            pos = next;
            next = (short)(pos - 9);
        }

        
        return lines;
    }

}