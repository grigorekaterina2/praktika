namespace ImageViewer
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
            this.lblZoom = new System.Windows.Forms.Label();
            this.btnFit = new System.Windows.Forms.Button();
            this.btnActual = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.panelView = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelTools.SuspendLayout();
            this.panelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTools
            // 
            this.panelTools.Controls.Add(this.lblZoom);
            this.panelTools.Controls.Add(this.btnFit);
            this.panelTools.Controls.Add(this.btnActual);
            this.panelTools.Controls.Add(this.btnZoomOut);
            this.panelTools.Controls.Add(this.btnZoomIn);
            this.panelTools.Controls.Add(this.btnOpen);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTools.Location = new System.Drawing.Point(0, 0);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(800, 45);
            this.panelTools.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 30);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Открыть...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(120, 8);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(40, 30);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.Text = "+";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(166, 8);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(40, 30);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.Text = "−";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnActual
            // 
            this.btnActual.Location = new System.Drawing.Point(212, 8);
            this.btnActual.Name = "btnActual";
            this.btnActual.Size = new System.Drawing.Size(60, 30);
            this.btnActual.TabIndex = 3;
            this.btnActual.Text = "100%";
            this.btnActual.UseVisualStyleBackColor = true;
            this.btnActual.Click += new System.EventHandler(this.btnActual_Click);
            // 
            // btnFit
            // 
            this.btnFit.Location = new System.Drawing.Point(278, 8);
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(80, 30);
            this.btnFit.TabIndex = 4;
            this.btnFit.Text = "Вписать";
            this.btnFit.UseVisualStyleBackColor = true;
            this.btnFit.Click += new System.EventHandler(this.btnFit_Click);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblZoom.Location = new System.Drawing.Point(376, 15);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(24, 17);
            this.lblZoom.TabIndex = 5;
            this.lblZoom.Text = "—";
            // 
            // panelView
            // 
            this.panelView.AutoScroll = true;
            this.panelView.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelView.Controls.Add(this.pictureBox1);
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(0, 45);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(800, 405);
            this.panelView.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы (*.*)|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.panelTools);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр изображений";
            this.panelTools.ResumeLayout(false);
            this.panelTools.PerformLayout();
            this.panelView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnActual;
        private System.Windows.Forms.Button btnFit;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}