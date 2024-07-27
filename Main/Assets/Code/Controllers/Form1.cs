using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        readonly Stack<UserInterface> uiStack;

        public Form1()
        {
            uiStack = new Stack<UserInterface>();
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
            uiStack.Push(ui);
            SuspendLayout();
            Controls.AddRange(uiStack.Peek().GetControls().ToArray());
            ResumeLayout(false);
        }

        /* Removes all interfaces from the stack and their respective
         * panels from the main window form
         */
        void ClearScreen()
        {
            uiStack.Clear();
            SuspendLayout();
            Controls.Clear();
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
            LoadBoardUI();
        }

        void StartPuzzleGenerator()
        {
            ClearScreen();
            LoadBoardUI();
            LoadPuzzleUI();
        }

        void LoadMainMenuUI()
        {
            LoadInterface(new MainMenuUI(StandardButton_Click,
                PuzzleButton_Click, QuitButton_Click));
        }

        void LoadBoardUI()
        {
            LoadInterface(new BoardUI(HomeButton_Click));
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
