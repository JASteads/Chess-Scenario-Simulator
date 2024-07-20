using System;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        MainMenuUI mainMenuUI;
        BoardUI boardUI;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMainMenu();
        }

        void LoadMainMenu()
        {
            mainMenuUI = new MainMenuUI(
                StandardButton_Click, PuzzleButton_Click,
                QuitButton_Click);
            Controls.Add(mainMenuUI.GetPanel());
        }

        void LoadBoardUI()
        {
            boardUI = new BoardUI(HomeButton_Click);
            Controls.Add(boardUI.GetPanel());
        }
        
        void CloseMainMenu()
        {
            Controls.Remove(mainMenuUI.GetPanel());
            mainMenuUI = null;
        }

        void CloseStandard()
        {
            Controls.Remove(boardUI.GetPanel());
            boardUI = null;
        }

        void StartStandard()
        {
            Console.WriteLine("Standard starting ..");
            LoadBoardUI();
        }

        void StartPuzzleGenerator()
        {
            Console.WriteLine("Puzzle starting ..");

        }

        void StandardButton_Click(object sender, EventArgs e)
        {
            CloseMainMenu();
            StartStandard();
        }

        void PuzzleButton_Click(object sender, EventArgs e)
        {
            CloseMainMenu();
            StartPuzzleGenerator();
        }

        void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void HomeButton_Click(object sender, EventArgs e)
        {
            CloseStandard();
            LoadMainMenu();
        }
    }
}
