using System;
using System.Collections.Generic;

public abstract class Shape // Создание базового абстрактного класса Фигура
{
    
    public abstract double CalculateArea(); // Абстрактный метод не имеет реализации здесь 
}

public class Circle : Shape // Производный класс Круг, наследующийся от Shape
{  
    public double Radius { get; set; } // Свойство для хранения радиуса круга
    
    public Circle(double radius) // Конструктор для инициализации радиуса при создании объекта
    {
        Radius = radius;
    }

    public override double CalculateArea() // Переопределение метода для вычисления площади круга 
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape // Производный класс Прямоугольник, наследующийся от Фигура
{
    // Свойства для ширины и высоты
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height) // Конструктор для инициализации сторон прямоугольника
    {
        Width = width;
        Height = height;
    }

    public override double CalculateArea() // Переопределение метода для вычисления площади прямоугольника
    {
        return Width * Height;
    }
}

public class Triangle : Shape // Производный класс Треугольник, наследующийся от Фигура
{
    // Свойства для хранения трех сторон треугольника
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }
    
    public Triangle(double a, double b, double c) // Конструктор для инициализации сторон
    {
        A = a;
        B = b;
        C = c;
    }

    public override double CalculateArea() // Переопределение метода для вычисления площади 
    {
        double p = (A + B + C) / 2.0; // Вычисление полупериметра
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C)); // Возвращение корня из произведения разностей полупериметра и сторон
    }
}

class Program // Основной класс
{
    static void Main()
    {
        // Создание списка базового типа Фигура,куда складываются объекты разных классов-наследников (полиморфизм)
        List<Shape> shapes = new List<Shape>
        {
            new Circle(5.0), // Выделение памяти под новый объект и вызов его конструктора
            new Rectangle(4.0, 6.0),
            new Triangle(3.0, 4.0, 5.0)
        };

        Console.WriteLine("Динамический вызов метода вычисления площади через делегат:\n");

        // Проход в цикле по каждой фигуре в списке
        foreach (var shape in shapes)
        {
            // Использование стандартного обобщенного делегата Func<double>, он ссылается на метод, который не принимает аргументов и возвращает тип double
            // Привязка делегата к методу CalculateArea 
            Func<double> areaDelegate = shape.CalculateArea;

            // Вызов метода динамически через созданный делегат (делегат знает, к какому именно объекту он привязан, и вызовет правильный вариант метода)
            double area = areaDelegate();

            // Вывод имени типа фигуры и вычисленную площадь 
            Console.WriteLine($"Фигура: {shape.GetType().Name}, Площадь: {area:F2}");
        }
    }
}