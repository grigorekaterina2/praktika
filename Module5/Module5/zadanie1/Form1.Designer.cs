namespace PaintApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            this.panelTools = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.rbSquare = new System.Windows.Forms.RadioButton();
            this.rbCircle = new System.Windows.Forms.RadioButton();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.panelTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTools
            // 
            this.panelTools.BackColor = System.Drawing.SystemColors.Control;
            this.panelTools.Controls.Add(this.btnClear);
            this.panelTools.Controls.Add(this.btnUndo);
            this.panelTools.Controls.Add(this.rbSquare);
            this.panelTools.Controls.Add(this.rbCircle);
            this.panelTools.Controls.Add(this.rbLine);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTools.Location = new System.Drawing.Point(0, 0);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(800, 50);
            this.panelTools.TabIndex = 0;
            // 
            // rbLine
            // 
            this.rbLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLine.Checked = true;
            this.rbLine.Location = new System.Drawing.Point(12, 10);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(80, 30);
            this.rbLine.TabIndex = 0;
            this.rbLine.TabStop = true;
            this.rbLine.Text = "Линия";
            this.rbLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbLine.UseVisualStyleBackColor = true;
            // 
            // rbCircle
            // 
            this.rbCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbCircle.Location = new System.Drawing.Point(98, 10);
            this.rbCircle.Name = "rbCircle";
            this.rbCircle.Size = new System.Drawing.Size(80, 30);
            this.rbCircle.TabIndex = 1;
            this.rbCircle.Text = "Круг";
            this.rbCircle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbCircle.UseVisualStyleBackColor = true;
            // 
            // rbSquare
            // 
            this.rbSquare.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSquare.Location = new System.Drawing.Point(184, 10);
            this.rbSquare.Name = "rbSquare";
            this.rbSquare.Size = new System.Drawing.Size(80, 30);
            this.rbSquare.TabIndex = 2;
            this.rbSquare.Text = "Квадрат";
            this.rbSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSquare.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(284, 10);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(90, 30);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "Отменить";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(380, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 50);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(800, 400);
            this.pbCanvas.TabIndex = 1;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbCanvas);
            this.Controls.Add(this.panelTools);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рисование на холсте";
            this.panelTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.RadioButton rbCircle;
        private System.Windows.Forms.RadioButton rbSquare;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox pbCanvas;
    }
}