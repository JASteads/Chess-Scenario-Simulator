using System;
using System.Drawing;
using System.Windows.Forms;

public class PuzzleUI : UserInterface
{
    
    Label  ratingText,
           scoreText;
    Panel  gameOverPanel;
    Button nextButton;
    PictureBox[] lives;

    public PuzzleUI(EventHandler hEvent, EventHandler nEvent) 
        : base()
    {
        Init(hEvent, nEvent);
        Control test = new Label();
    }

    void Init(EventHandler hEvent, EventHandler nEvent)
    {
        Point livesStartPos = new Point(60, 520);
        Point ratingPos = new Point(110, 210),
              attemptsPos = new Point(80, 410),
              scorePos = new Point(1030, 450);
        Size  numSize = new Size(300, 60);
        Font  infoFont = new Font("Soopafresh", 24F),
              numFont = new Font("Soopafresh", 32F);

        // Static rating label
        Label rating = new Label();
        rating.AutoSize = true;
        rating.BackColor = Color.Transparent;
        rating.Font = infoFont;
        rating.ForeColor = Color.White;
        rating.Location = ratingPos;
        rating.Name = "Rating Label";
        rating.Size = new Size(119, 39);
        rating.Text = "Rating";
        rating.TextAlign = ContentAlignment.MiddleCenter;

        // Generate ratingBar below turn label
        PictureBox ratingBar = new PictureBox();
        ratingBar.BackColor = Color.White;
        ratingBar.Location =
            new Point(ratingPos.X - 33, ratingPos.Y + 45);
        ratingBar.Name = $"Turn Bar";
        ratingBar.Size = new Size(185, 5);
        ratingBar.TabStop = false;

        ratingText = new Label();
        ratingText.AutoSize = false;
        ratingText.BackColor = Color.Transparent;
        ratingText.Font = numFont;
        ratingText.ForeColor = Color.White;
        ratingText.Location = 
            new Point(ratingPos.X - 95, ratingPos.Y + 60);
        ratingText.Name = "Rating Label";
        ratingText.Text = "777";
        ratingText.TextAlign = ContentAlignment.MiddleCenter;
        ratingText.Size = numSize;

        // Static attempts label
        Label attempts = new Label();
        attempts.AutoSize = true;
        attempts.BackColor = Color.Transparent;
        attempts.Font = infoFont;
        attempts.ForeColor = Color.White;
        attempts.Location = attemptsPos;
        attempts.Name = "label2";
        attempts.Size = new Size(185, 78);
        attempts.Text = "Attempts\r\nRemaining";
        attempts.TextAlign = ContentAlignment.MiddleCenter;

        PictureBox attemptsBar = new PictureBox();
        attemptsBar.BackColor = Color.White;
        attemptsBar.Location =
            new Point(attemptsPos.X, attemptsPos.Y + 85);
        attemptsBar.Name = $"Turn Bar";
        attemptsBar.Size = new Size(185, 5);
        attemptsBar.TabStop = false;

        // Generate lives
        lives = new PictureBox[3];
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i] = new PictureBox();
            PictureBox p = lives[i];
            p.Image = Main.Properties.Resources.King_W;
            p.Location = new Point(livesStartPos.X + (76 * i), 520);
            p.Name = $"Life {i + 1}";
            p.Size = new Size(70, 70);
            p.TabStop = false;
        }

        // Next button
        nextButton = new Button();
        nextButton.FlatAppearance.BorderColor =
            Color.White;
        nextButton.FlatAppearance.BorderSize = 5;
        nextButton.FlatStyle = FlatStyle.Flat;
        nextButton.Font = infoFont;
        nextButton.ForeColor = Color.White;
        nextButton.Location = new Point(990, 59);
        nextButton.Margin = new Padding(60, 50, 60, 60);
        nextButton.Name = "nextButton";
        nextButton.Size = new Size(180, 55);
        nextButton.Text = "NEXT";
        nextButton.UseVisualStyleBackColor = true;
        nextButton.Click += nEvent;

        // Static score label
        Label score = new Label();
        score.AutoSize = true;
        score.BackColor = Color.Transparent;
        score.Font = infoFont;
        score.ForeColor = Color.White;
        score.Location = scorePos;
        score.Name = "label3";
        score.Size = new Size(107, 39);
        score.TabIndex = 6;
        score.Text = "Score";
        score.TextAlign = ContentAlignment.MiddleCenter;

        PictureBox scoreBar = new PictureBox();
        scoreBar.BackColor = Color.White;
        scoreBar.Location =
            new Point(scorePos.X - 37, scorePos.Y + 45);
        scoreBar.Name = $"Turn Bar";
        scoreBar.Size = new Size(185, 5);
        scoreBar.TabStop = false;

        scoreText = new Label();
        scoreText.AutoSize = false;
        scoreText.BackColor = Color.Transparent;
        scoreText.Font = numFont;
        scoreText.ForeColor = Color.White;
        scoreText.Location = new Point(
            scorePos.X - 95, scorePos.Y + 60);
        scoreText.Name = "Score Label";
        scoreText.Size = numSize;
        scoreText.Text = "4311";
        scoreText.TextAlign = ContentAlignment.MiddleCenter;

        // Add all items to the panel
        controls.Add(rating);
        controls.Add(ratingBar);
        controls.Add(ratingText);
        controls.Add(score);
        controls.Add(scoreBar);
        controls.Add(scoreText);
        controls.Add(attempts);
        controls.Add(attemptsBar);
        controls.Add(nextButton);
        foreach (PictureBox p in lives)
            controls.Add(p);
    }
}