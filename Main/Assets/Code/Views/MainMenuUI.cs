using System;
using System.Windows.Forms;
using System.Drawing;

public class MainMenuUI : UserInterface
{
    Label  title,
           authorsLabel,
           hiScoreText;
    Button quitButton,
           puzzleButton,
           standardButton;

    public MainMenuUI(EventHandler sEvent, 
        EventHandler pEvent, EventHandler qEvent) : base()
    {
        hiScoreText = new Label();
        authorsLabel = new Label();
        title = new Label();
        quitButton = new Button();
        puzzleButton = new Button();
        standardButton = new Button();

        Init(sEvent, pEvent, qEvent);
    }

    void Init(EventHandler sEvent,
        EventHandler pEvent, EventHandler qEvent)
    {
        Font buttonFont = new Font("Microsoft Sans Serif", 24F);

        // 
        // panel
        // 
        controls.Add(hiScoreText);
        controls.Add(authorsLabel);
        controls.Add(title);
        controls.Add(quitButton);
        controls.Add(puzzleButton);
        controls.Add(standardButton);
        // 
        // hiScoreText
        // 
        hiScoreText.AutoSize = true;
        hiScoreText.Font = new Font("Soopafresh", 16F);
        hiScoreText.ForeColor = Color.White;
        hiScoreText.Location = new Point(50, 27);
        hiScoreText.Margin = new Padding(50, 30, 80, 80);
        hiScoreText.Name = "hiScoreText";
        hiScoreText.Size = new Size(149, 27);
        hiScoreText.TabIndex = 5;
        hiScoreText.Text = "Hi-Score : 00";
        // 
        // authorsLabel
        // 
        authorsLabel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        authorsLabel.AutoSize = true;
        authorsLabel.Font = new Font("Soopafresh", 12F);
        authorsLabel.ForeColor = Color.White;
        authorsLabel.Location = new Point(786, 680);
        authorsLabel.Margin = new Padding(5);
        authorsLabel.Name = "authorsLabel";
        authorsLabel.Size = new Size(452, 20);
        authorsLabel.TabIndex = 8;
        authorsLabel.Text = "Authors: Jonavinne Steadham, Milton Pope, Hammad Ali";
        // 
        // title
        // 
        title.Anchor = AnchorStyles.None;
        title.AutoSize = true;
        title.BackColor = Color.Transparent;
        title.Font = new Font("Soopafresh", 32F, FontStyle.Bold);
        title.ForeColor = Color.White;
        title.Location = new Point(445, 77);
        title.Name = "title";
        title.Size = new Size(362, 104);
        title.TabIndex = 7;
        title.Text = "Chess Scenario\r\nSimulator";
        title.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // quitButton
        // 
        quitButton.Anchor = AnchorStyles.None;
        quitButton.Font = new Font("Microsoft Sans Serif", 24F);
        quitButton.Location = new Point(451, 506);
        quitButton.Name = "quitButton";
        quitButton.Size = new Size(360, 82);
        quitButton.TabIndex = 6;
        quitButton.Text = "Quit";
        quitButton.UseVisualStyleBackColor = true;
        quitButton.Click += qEvent;
        // 
        // puzzleButton
        // 
        puzzleButton.Anchor = AnchorStyles.None;
        puzzleButton.Font = buttonFont;
        puzzleButton.Location = new Point(451, 418);
        puzzleButton.Name = "puzzleButton";
        puzzleButton.Size = new Size(360, 82);
        puzzleButton.TabIndex = 5;
        puzzleButton.Text = "Start Puzzle";
        puzzleButton.UseVisualStyleBackColor = true;
        puzzleButton.Click += pEvent;
        // 
        // standardButton
        // 
        standardButton.Anchor = AnchorStyles.None;
        standardButton.Font = buttonFont;
        standardButton.Location = new Point(451, 332);
        standardButton.Name = "standardButton";
        standardButton.Size = new Size(360, 80);
        standardButton.TabIndex = 4;
        standardButton.Text = "Start Standard";
        standardButton.UseVisualStyleBackColor = true;
        standardButton.Click += sEvent;
    }
}