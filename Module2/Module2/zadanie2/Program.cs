using System;

class Shape // Базовый класс, представляющий геометрическую фигуру
{
    // virtual позволяет производным классам переопределить метод
    public virtual double Area() // Возврат площади фигуры
    {
        return 0;
    }

    public virtual double Perimeter() // Возврат периметра фигуры
    {
        return 0;
    }
}

class Circle : Shape // Наследование класса Circle от Shape
{
    private double radius; // Радиус круга

    public Circle(double radius) // Конструктор принимает радиус
    {
        this.radius = radius;
    }

    // override заменяет метод базового класса своей реализацией
    public override double Area()
    {
        return Math.PI * radius * radius;
    }

    public override double Perimeter()
    {
        return 2 * Math.PI * radius; // Длина окружности
    }
}

class Rectangle : Shape // Наследовнаие класса Rectangle от Shape
{
    private double width;
    private double height;

    public Rectangle(double width, double height) // Конструктор принимает стороны
    {
        this.width = width;
        this.height = height;
    }

    public override double Area()
    {
        return width * height;
    }

    public override double Perimeter()
    {
        return 2 * (width + height);
    }
}

class Program
{
    // Ввод числа с проверкой на корректность
    static double ReadPositiveDouble(string message)
    {
        double value;
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            // Попытка преобразовать строку в число
            if (double.TryParse(input, out value) && value > 0) // Проверка: текст — это число и оно больше нуля
            {
                return value;
            }
            Console.WriteLine("Некорректный ввод! Введите положительное число.");
        }
    }

    static void Main(string[] args)
    {
        // Создание объектов всех классов
        Shape shape = new Shape();

        Console.WriteLine("Круг");
        double r = ReadPositiveDouble("Введите радиус: ");
        Circle circle = new Circle(r); // Создание объекта Circle с введенным радиусом

        Console.WriteLine("Прямоугольник");
        double w = ReadPositiveDouble("Введите ширину: ");
        double h = ReadPositiveDouble("Введите длину: ");
        Rectangle rectangle = new Rectangle(w, h); // Создание объекта Rectangle с введенными сторонами

        // Вывод площадей и периметров
        Console.WriteLine();
        Console.WriteLine($"Shape:     площадь = {shape.Area()}, периметр = {shape.Perimeter()}");
        Console.WriteLine($"Circle:    площадь = {circle.Area():F2}, периметр = {circle.Perimeter():F2}");
        Console.WriteLine($"Rectangle: площадь = {rectangle.Area():F2}, периметр = {rectangle.Perimeter():F2}");
    }
}