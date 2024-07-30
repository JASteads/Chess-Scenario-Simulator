public class Bishop:Piece
{
    public Bishop(short location, short team) : base(location, team)
    {
        SetKind(3);
    }

    public override List<List<short>> CheckMoves()
    {
        List<List<short>> lines = new List<List<short>>();
        int pos, next;

        for (int i = 0; i < 4; i++)
            lines.Add(new List<short>());

        // Top-left diagonal
        pos = (short)GetPosition();
        next = pos + 7;
        while (next < 64 && (pos / 8) != (next / 8))
        {
            lines[0].Add((short)next);
            pos = next;
            next = (short)(pos + 7);
        }

        // Top-right diagonal
        pos = (short)GetPosition();
        next = pos + 9;
        while (next < 64 && (pos / 8) + 2 != (next / 8))
        {
            lines[1].Add((short)next);
            pos = next;
            next = (short)(pos + 9);
        }

        // Bottom-right diagonal
        pos = (short)GetPosition();
        next = pos - 7;
        while (next < 64 && (pos / 8) != (next / 8))
        {
            lines[2].Add((short)next);
            pos = next;
            next = (short)(pos - 7);
        }

        // Bottom-left diagonal
        pos = (short)GetPosition();
        next = pos - 9;
        while (next >= 0 && (pos / 8) - 2 != (next / 8))
        {
            lines[3].Add((short)next);
            pos = next;
            next = (short)(pos - 9);
        }

        return lines;
    }
}
    
