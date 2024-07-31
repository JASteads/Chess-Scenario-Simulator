using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        readonly List<UserInterface> uiList;
        Mode activeGameMode;

        public Form1()
        {
            uiList = new List<UserInterface>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartMainMenu();
        }

        /* Adds an interface to the UI stack. This is necessary for
         * displaying multiple interfaces at once
         */
        void LoadInterface(UserInterface ui)
        {
            uiList.Add(ui);
            SuspendLayout();
            Controls.AddRange(ui.GetControls().ToArray());
            ResumeLayout(false);
        }

        /* Removes all interfaces from the stack and their respective
         * panels from the main window form
         */
        void ClearScreen()
        {
            SuspendLayout();
            uiList.Clear();
            Controls.Clear();
            ResumeLayout(false);
        }

        void FindNewElements(object sender, EventArgs e)
        {
            SuspendLayout();
            foreach (UserInterface ui in uiList)
            {
                ui.GetControls().ForEach(c =>
                {
                    if (!Controls.Contains(c)) Controls.Add(c);
                });
            }
            ResumeLayout(false);
        }

        void StartMainMenu()
        {
            ClearScreen();
            LoadMainMenuUI();
        }

        void StartStandard()
        {
            ClearScreen();
            activeGameMode = 
                new StandardMode(HomeButton_Click, FindNewElements);
            LoadBoard();
        }

        void StartPuzzleGenerator()
        {
            ClearScreen();
            LoadBoard();
            LoadPuzzleUI();
        }

        void LoadMainMenuUI()
        {
            LoadInterface(new MainMenuUI(StandardButton_Click,
                PuzzleButton_Click, QuitButton_Click));
        }

        void LoadBoard()
        {
            LoadInterface(activeGameMode.GetBoardUI());
            activeGameMode.StartGame();
        }

        void LoadPuzzleUI()
        {
            LoadInterface(new PuzzleUI(
                HomeButton_Click, NextButton_Click));
        }


        // BUTTON EVENT HANDLING

        void StandardButton_Click(object sender, EventArgs e)
        {
            StartStandard();
        }

        void PuzzleButton_Click(object sender, EventArgs e)
        {
            StartPuzzleGenerator();
        }

        void QuitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void HomeButton_Click(object sender, EventArgs e)
        {
            StartMainMenu();
        }

        void NextButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Next, please!");
        }
    }
}
