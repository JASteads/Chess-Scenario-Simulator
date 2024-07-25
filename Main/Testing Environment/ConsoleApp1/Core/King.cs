using System.Collections.Generic;

public class King:Piece
{
    public bool HasMoved, IsChecked;

    public King(short location, short team) : base(location, team)
    {
        SetKind(5);
    }

    public override List<List<short>> CheckMoves()
    {
        List<List<short>> lines = new List<List<short>>();
        int pos = GetPosition(), next,
            row = pos / 8;

        for (int i = 0; i < 8; i++)
            lines.Add(new List<short>());

        // Top-right
        next = pos + 9;
        if (next < 64 && row + 1 == (next / 8))
            lines[0].Add((short)next);

        // Top-middle
        --next;
        if (next < 64 && row + 1 == (next / 8))
            lines[1].Add((short)next);

        // Top-left
        --next;
        if (next < 64 && row + 1 == (next / 8))
            lines[2].Add((short)next);

        // Center-right
        next = pos + 1;
        if (next < 64 && row == (next / 8))
            lines[3].Add((short)next);

        // Center-left
        next -= 2;
        if (next >= 0 && row == (next / 8))
            lines[4].Add((short)next);

        // Bottom-right
        next = pos - 7;
        if (next >= 0 && row - 1 == (next / 8))
            lines[5].Add((short)next);

        // Bottom-middle
        --next;
        if (next >= 0 && row - 1 == (next / 8))
            lines[6].Add((short)next);

        // Bottom-left
        --next;
        if (next >= 0 && row - 1 == (next / 8))
            lines[7].Add((short)next);

        return lines;
    }

    public void TryCastle() { }
}