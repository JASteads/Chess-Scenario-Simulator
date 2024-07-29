public class Pawn:Piece
{
    public bool isVulnerable;

    public Pawn(short location, short team) : base(location, team)
    {
        isVulnerable = false;
        SetKind(0);
    }

    public override List<List<short>> CheckMoves()
    {
        List<List<short>> lines = [];
        int pos = GetPosition(), next,
            row = pos / 8,
            direction = GetTeam() == 0 ? 1 : -1;

        for (int i = 0; i < 4; i++)
            lines.Add(new List<short>());

        // Top-right
        next = pos + (9 * direction);
        if (next < 64 && row + (1 * direction) == (next / 8))
            lines[0].Add((short)next);

        // Top-middle
        next -= (1 * direction);
        if (next < 64 && row + (1 * direction) == (next / 8))
            lines[1].Add((short)next);

        // Top-left
        next -= (1 * direction);
        if (next < 64 && row + (1 * direction) == (next / 8))
            lines[2].Add((short)next);

        // Double-step
        next = pos + (16 * direction);
        lines[3].Add((short)next);

        return lines;
    }

    public void Promote() { }
}