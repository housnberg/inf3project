namespace game.gui
{
    partial class Gui
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
            this.board = new System.Windows.Forms.Panel();
            this.chatWindow = new System.Windows.Forms.RichTextBox();
            this.chatInput = new System.Windows.Forms.TextBox();
            this.Placeholder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // board
            // 
            this.board.Location = new System.Drawing.Point(17, 15);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(400, 400);
            this.board.TabIndex = 0;
            this.board.MouseDown += new System.Windows.Forms.MouseEventHandler(this.board_MouseDown);
            // 
            // chatWindow
            // 
            this.chatWindow.Location = new System.Drawing.Point(17, 421);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ReadOnly = true;
            this.chatWindow.Size = new System.Drawing.Size(636, 117);
            this.chatWindow.TabIndex = 1;
            this.chatWindow.Text = "";
            // 
            // chatInput
            // 
            this.chatInput.Location = new System.Drawing.Point(17, 544);
            this.chatInput.Name = "chatInput";
            this.chatInput.Size = new System.Drawing.Size(636, 20);
            this.chatInput.TabIndex = 2;
            // 
            // Placeholder
            // 
            this.Placeholder.AutoSize = true;
            this.Placeholder.Location = new System.Drawing.Point(445, 199);
            this.Placeholder.Name = "Placeholder";
            this.Placeholder.Size = new System.Drawing.Size(195, 13);
            this.Placeholder.TabIndex = 3;
            this.Placeholder.Text = "This is a placeholder for the Game Stats";
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 575);
            this.Controls.Add(this.Placeholder);
            this.Controls.Add(this.chatInput);
            this.Controls.Add(this.chatWindow);
            this.Controls.Add(this.board);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Gui";
            this.Text = "Game - Inf3 - Group 8";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Gui_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel board;
        private System.Windows.Forms.RichTextBox chatWindow;
        private System.Windows.Forms.TextBox chatInput;
        private System.Windows.Forms.Label Placeholder;
    }
}