using System.Windows.Forms;
using System.Drawing;
using Main.Properties;

public class PieceGraphic
{
    public Piece piece;
    public Button b;

    static readonly Color 
        lightColor = Color.White, 
        darkColor = Color.Gray,
        selectedColor = Color.LightGoldenrodYellow;

    static public int SELECTED = 64;

    public PieceGraphic(Button b)
    {
        this.b = b;
    }

    public void SetButtonImage(Piece p)
    {
        if (p == null)
        {
            b.BackgroundImage = null;
            return;
        }

        Piece.Kind kind = (Piece.Kind)p.GetKind();

        if (p.GetTeam() == 0)
        {
            if (kind == Piece.Kind.Rook)
                b.BackgroundImage = Resources.Rook_W;
            if (kind == Piece.Kind.Bishop)
                b.BackgroundImage = Resources.Bishop_W;
            if (kind == Piece.Kind.Knight)
                b.BackgroundImage = Resources.Knight_W;
            if (kind == Piece.Kind.King)
                b.BackgroundImage = Resources.King_W;
            if (kind == Piece.Kind.Queen)
                b.BackgroundImage = Resources.Queen_W;
            if (kind == Piece.Kind.Pawn)
                b.BackgroundImage = Resources.Pawn_W;
        }
        else
        {
            if (kind == Piece.Kind.Rook)
                b.BackgroundImage = Resources.Rook_B;
            if (kind == Piece.Kind.Bishop)
                b.BackgroundImage = Resources.Bishop_B;
            if (kind == Piece.Kind.Knight)
                b.BackgroundImage = Resources.Knight_B;
            if (kind == Piece.Kind.King)
                b.BackgroundImage = Resources.King_B;
            if (kind == Piece.Kind.Queen)
                b.BackgroundImage = Resources.Queen_B;
            if (kind == Piece.Kind.Pawn)
                b.BackgroundImage = Resources.Pawn_B;
        }
    }

    public void SetTileColor(int pos)
    {
        Color c;

        if (pos == SELECTED) c = selectedColor;
        // Determines dark color for even rows
        else if (pos % 16 >= 8 && pos % 2 == 1) c = darkColor;
        // Determines dark color for odd rows
        else if (pos % 16 < 8 && pos % 2 == 0) c = darkColor;
        // Neither condition means tile is white
        else c = lightColor;

        b.BackColor = c;
    }
}

