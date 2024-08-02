using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class BoardUI : UserInterface
{
    Label currentTurnLabel;
    List<PieceGraphic> tiles;


    public BoardUI(
        EventHandler hEvent) : base()
    {
        currentTurnLabel = new Label();
        tiles = new List<PieceGraphic>();

        Init(hEvent);
    }

    public void SetTiles(List<Piece> wTeam, List<Piece> bTeam,
        List<List<short>> moves)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Piece p = wTeam.Find(w => w.GetPosition() == i);

            if (p == null) p = bTeam.Find(w => w.GetPosition() == i);

            if (tiles[i].piece != p)
            {
                tiles[i].piece = p;
                tiles[i].SetButtonImage(p);
            }

            tiles[i].SetTileColor(moves.Exists(
                v => v.Contains((short)i)) ?
                PieceGraphic.SELECTED : i);
        }
    }

    void SelectTile(int pos)
    {
        // Send message containing 'pos' to all listeners
        BoardEventArgs args = new BoardEventArgs();
        args.Position = pos;
        OnSelection(args);
    }

    public void ChangeTurn()
    {
        string turn = currentTurnLabel.Text.Substring(0, 5);
        currentTurnLabel.Text = turn == "Black" ?
            "White turn" : "Black turn";
    }

    public void EndStandardGame(bool whiteTurn, bool isCheckmate)
    {
        if (isCheckmate)
        {
            string winner = whiteTurn ? "White" : "Black";

            Console.WriteLine($"Checkmate!! {winner} team wins.");
        }
        else
            Console.WriteLine("Stalemate! Nobody wins.");
        
        tiles.ForEach(t => { t.b.Enabled = false; });
    }

    void Init(EventHandler hEvent)
    {
        const int TILE_SIZE = 70;
        Point tileStart = new Point (360, 550),
              turnLabelPos = new Point(985, 230);
        Font  noteFont = new Font("Soopafresh", 16F);

        // Generate tile buttons
        for (int i = 0; i < 64; i++)
        {
            int xOffset = (i % 8) * TILE_SIZE,
                yOffset = (i / 8) * TILE_SIZE,
                position = i;

            Point pos = new Point(
                xOffset + tileStart.X,
                tileStart.Y - yOffset);
            
            Button tile = new Button();
            tile.FlatAppearance.BorderSize = 0;
            tile.FlatStyle = FlatStyle.Flat;
            tile.Location = pos;
            tile.Margin = new Padding(0);
            tile.Name = $"Tile #{i + 1}";
            tile.Size =
                new Size(TILE_SIZE, TILE_SIZE);
            tile.Text = "";
            tile.UseVisualStyleBackColor = false;
            tile.Click += (s, e) => SelectTile(position);
            tile.TabStop = false;
            tiles.Add(new PieceGraphic(tile));
            controls.Add(tile);
        }

        // Generate alphabetical markers
        for (int i = 0; i < 8; i++)
        {
            char c = (char)('a' + i);
            int xPos = (TILE_SIZE / 4) + (TILE_SIZE * i) + tileStart.X;

            Label symbol = new Label();
            symbol.AutoSize = true;
            symbol.BackColor = Color.Transparent;
            symbol.Font = noteFont;
            symbol.ForeColor = Color.White;
            symbol.Location =
                new Point(xPos, tileStart.Y + 80);
            symbol.Margin = new Padding(0);
            symbol.Name = $"Symbol {c}";
            symbol.Size = new Size(25, 27);
            symbol.Text = c.ToString();
            controls.Add(symbol);
        }

        // Generate numeric markers
        for (int i = 0; i < 8; i++)
        {
            int offset = TILE_SIZE * i;
            int index = i + 1,
                yPos = (TILE_SIZE / 4) + tileStart.Y - offset;

            Label symbol = new Label();
            symbol.AutoSize = true;
            symbol.BackColor = Color.Transparent;
            symbol.Font = noteFont;
            symbol.ForeColor = Color.White;
            symbol.Location =
                new Point(315, yPos);
            symbol.Margin = new Padding(0);
            symbol.Name = $"Symbol {index}";
            symbol.Size = new Size(25, 27);
            symbol.Text = $"{index}";
            controls.Add(symbol);
        }

        // Generate home button
        Button homeButton = new Button();
        homeButton.FlatAppearance.BorderColor =
            Color.White;
        homeButton.FlatAppearance.BorderSize = 5;
        homeButton.FlatStyle = FlatStyle.Flat;
        homeButton.Font =
            new Font("Soopafresh", 24F);
        homeButton.ForeColor = Color.White;
        homeButton.Location = new Point(70, 59);
        homeButton.Margin = new Padding(60, 50, 60, 60);
        homeButton.Name = "homeButton";
        homeButton.Size = new Size(180, 55);
        homeButton.Text = "HOME";
        homeButton.Click += hEvent;
        homeButton.UseVisualStyleBackColor = true;
            
        // Format turn label
        currentTurnLabel.AutoSize = true;
        currentTurnLabel.Font = 
            new Font("Soopafresh", 24F);
        currentTurnLabel.ForeColor = Color.White;
        currentTurnLabel.Location = turnLabelPos;
        currentTurnLabel.Name = "Current Turn Label";
        currentTurnLabel.Size = new Size(192, 39);
        currentTurnLabel.Text = "White Turn";
        currentTurnLabel.AutoSize = false;
        currentTurnLabel.TextAlign = ContentAlignment.MiddleCenter;

        // Generate bar below turn label
        PictureBox bar = new PictureBox();
        bar.BackColor = Color.White;
        bar.Location =
            new Point(turnLabelPos.X + 3, turnLabelPos.Y + 45);
        bar.Name = $"Turn Bar";
        bar.Size = new Size(185, 5);
        bar.TabStop = false;

        controls.Add(homeButton);
        controls.Add(currentTurnLabel);
        controls.Add(bar);
    }

    protected virtual void OnSelection(BoardEventArgs e)
    {
        Selection?.Invoke(this, e);
    }
    
    public event EventHandler<BoardEventArgs> Selection;
}