namespace Frontend
{
    partial class DefaultGui
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatInput = new System.Windows.Forms.TextBox();
            this.chatWindow = new System.Windows.Forms.RichTextBox();
            this.board = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // chatInput
            // 
            this.chatInput.BackColor = System.Drawing.SystemColors.Window;
            this.chatInput.Location = new System.Drawing.Point(23, 455);
            this.chatInput.Name = "chatInput";
            this.chatInput.Size = new System.Drawing.Size(350, 20);
            this.chatInput.TabIndex = 0;
            // 
            // chatWindow
            // 
            this.chatWindow.Location = new System.Drawing.Point(23, 368);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.ReadOnly = true;
            this.chatWindow.Size = new System.Drawing.Size(350, 81);
            this.chatWindow.TabIndex = 1;
            this.chatWindow.Text = "";
            // 
            // board
            // 
            this.board.Location = new System.Drawing.Point(23, 12);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(350, 350);
            this.board.TabIndex = 2;
            // 
            // DefaultGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(398, 487);
            this.Controls.Add(this.board);
            this.Controls.Add(this.chatWindow);
            this.Controls.Add(this.chatInput);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DefaultGui";
            this.ShowIcon = false;
            this.Text = "Default GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatInput;
        private System.Windows.Forms.RichTextBox chatWindow;
        private System.Windows.Forms.Panel board;
    }
}

