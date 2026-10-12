using System;
using System.Collections.Generic;   // коллекция List<T>
using System.Drawing;               // Point, Color, Pen, Graphics
using System.Drawing.Drawing2D;     // Сглаживание
using System.Windows.Forms;         // формы и элементы управления

namespace PaintApp
{
    public partial class Form1 : Form   // главная форма
    {
        private readonly List<Shape> shapes = new List<Shape>();   // список законченных фигур

        private Shape currentShape = null;   // текущая фигура, null: рисования нет

        public Form1()   // конструктор формы
        {
            InitializeComponent();   // создание элементов из Designer
            this.DoubleBuffered = true;   // двойная буферизация, отсутствие мерцания
        }

        private Shape CreateShape(Point p)   // создание фигуры, p: точка нажатия
        {
            Shape shape;   // переменная для новой фигуры

            if (rbCircle.Checked)   // проверка кнопки «Круг»
                shape = new CircleShape();   // создание круга
            else if (rbSquare.Checked)   // проверка кнопки «Квадрат»
                shape = new SquareShape();   // создание квадрата
            else
                shape = new LineShape();   // линия по умолчанию

            shape.Start = p;   // начальная точка
            shape.End = p;   // конечная точка равна начальной
            return shape;   // возврат готовой фигуры
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)   // нажатие кнопки мыши
        {
            if (e.Button != MouseButtons.Left) return;   // реакция только на левую кнопку
            currentShape = CreateShape(e.Location);   // начало новой фигуры
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)   // движение мыши
        {
            if (currentShape == null) return;   // выход при отсутствии рисования
            currentShape.End = e.Location;   // перенос конечной точки за курсором
            pbCanvas.Invalidate();   // запрос перерисовки холста
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)   // отпускание кнопки мыши
        {
            if (currentShape == null) return;   // выход при отсутствии рисования
            currentShape.End = e.Location;   // фиксация конечной точки
            shapes.Add(currentShape);   // добавление фигуры в список
            currentShape = null;   // сброс текущей фигуры
            pbCanvas.Invalidate();   // запрос перерисовки холста
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)   // отрисовка холста
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;   // сглаживание краёв

            foreach (Shape shape in shapes)   // перебор готовых фигур
                shape.Draw(e.Graphics);   // полиморфный вызов Draw

            if (currentShape != null)   // проверка наличия текущей фигуры
                currentShape.Draw(e.Graphics);   // предпросмотр текущей фигуры
        }

        private void btnUndo_Click(object sender, EventArgs e)   // кнопка «Отменить»
        {
            if (shapes.Count == 0) return;   // выход при пустом списке
            shapes.RemoveAt(shapes.Count - 1);   // удаление последней фигуры
            pbCanvas.Invalidate();   // запрос перерисовки холста
        }

        private void btnClear_Click(object sender, EventArgs e)   // кнопка «Очистить»
        {
            shapes.Clear();   // очистка списка фигур
            currentShape = null;   // отмена текущей фигуры
            pbCanvas.Invalidate();   // запрос перерисовки холста
        }
    }

    public abstract class Shape   // абстрактный базовый класс фигур
    {
        public Point Start { get; set; }   // точка нажатия мыши
        public Point End { get; set; }   // текущая точка или точка отпускания

        public abstract void Draw(Graphics g);   // абстрактный метод, реализация в наследниках

        protected Pen CreatePen()   // создание пера, доступ у наследников
        {
            return new Pen(Color.Black, 2);   // чёрный цвет, толщина 2
        }
    }

    public class LineShape : Shape   // линия
    {
        public override void Draw(Graphics g)   // отрисовка линии
        {
            using (Pen pen = CreatePen())   // перо с автоматическим освобождением
            {
                g.DrawLine(pen, Start, End);   // отрезок от Start до End
            }
        }
    }

    public class CircleShape : Shape   // круг
    {
        public override void Draw(Graphics g)   // отрисовка круга
        {
            int dx = End.X - Start.X;   // смещение по X
            int dy = End.Y - Start.Y;   // смещение по Y
            int r = (int)Math.Sqrt(dx * dx + dy * dy);   // радиус по теореме Пифагора

            using (Pen pen = CreatePen())   // перо с автоматическим освобождением
            {
                g.DrawEllipse(pen, Start.X - r, Start.Y - r, 2 * r, 2 * r);   // эллипс: угол и диаметр
            }
        }
    }

    public class SquareShape : Shape   // квадрат
    {
        public override void Draw(Graphics g)   // отрисовка квадрата
        {
            int dx = End.X - Start.X;   // смещение по X
            int dy = End.Y - Start.Y;   // смещение по Y
            int side = Math.Max(Math.Abs(dx), Math.Abs(dy));   // сторона: большее смещение

            int x = dx >= 0 ? Start.X : Start.X - side;   // левый край с учётом направления
            int y = dy >= 0 ? Start.Y : Start.Y - side;   // верхний край с учётом направления

            using (Pen pen = CreatePen())   // перо с автоматическим освобождением
            {
                g.DrawRectangle(pen, x, y, side, side);   // прямоугольник со равными сторонами
            }
        }
    }
}