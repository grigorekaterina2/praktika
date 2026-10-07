using System;
using System.Collections.Generic; // Пространство имен для списков

namespace UniversitySystem
{
    interface IStudent // Интерфейс «Студент» 
    {
        double GetAverageGrade(); // Метод для получения среднего балла
        int GetCourseNumber();    // Метод для получения номера курса
        string GetName();         // Метод для получения имени
    }

    // Класс для студентов 1-го курса
    class FirstYearStudent : IStudent
    {
        private string name;         // Имя студента
        private double averageGrade; // Средний балл

        public FirstYearStudent(string name, double averageGrade)
        {
            this.name = name;             // Сохраняем имя в поле
            this.averageGrade = averageGrade; // Сохраняем средний балл
        }

        public double GetAverageGrade() => averageGrade; // Возвращаем средний балл
        public int GetCourseNumber() => 1;               // Возвращаем номер курса (1)
        public string GetName() => name;                 // Возвращаем имя
    }

    // Класс для студентов 2-го курса
    class SecondYearStudent : IStudent
    {
        private string name;         // Имя студента
        private double averageGrade; // Средний балл

        public SecondYearStudent(string name, double averageGrade)
        {
            this.name = name;
            this.averageGrade = averageGrade;
        }

        public double GetAverageGrade() => averageGrade; // Возвращаем средний балл
        public int GetCourseNumber() => 2;               // Возвращаем номер курса (2)
        public string GetName() => name;                 // Возвращаем имя
    }

    class Program   // Главный класс программы
    {
        static void Main()
        {
            // Создаем список для хранения студентов
            List<IStudent> students = new List<IStudent>();

            Console.WriteLine(" Учет студентов ");

            // Ввод студента 1-го курса
            try
            {
                Console.Write("\nВведите имя студента 1 курса: ");
                string name1 = Console.ReadLine() ?? "Без имени";

                Console.Write("Введите средний балл: ");
                double grade1 = double.Parse(Console.ReadLine() ?? "0");

                // Добавление в общий список
                students.Add(new FirstYearStudent(name1, grade1));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Ввод студента 2-го курса
            try
            {
                Console.Write("\nВведите имя студента 2 курса: ");
                string name2 = Console.ReadLine() ?? "Без имени";

                Console.Write("Введите средний балл: ");
                double grade2 = double.Parse(Console.ReadLine() ?? "0");

                // Добавление в общий список
                students.Add(new SecondYearStudent(name2, grade2));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Вывод результатов
            Console.WriteLine("\n Результаты ");
            foreach (var student in students)
            {
                Console.WriteLine($"Студент: {student.GetName()}");
                Console.WriteLine($"Курс: {student.GetCourseNumber()}");
                Console.WriteLine($"Средний балл: {student.GetAverageGrade()}");
                Console.WriteLine(new string('-', 30));
            }
        }
    }
}