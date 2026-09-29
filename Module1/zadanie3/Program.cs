using System;

class Program
{
    static void Main()
    {
        // Бесконечный цикл
        while (true)
        {
            // Запрос первой строки
            Console.Write("Введите первую строку (q для выхода): ");
            string? text = Console.ReadLine();

            // Проверка на выход
            if (text == null || text == "q")
            {
                break;
            }

            // Запрос второй строки
            Console.Write("Введите вторую строку: ");
            string? part = Console.ReadLine();

            // Проверка на конец ввода
            if (part == null)
            {
                break;
            }

            // Проверка вхождения второй строки в первую с учётом регистра
            if (text.Contains(part))
            {
                // Вывод результата при наличии вхождения
                Console.WriteLine($"Да, вторая строка является подстрокой первой.");
            }
            else
            {
                // Вывод результата при отсутствии вхождения
                Console.WriteLine($"Нет, вторая строка не является подстрокой первой.");
            }

            // Пустая строка для разделения проверок
            Console.WriteLine();
        }
    }
}