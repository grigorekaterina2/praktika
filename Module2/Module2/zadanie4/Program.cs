using System;

// Создание интерфейса IDrawable
interface IDrawable
{
    void Draw(); // Метод для вывода информации о рисуемом объекте
}

// Класс Circle, реализующий интерфейс IDrawable
class Circle : IDrawable
{
    public double Radius { get; set; } // Свойство для хранения радиуса круга

    public Circle(double radius) // Конструктор круга, принимающий радиус
    {
        Radius = radius; // Сохраняет радиус в свойство
    }

    public void Draw() // Реализация обязательного метода Draw
    {
        Console.WriteLine($"Рисуем круг с радиусом: {Radius}");
    }
}

// Класс Rectangle, реализующий интерфейс IDrawable
class Rectangle : IDrawable
{
    public double Width { get; set; } // Автосвойство для ширины прямоугольника
    public double Height { get; set; } // Автосвойство для высоты прямоугольника

    public Rectangle(double width, double height) // Конструктор прямоугольника
    {
        Width = width; // Сохраняет ширину
        Height = height; // Сохраняет высоту
    }

    public void Draw() // Реализация метода Draw для прямоугольника
    {
        Console.WriteLine($"Рисуем прямоугольник с шириной {Width} и высотой {Height}");
    }
}

// Класс Triangle, реализующий интерфейс IDrawable
class Triangle : IDrawable
{
    public double SideA { get; set; } // Автосвойство первой стороны
    public double SideB { get; set; }
    public double SideC { get; set; }

    public Triangle(double sideA, double sideB, double sideC) // Конструктор треугольника
    {
        SideA = sideA; // Сохраняет сторону A
        SideB = sideB;
        SideC = sideC;
    }

    public void Draw() // Реализация метода Draw для треугольника
    {
        Console.WriteLine($"Рисуем треугольник со сторонами: {SideA}, {SideB}, {SideC}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание массива объектов, реализующих интерфейс IDrawable
        IDrawable[] drawables = new IDrawable[]
        {
            new Circle(5.5),
            new Rectangle(10.0, 4.0),
            new Triangle(3.0, 4.0, 5.0),
            new Circle(2.1) 
        };

        // Вызов метода Draw() для каждого объекта в цикле (полиморфизм)
        Console.WriteLine(" Вывод информации об объектах с помощью интерфейса IDrawable ");
        foreach (IDrawable shape in drawables)
        {
            shape.Draw(); // Вызов реализация метода Draw() конкретного класса
        }
    }
}