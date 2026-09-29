using System;

class Program
{  
    static double ReadNumber(string prompt) // Метод чтения числа с проверкой корректности ввода
    {
        // Бесконечный цикл: повтор запроса до получения корректного значения
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            // Попытка преобразования строки в число 
            if (double.TryParse(input, out double number))
            {
                // Возврат корректного числа и выход из метода
                return number;
            }

            // Сообщение об ошибке ввода
            Console.WriteLine("Ошибка: введите число. Попробуйте снова.");
        }
    }

    static void Main()
    {
        // Ввод трёх чисел пользователем
        double a = ReadNumber("Введите первое число: ");
        double b = ReadNumber("Введите второе число: ");
        double c = ReadNumber("Введите третье число: ");

        // Вычисление среднего арифметического: сумма чисел, делённая на их количество
        double average = (a + b + c) / 3;

        // Вывод результата на экран
        Console.WriteLine($"Среднее арифметическое: {average}");
    }
}