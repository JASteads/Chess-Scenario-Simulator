public class Rook:Piece
{
    public bool HasMoved;

    public Rook(short location, short team) : base(location, team)
    {
        SetKind(1);
    }

    public override List<List<short>> CheckMoves()
    {
        List<List<short>> lines = new List<List<short>>();

        int column = GetPosition() % 8,
            row = GetPosition() / 8;

        for (int i = 0; i < 4; i++)
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

        return lines;
    }

    public void TryCastle() { }
}