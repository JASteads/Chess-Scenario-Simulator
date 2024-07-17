using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CloseMainMenu();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void CloseMainMenu()
        {
            Controls.Remove(title);
            Controls.Remove(standardButton);
            Controls.Remove(puzzleButton);
            Controls.Remove(quitButton);
        }

        void StartStandard()
        {
            
        }

        void StartPuzzleGenerator()
        {

        }

        
    }
}
