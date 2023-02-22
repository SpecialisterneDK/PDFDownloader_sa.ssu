namespace PDFDownloader
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
            this.StartButton = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.PathButton = new System.Windows.Forms.Button();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.InputButton = new System.Windows.Forms.Button();
            this.CopyProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(50, 84);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(93, 46);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Run";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(252, 150);
            this.OutputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.Size = new System.Drawing.Size(218, 43);
            this.OutputBox.TabIndex = 1;
            // 
            // PathButton
            // 
            this.PathButton.Location = new System.Drawing.Point(494, 150);
            this.PathButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PathButton.Name = "PathButton";
            this.PathButton.Size = new System.Drawing.Size(86, 42);
            this.PathButton.TabIndex = 2;
            this.PathButton.Text = "Select Output Folder";
            this.PathButton.UseVisualStyleBackColor = true;
            this.PathButton.Click += new System.EventHandler(this.PathButton_Click);
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(252, 84);
            this.InputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.InputBox.Multiline = true;
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(218, 43);
            this.InputBox.TabIndex = 3;
            // 
            // InputButton
            // 
            this.InputButton.Location = new System.Drawing.Point(494, 84);
            this.InputButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.InputButton.Name = "InputButton";
            this.InputButton.Size = new System.Drawing.Size(86, 42);
            this.InputButton.TabIndex = 4;
            this.InputButton.Text = "Select Input (.xlsx)";
            this.InputButton.UseVisualStyleBackColor = true;
            this.InputButton.Click += new System.EventHandler(this.InputButton_Click);
            // 
            // CopyProgressBar
            // 
            this.CopyProgressBar.Location = new System.Drawing.Point(83, 273);
            this.CopyProgressBar.Name = "CopyProgressBar";
            this.CopyProgressBar.Size = new System.Drawing.Size(497, 47);
            this.CopyProgressBar.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 375);
            this.Controls.Add(this.CopyProgressBar);
            this.Controls.Add(this.InputButton);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.PathButton);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.StartButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.Button PathButton;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.Button InputButton;
        private System.Windows.Forms.ProgressBar CopyProgressBar;
    }
}

