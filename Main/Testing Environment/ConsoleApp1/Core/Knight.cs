using System.Collections.Generic;

public class Knight:Piece
{
    public Knight(short location, short team) : base(location, team)
    {
        SetKind(2);
    }

    public override List<List<short>> CheckMoves()
    {
        List<List<short>> lines = new List<List<short>>();
        int pos = GetPosition(), next;

        for (int i = 0; i < 8; i++)
            lines.Add(new List<short>());

        // Top-left
        next = pos + 15;
        if (next < 64 && (pos / 8) + 2 == (next / 8))
            lines[0].Add((short)next);

        // Top-right
        next += 2;
        if (next < 64 && (pos / 8) + 2 == (next / 8))
            lines[1].Add((short)next);

        // Upper-left
        next = pos + 6;
        if ((next < 64 && (pos / 8) + 1 == (next / 8)))
            lines[2].Add((short)next);

        // Upper-right
        next += 4;
        if ((next < 64 && (pos / 8) + 1 == (next / 8)))
            lines[3].Add((short)next);

        // Lower-right
        next = pos - 6;
        if ((next >= 0 && (pos / 8) - 1 == (next / 8)))
            lines[4].Add((short)next);

        // Lower-left
        next = pos - 4;
        if ((next >= 0 && (pos / 8) - 1 == (next / 8)))
            lines[5].Add((short)next);

        // Bottom-left
        next = pos - 15;
        if ((next >= 0 && (pos / 8) - 2 == (next / 8)))
            lines[6].Add((short)next);

        // Bottom-right
        next -= 2;
        if ((next >= 0 && (pos / 8) - 2 == (next / 8)))
            lines[7].Add((short)next);
        
        return lines;
    }
}