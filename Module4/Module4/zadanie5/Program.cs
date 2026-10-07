using System;
using System.Collections.Generic; // Пространство имен для коллекций

namespace CanvasDrawingApp
{
    // Создание интерфейса «Рисунок» — контракт для холста
    interface IDrawing
    {
        void DrawLine(string startPoint, string endPoint);     // Рисование линии
        void DrawCircle(string center, double radius);          // Рисование круга
        void DrawRectangle(string topLeft, double width, double height); // Рисование прямоугольника
    }

    // Класс «Холст», реализующий интерфейс IDrawing
    class Canvas : IDrawing
    {
        private string canvasName; // Название холста

        // Конструктор класса холста для инициализации названия
        public Canvas(string canvasName)
        {
            this.canvasName = canvasName;
        }

        // Реализация метода рисования линии
        public void DrawLine(string startPoint, string endPoint)
        {
            Console.WriteLine($"Холст '{canvasName}': Построение линии из точки {startPoint} в точку {endPoint}.");
        }

        // Реализация метода рисования круга
        public void DrawCircle(string center, double radius)
        {
            Console.WriteLine($"Холст '{canvasName}': Построение круга с центром в точке {center} и радиусом {radius}.");
        }

        // Реализация метода рисования прямоугольника
        public void DrawRectangle(string topLeft, double width, double height)
        {
            Console.WriteLine($"Холст '{canvasName}': Построение прямоугольника с верхним левым углом {topLeft}, шириной {width} и высотой {height}.");
        }

        // Получение названия холста
        public string GetCanvasName() => canvasName;
    }

    // Главный класс программы
    class Program
    {
        static void Main()
        {
            Console.WriteLine(" Приложение для автоматической генерации рисунков на холсте ");

            // Создание генератора случайных чисел
            Random random = new Random();

            // Создание объекта холста с именем
            Canvas myCanvas = new Canvas("Холст_1");

            // Генерация случайных параметров для линий, кругов и прямоугольников
            string lineStart = $"{random.Next(0, 10)},{random.Next(0, 10)}";
            string lineEnd = $"{random.Next(10, 50)},{random.Next(10, 50)}";

            string circleCenter = $"{random.Next(10, 30)},{random.Next(10, 30)}";
            double circleRadius = Math.Round(random.NextDouble() * 10 + 1, 2); // Случайный радиус от 1 до 11

            string rectTopLeft = $"{random.Next(0, 20)},{random.Next(0, 20)}";
            double rectWidth = Math.Round(random.NextDouble() * 20 + 5, 2);   // Случайная ширина от 5 до 25
            double rectHeight = Math.Round(random.NextDouble() * 20 + 5, 2);  // Случайная высота от 5 до 25

            // Заголовок для результатов отрисовки
            Console.WriteLine($"\n Результаты автоматической отрисовки на холсте {myCanvas.GetCanvasName()}");

            // Вызов методов рисования фигур через интерфейсную ссылку
            IDrawing drawingTool = myCanvas; // Присваивание объекта холста переменной интерфейсного типа
            drawingTool.DrawLine(lineStart, lineEnd); // Вызов метода рисования линии
            drawingTool.DrawCircle(circleCenter, circleRadius); // Вызов метода рисования круга
            drawingTool.DrawRectangle(rectTopLeft, rectWidth, rectHeight); // Вызов метода рисования прямоугольника
        }
    }
}