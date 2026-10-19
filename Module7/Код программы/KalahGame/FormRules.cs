using System;
using System.Drawing;
using System.Windows.Forms;
namespace KalahGame
{
    // Окно для отображения правил игры
    public partial class FormRules : Form
    {
        public FormRules()
        {
            InitializeComponent();
            // Кнопка для закрытия окна правил
            btnClose.Click += (s, e) => this.Close();
            // Подсказка при наведении на кнопку закрытия
            ToolTip rulesTip = new ToolTip();
            rulesTip.SetToolTip(btnClose, "Закрыть правила");
        }
        // Рисование рамки вокруг окна
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Создание коричневой рамки толщиной 12 пикселей
            using (Pen borderPen = new Pen(Color.FromArgb(160, 100, 60), 12))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}