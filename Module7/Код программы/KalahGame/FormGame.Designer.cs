namespace KalahGame
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.btnSound = new System.Windows.Forms.Button();
            this.btnMusic = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSound
            // 
            this.btnSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSound.ForeColor = System.Drawing.Color.Sienna;
            this.btnSound.Location = new System.Drawing.Point(569, 12);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(45, 45);
            this.btnSound.TabIndex = 8;
            this.btnSound.Text = "🔊";
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // btnMusic
            // 
            this.btnMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMusic.ForeColor = System.Drawing.Color.Sienna;
            this.btnMusic.Location = new System.Drawing.Point(620, 12);
            this.btnMusic.Name = "btnMusic";
            this.btnMusic.Size = new System.Drawing.Size(45, 45);
            this.btnMusic.TabIndex = 9;
            this.btnMusic.Text = "🎵";
            this.btnMusic.Click += new System.EventHandler(this.btnMusic_Click);
            // 
            // btnRules
            // 
            this.btnRules.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRules.ForeColor = System.Drawing.Color.Sienna;
            this.btnRules.Location = new System.Drawing.Point(677, 12);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(111, 45);
            this.btnRules.TabIndex = 10;
            this.btnRules.Text = "Правила";
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMenu.ForeColor = System.Drawing.Color.Sienna;
            this.btnMenu.Location = new System.Drawing.Point(21, 12);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(111, 45);
            this.btnMenu.TabIndex = 12;
            this.btnMenu.Text = "Меню";
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.btnMusic);
            this.Controls.Add(this.btnSound);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kalah: Your Turn";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSound;
        private System.Windows.Forms.Button btnMusic;
        private System.Windows.Forms.Button btnRules;
        private System.Windows.Forms.Button btnMenu;
    }
}