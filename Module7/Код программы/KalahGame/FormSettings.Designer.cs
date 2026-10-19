namespace KalahGame
{
    partial class FormSettings
    {
        private System.ComponentModel.IContainer components = null;

        // Элементы управления
        public System.Windows.Forms.Label lblBoards, lblStones;
        public System.Windows.Forms.Button btnWood, btnBlue, btnPink, btnGravel, btnMarble, btnGems, btnSave, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.lblBoards = new System.Windows.Forms.Label();
            this.lblStones = new System.Windows.Forms.Label();
            this.btnWood = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnPink = new System.Windows.Forms.Button();
            this.btnGravel = new System.Windows.Forms.Button();
            this.btnMarble = new System.Windows.Forms.Button();
            this.btnGems = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBoards
            // 
            this.lblBoards.BackColor = System.Drawing.Color.Peru;
            this.lblBoards.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBoards.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblBoards.Location = new System.Drawing.Point(50, 58);
            this.lblBoards.Name = "lblBoards";
            this.lblBoards.Size = new System.Drawing.Size(500, 40);
            this.lblBoards.TabIndex = 0;
            this.lblBoards.Text = "Виды досок";
            this.lblBoards.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStones
            // 
            this.lblStones.BackColor = System.Drawing.Color.Peru;
            this.lblStones.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStones.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblStones.Location = new System.Drawing.Point(50, 201);
            this.lblStones.Name = "lblStones";
            this.lblStones.Size = new System.Drawing.Size(500, 40);
            this.lblStones.TabIndex = 1;
            this.lblStones.Text = "Виды камней";
            this.lblStones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWood
            // 
            this.btnWood.BackColor = System.Drawing.Color.Sienna;
            this.btnWood.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnWood.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnWood.Location = new System.Drawing.Point(50, 114);
            this.btnWood.Name = "btnWood";
            this.btnWood.Size = new System.Drawing.Size(133, 38);
            this.btnWood.TabIndex = 2;
            this.btnWood.Text = "Деревянная";
            this.btnWood.UseVisualStyleBackColor = false;
            // 
            // btnBlue
            // 
            this.btnBlue.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnBlue.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBlue.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnBlue.Location = new System.Drawing.Point(220, 114);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(137, 38);
            this.btnBlue.TabIndex = 3;
            this.btnBlue.Text = "Синяя";
            this.btnBlue.UseVisualStyleBackColor = false;
            // 
            // btnPink
            // 
            this.btnPink.BackColor = System.Drawing.Color.HotPink;
            this.btnPink.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPink.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnPink.Location = new System.Drawing.Point(399, 114);
            this.btnPink.Name = "btnPink";
            this.btnPink.Size = new System.Drawing.Size(151, 38);
            this.btnPink.TabIndex = 4;
            this.btnPink.Text = "Розовая";
            this.btnPink.UseVisualStyleBackColor = false;
            // 
            // btnGravel
            // 
            this.btnGravel.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnGravel.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGravel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGravel.Location = new System.Drawing.Point(50, 267);
            this.btnGravel.Name = "btnGravel";
            this.btnGravel.Size = new System.Drawing.Size(133, 43);
            this.btnGravel.TabIndex = 5;
            this.btnGravel.Text = "Галька";
            this.btnGravel.UseVisualStyleBackColor = false;
            // 
            // btnMarble
            // 
            this.btnMarble.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnMarble.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMarble.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnMarble.Location = new System.Drawing.Point(220, 267);
            this.btnMarble.Name = "btnMarble";
            this.btnMarble.Size = new System.Drawing.Size(137, 43);
            this.btnMarble.TabIndex = 6;
            this.btnMarble.Text = "Мрамор";
            this.btnMarble.UseVisualStyleBackColor = false;
            // 
            // btnGems
            // 
            this.btnGems.BackColor = System.Drawing.Color.Orange;
            this.btnGems.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGems.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnGems.Location = new System.Drawing.Point(399, 267);
            this.btnGems.Name = "btnGems";
            this.btnGems.Size = new System.Drawing.Size(151, 43);
            this.btnGems.TabIndex = 7;
            this.btnGems.Text = "Драгоценные";
            this.btnGems.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SpringGreen;
            this.btnSave.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnSave.Location = new System.Drawing.Point(315, 364);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 62);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Tomato;
            this.btnCancel.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnCancel.Location = new System.Drawing.Point(50, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(227, 62);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Не сохранять";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.PaleVioletRed;
            this.label1.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(-6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(613, 37);
            this.label1.TabIndex = 10;
            this.label1.Text = "Настройки внешнего вида";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.PaleVioletRed;
            this.label2.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(-6, 350);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(613, 105);
            this.label2.TabIndex = 11;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FormSettings
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBoards);
            this.Controls.Add(this.lblStones);
            this.Controls.Add(this.btnWood);
            this.Controls.Add(this.btnBlue);
            this.Controls.Add(this.btnPink);
            this.Controls.Add(this.btnGravel);
            this.Controls.Add(this.btnMarble);
            this.Controls.Add(this.btnGems);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки внешнего вида";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}