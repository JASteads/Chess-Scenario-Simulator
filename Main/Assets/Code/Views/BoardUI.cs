using System;
using System.Drawing;
using System.Windows.Forms;

public class BoardUI : UserInterface
{
    readonly Color lightColor, darkColor;
    Label currentTurnLabel;

    public BoardUI(EventHandler hEvent) : base()
    {
        currentTurnLabel = new Label();

        lightColor = Color.White;
        darkColor = Color.Gray;

        Init(hEvent);
    }

    void SelectTile(int pos)
    {
        Console.WriteLine(pos + 1);
        ChangeTurn();
    }

    void ChangeTurn()
    {
        string turn = currentTurnLabel.Text.Substring(0, 5);
        currentTurnLabel.Text = turn == "Black" ?
            "White turn" : "Black turn";
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
            Color c = lightColor;

            // Determines dark color for even rows
            if (i % 16 >= 8 && i % 2 == 1)
                c = darkColor;
            // Determines dark color for odd rows
            else if (i % 16 < 8 && i % 2 == 0)
                c = darkColor;

            Button tile = new Button();
            tile.BackColor = c;
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
}