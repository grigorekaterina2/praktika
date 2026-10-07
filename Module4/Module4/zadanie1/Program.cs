using System;
using System.Collections.Generic; // Пространство имен для коллекций (списков, массивов)

namespace GeometricShapes
{

    // Интерфейс — это контракт, который обязывает классы реализовать указанные методы.
    interface IShape // Создание интерфейса «Фигура» 
    {
        double GetArea();      // Сигнатура метода для вычисления площади (возвращает дробное число)
        double GetPerimeter(); // Сигнатура метода для вычисления периметра
    }

    class Circle : IShape // Класс «Круг», реализующий интерфейс IShape 
    {
        private double radius; // Приватное поле для хранения радиуса круга


        public Circle(double radius) // Конструктор класса Circle, который принимает радиус при создании объекта
        {
            this.radius = radius; // Сохранение переданного радиуса в поле 
        }

        public double GetArea()  // Реализация метода для вычисления площади круга 
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        public double GetPerimeter() // Реализация метода для вычисления периметра круга
        {
            return 2 * Math.PI * radius;
        }
    }

    class Rectangle : IShape // Класс «Прямоугольник», также реализующий интерфейс IShape
    {
        private double width;  // Ширина прямоугольника
        private double height; // Высота прямоугольника

        public Rectangle(double width, double height) // Конструктор для инициализации ширины и высоты
        {
            this.width = width;   // Используем 'this' для обращения к полю объекта, отличая его от параметра 'width'
            this.height = height; // Используем 'this' для обращения к полю объекта, отличая его от параметра 'height'
        }

        public double GetArea() // Реализация метода площади 
        {
            return width * height;
        }

        public double GetPerimeter() // Реализация метода периметра 
        {
            return 2 * (width + height);
        }
    }

    class Triangle : IShape // Класс «Треугольник», реализующий интерфейс IShape
    {
        private double a; // Первая сторона (поле)
        private double b; // Вторая сторона
        private double c; // Третья сторона

        public Triangle(double a, double b, double c) // Конструктор треугольника 
        {
            // Проверка правила треугольника: сумма любых двух сторон должна быть больше третьей
            if ((a + b <= c) || (a + c <= b) || (b + c <= a))
            {
                throw new ArgumentException("Такой треугольник не существует."); // Если треугольник построить нельзя, выбрасываем исключение
            }

            // Запись "this.a" означает поле класса, а "a" — аргумент, переданный извне
            this.a = a; // Записываем первую сторону в поле 
            this.b = b; // Записываем вторую сторону в поле 
            this.c = c; // Записываем третью сторону в поле 
        }

        public double GetPerimeter()  // Вычисление периметра треугольника
        {
            return a + b + c;
        }

        public double GetArea() // Вычисление площади треугольника по формуле Герона
        {
            double p = GetPerimeter() / 2; // Нахождение полупериметра
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c)); // Вычисление площади по формуле Герона
        }
    }

    class Program // Главный класс программы
    {
        static void Main()
        {
            List<IShape> shapes = new List<IShape>(); // Создание пустого списка (List) для динамического добавления фигур, которые введет пользователь

            Console.WriteLine(" Ввод геометрических фигур ");

            // Блок try-catch для безопасного ввода данных для Круга и перехвата возможных ошибок
            try
            {
                Console.Write("Введите радиус круга: ");

                // Использование оператора ?? "0", чтобы защититься от null и пустых строк
                double radius = double.Parse(Console.ReadLine() ?? "0");

                // Создание объекта круга и добавление его в общий список фигур
                shapes.Add(new Circle(radius));
            }
            catch (Exception ex) // Если пользователь ввел буквы или произошла ошибка
            {
                Console.WriteLine($"Ошибка при вводе круга: {ex.Message}");
            }

            // Блок try-catch для безопасного ввода данных для Прямоугольника
            try
            {
                Console.Write("\nВведите ширину прямоугольника: ");
                double width = double.Parse(Console.ReadLine() ?? "0"); // Считываем ширину с защитой от null

                Console.Write("Введите высоту прямоугольника: ");
                double height = double.Parse(Console.ReadLine() ?? "0"); // Считываем высоту с защитой от null

                // Создание прямоугольника и добавление его в список
                shapes.Add(new Rectangle(width, height));
            }
            catch (Exception ex) // Перехват исключений ввода или параметров
            {
                Console.WriteLine($"Ошибка при вводе прямоугольника: {ex.Message}");
            }

            // Блок try-catch для безопасного ввода данных для Треугольника
            try
            {
                Console.WriteLine("\nВведите стороны треугольника:");
                Console.Write("Сторона a: ");
                double a = double.Parse(Console.ReadLine() ?? "0"); // Считывание стороны a

                Console.Write("Сторона b: ");
                double b = double.Parse(Console.ReadLine() ?? "0"); // Считывание стороны b

                Console.Write("Сторона c: ");
                double c = double.Parse(Console.ReadLine() ?? "0"); // Считывание стороны c

                // Создание треугольника (проверка конструктора на существование сторон)
                shapes.Add(new Triangle(a, b, c));
            }
            catch (Exception ex) // Перехват ошибок 
            {
                Console.WriteLine($"Ошибка при создании треугольника: {ex.Message}");
            }

            Console.WriteLine("\n Результаты вычислений ");

            // Проход циклом по каждой фигуре 
            foreach (var shape in shapes)
            {
                // Узнаем реальное имя класса текущей фигуры (например, "Circle", "Rectangle" или "Triangle")
                Console.WriteLine($"Фигура: {shape.GetType().Name}");

                // Вызов метода вычисления площади
                Console.WriteLine($"Площадь: {Math.Round(shape.GetArea(), 2)}");

                // Вызов метода вычисления периметра
                Console.WriteLine($"Периметр: {Math.Round(shape.GetPerimeter(), 2)}");
            }
        }
    }
}