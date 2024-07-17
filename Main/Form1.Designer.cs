using System.Windows.Forms;

namespace Main
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.standardButton = new System.Windows.Forms.Button();
            this.puzzleButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // standardButton
            // 
            this.standardButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.standardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.standardButton.Location = new System.Drawing.Point(460, 350);
            this.standardButton.Name = "standardButton";
            this.standardButton.Size = new System.Drawing.Size(360, 80);
            this.standardButton.TabIndex = 0;
            this.standardButton.Text = "Start Standard";
            this.standardButton.UseVisualStyleBackColor = true;
            this.standardButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // puzzleButton
            // 
            this.puzzleButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.puzzleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.puzzleButton.Location = new System.Drawing.Point(460, 436);
            this.puzzleButton.Name = "puzzleButton";
            this.puzzleButton.Size = new System.Drawing.Size(360, 82);
            this.puzzleButton.TabIndex = 1;
            this.puzzleButton.Text = "Start Puzzle";
            this.puzzleButton.UseVisualStyleBackColor = true;
            this.puzzleButton.Click += new System.EventHandler(this.puzzleButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitButton.Location = new System.Drawing.Point(460, 524);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(360, 82);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // title
            // 
            this.title.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(357, 86);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(545, 51);
            this.title.TabIndex = 3;
            this.title.Text = "Chess Scenario Simulator";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.title);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.puzzleButton);
            this.Controls.Add(this.standardButton);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess Scenario Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button standardButton;
        private Button puzzleButton;
        private Button quitButton;
        private Label title;
    }
}

