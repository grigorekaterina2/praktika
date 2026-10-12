using System;
using System.Drawing;           // Image, Bitmap, Size
using System.IO;                // Path для имени файла
using System.Windows.Forms;     // формы и элементы управления

namespace ImageViewer
{
    public partial class Form1 : Form   // главная форма, вторая часть в Form1.Designer.cs
    {
        private double zoom = 1.0;   // текущий масштаб, 1.0 = 100%
        private const double Step = 1.25;   // шаг изменения масштаба
        private const double MinZoom = 0.05;   // минимальный масштаб 5%
        private const double MaxZoom = 10.0;   // максимальный масштаб 1000%

        public Form1()   // конструктор формы
        {
            InitializeComponent();   // создание элементов из Designer
        }

        private void ApplyZoom()   // применение масштаба к изображению
        {
            Image img = pictureBox1.Image;   // текущее изображение
            if (img == null) return;   // выход при отсутствии изображения

            if (zoom < MinZoom) zoom = MinZoom;   // ограничение снизу
            if (zoom > MaxZoom) zoom = MaxZoom;   // ограничение сверху

            int w = Math.Max(1, (int)(img.Width * zoom));   // новая ширина
            int h = Math.Max(1, (int)(img.Height * zoom));   // новая высота
            pictureBox1.Size = new Size(w, h);   // изменение размера рамки с картинкой
            lblZoom.Text = Math.Round(zoom * 100) + "%";   // вывод масштаба в процентах
        }

        private void FitToWindow()   // масштаб по размеру окна
        {
            Image img = pictureBox1.Image;   // текущее изображение
            if (img == null) return;   // выход при отсутствии изображения

            double kx = (double)panelView.ClientSize.Width / img.Width;   // масштаб по ширине
            double ky = (double)panelView.ClientSize.Height / img.Height;   // масштаб по высоте
            zoom = Math.Min(kx, ky);   // меньший масштаб: изображение целиком в окне
            ApplyZoom();   // применение масштаба
        }

        private void btnOpen_Click(object sender, EventArgs e)   // кнопка «Открыть»
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;   // выход при отмене выбора

            try   // защита от ошибок чтения
            {
                using (Image temp = Image.FromFile(openFileDialog1.FileName))   // загрузка из файла
                {
                    Image old = pictureBox1.Image;   // прежнее изображение
                    pictureBox1.Image = new Bitmap(temp);   // копия в памяти, файл не блокируется
                    if (old != null) old.Dispose();   // освобождение прежнего изображения
                }

                this.Text = Path.GetFileName(openFileDialog1.FileName) + " - Просмотр изображений";   // имя файла в заголовке

                Image img = pictureBox1.Image;   // новое изображение
                bool larger = img.Width > panelView.ClientSize.Width || img.Height > panelView.ClientSize.Height;   // проверка превышения окна

                if (larger)   // изображение больше окна
                    FitToWindow();   // вписывание в окно
                else
                {
                    zoom = 1.0;   // реальный размер
                    ApplyZoom();   // применение масштаба
                }
            }
            catch (Exception)   // ошибка чтения
            {
                MessageBox.Show("Не удалось открыть изображение.", "Ошибка");   // сообщение об ошибке
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)   // кнопка «+»
        {
            zoom *= Step;   // увеличение масштаба
            ApplyZoom();   // применение масштаба
        }

        private void btnZoomOut_Click(object sender, EventArgs e)   // кнопка «−»
        {
            zoom /= Step;   // уменьшение масштаба
            ApplyZoom();   // применение масштаба
        }

        private void btnActual_Click(object sender, EventArgs e)   // кнопка «100%»
        {
            zoom = 1.0;   // реальный размер
            ApplyZoom();   // применение масштаба
        }

        private void btnFit_Click(object sender, EventArgs e)   // кнопка «Вписать»
        {
            FitToWindow();   // масштаб по размеру окна
        }
    }
}