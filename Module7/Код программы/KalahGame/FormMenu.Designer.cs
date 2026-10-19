namespace KalahGame
{
    partial class FormMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.btn = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSound = new System.Windows.Forms.Button();
            this.btnMusic = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.gbMode = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbPvp = new System.Windows.Forms.RadioButton();
            this.rbPvc = new System.Windows.Forms.RadioButton();
            this.gbDiff = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbEasy = new System.Windows.Forms.RadioButton();
            this.rbMed = new System.Windows.Forms.RadioButton();
            this.rbHard = new System.Windows.Forms.RadioButton();
            this.gbStones = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.rb5 = new System.Windows.Forms.RadioButton();
            this.rb6 = new System.Windows.Forms.RadioButton();
            this.btnSettings = new System.Windows.Forms.Button();
            this.gbMode.SuspendLayout();
            this.gbDiff.SuspendLayout();
            this.gbStones.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Peru;
            this.btnStart.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStart.ForeColor = System.Drawing.Color.SeaShell;
            this.btnStart.Location = new System.Drawing.Point(150, 434);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(500, 80);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "НАЧАТЬ ИГРУ";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnRules
            // 
            this.btnRules.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRules.ForeColor = System.Drawing.Color.Sienna;
            this.btnRules.Location = new System.Drawing.Point(650, 7);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(111, 45);
            this.btnRules.TabIndex = 6;
            this.btnRules.Text = "Правила";
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(0, 0);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Impact", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExit.ForeColor = System.Drawing.Color.Sienna;
            this.btnExit.Location = new System.Drawing.Point(650, 515);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 38);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "ВЫХОД";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSound
            // 
            this.btnSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSound.ForeColor = System.Drawing.Color.Sienna;
            this.btnSound.Location = new System.Drawing.Point(545, 7);
            this.btnSound.Name = "btnSound";
            this.btnSound.Size = new System.Drawing.Size(45, 45);
            this.btnSound.TabIndex = 7;
            this.btnSound.Text = "🔊";
            this.btnSound.Click += new System.EventHandler(this.btnSound_Click);
            // 
            // btnMusic
            // 
            this.btnMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMusic.ForeColor = System.Drawing.Color.Sienna;
            this.btnMusic.Location = new System.Drawing.Point(596, 7);
            this.btnMusic.Name = "btnMusic";
            this.btnMusic.Size = new System.Drawing.Size(45, 45);
            this.btnMusic.TabIndex = 8;
            this.btnMusic.Text = "🎵";
            this.btnMusic.Click += new System.EventHandler(this.btnMusic_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTitle.ForeColor = System.Drawing.Color.Olive;
            this.labelTitle.Location = new System.Drawing.Point(50, 53);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(700, 94);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Kalah: Your Turn";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbMode
            // 
            this.gbMode.Controls.Add(this.label1);
            this.gbMode.Controls.Add(this.rbPvp);
            this.gbMode.Controls.Add(this.rbPvc);
            this.gbMode.Location = new System.Drawing.Point(150, 150);
            this.gbMode.Name = "gbMode";
            this.gbMode.Size = new System.Drawing.Size(500, 70);
            this.gbMode.TabIndex = 1;
            this.gbMode.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(145, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выбор режима игры";
            // 
            // rbPvp
            // 
            this.rbPvp.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPvp.Location = new System.Drawing.Point(6, 30);
            this.rbPvp.Name = "rbPvp";
            this.rbPvp.Size = new System.Drawing.Size(214, 34);
            this.rbPvp.TabIndex = 0;
            this.rbPvp.Text = "Игрок 1 vs Игрок 2";
            // 
            // rbPvc
            // 
            this.rbPvc.Checked = true;
            this.rbPvc.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPvc.Location = new System.Drawing.Point(256, 30);
            this.rbPvc.Name = "rbPvc";
            this.rbPvc.Size = new System.Drawing.Size(238, 34);
            this.rbPvc.TabIndex = 1;
            this.rbPvc.TabStop = true;
            this.rbPvc.Text = "Игрок vs Компьютер";
            // 
            // gbDiff
            // 
            this.gbDiff.Controls.Add(this.label2);
            this.gbDiff.Controls.Add(this.rbEasy);
            this.gbDiff.Controls.Add(this.rbMed);
            this.gbDiff.Controls.Add(this.rbHard);
            this.gbDiff.Location = new System.Drawing.Point(150, 230);
            this.gbDiff.Name = "gbDiff";
            this.gbDiff.Size = new System.Drawing.Size(500, 70);
            this.gbDiff.TabIndex = 2;
            this.gbDiff.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Sienna;
            this.label2.Location = new System.Drawing.Point(165, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Сложность ИИ";
            // 
            // rbEasy
            // 
            this.rbEasy.Checked = true;
            this.rbEasy.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEasy.Location = new System.Drawing.Point(36, 30);
            this.rbEasy.Name = "rbEasy";
            this.rbEasy.Size = new System.Drawing.Size(104, 24);
            this.rbEasy.TabIndex = 0;
            this.rbEasy.TabStop = true;
            this.rbEasy.Text = "Лёгкий";
            // 
            // rbMed
            // 
            this.rbMed.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMed.Location = new System.Drawing.Point(179, 25);
            this.rbMed.Name = "rbMed";
            this.rbMed.Size = new System.Drawing.Size(132, 34);
            this.rbMed.TabIndex = 1;
            this.rbMed.Text = "Средний";
            // 
            // rbHard
            // 
            this.rbHard.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHard.Location = new System.Drawing.Point(335, 25);
            this.rbHard.Name = "rbHard";
            this.rbHard.Size = new System.Drawing.Size(135, 34);
            this.rbHard.TabIndex = 2;
            this.rbHard.Text = "Сложный";
            // 
            // gbStones
            // 
            this.gbStones.Controls.Add(this.label3);
            this.gbStones.Controls.Add(this.rb4);
            this.gbStones.Controls.Add(this.rb5);
            this.gbStones.Controls.Add(this.rb6);
            this.gbStones.Location = new System.Drawing.Point(150, 310);
            this.gbStones.Name = "gbStones";
            this.gbStones.Size = new System.Drawing.Size(500, 70);
            this.gbStones.TabIndex = 3;
            this.gbStones.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Sienna;
            this.label3.Location = new System.Drawing.Point(145, -1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 28);
            this.label3.TabIndex = 3;
            this.label3.Text = "Количество камней";
            // 
            // rb4
            // 
            this.rb4.Checked = true;
            this.rb4.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb4.Location = new System.Drawing.Point(115, 30);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(104, 24);
            this.rb4.TabIndex = 0;
            this.rb4.TabStop = true;
            this.rb4.Text = "4";
            // 
            // rb5
            // 
            this.rb5.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb5.Location = new System.Drawing.Point(225, 30);
            this.rb5.Name = "rb5";
            this.rb5.Size = new System.Drawing.Size(104, 24);
            this.rb5.TabIndex = 1;
            this.rb5.Text = "5";
            // 
            // rb6
            // 
            this.rb6.Font = new System.Drawing.Font("Jokerman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb6.Location = new System.Drawing.Point(335, 30);
            this.rb6.Name = "rb6";
            this.rb6.Size = new System.Drawing.Size(104, 24);
            this.rb6.TabIndex = 2;
            this.rb6.Text = "6";
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Peru;
            this.btnSettings.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSettings.ForeColor = System.Drawing.Color.SeaShell;
            this.btnSettings.Location = new System.Drawing.Point(150, 386);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(500, 48);
            this.btnSettings.TabIndex = 9;
            this.btnSettings.Text = "Настройки внешнего вида";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.gbMode);
            this.Controls.Add(this.gbDiff);
            this.Controls.Add(this.gbStones);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.btnSound);
            this.Controls.Add(this.btnMusic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kalah: Your Turn";
            this.gbMode.ResumeLayout(false);
            this.gbMode.PerformLayout();
            this.gbDiff.ResumeLayout(false);
            this.gbDiff.PerformLayout();
            this.gbStones.ResumeLayout(false);
            this.gbStones.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnStart, btnRules, btn, btnExit, btnSound, btnMusic;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox gbMode, gbDiff, gbStones;
        private System.Windows.Forms.RadioButton rbPvp, rbPvc, rbEasy, rbMed, rbHard, rb4, rb5, rb6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSettings;
    }
}