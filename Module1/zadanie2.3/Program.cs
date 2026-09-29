using System;

class Program
{
    // Чтение целого положительного числа с проверкой ввода
    static int ReadCount(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            // Попытка преобразования строки в целое число больше 0
            if (int.TryParse(input, out int k) && k > 0)
            {
                return k;
            }

            // Сообщение об ошибке ввода
            Console.WriteLine("Ошибка: введите целое число больше 0.");
        }
    }

    // Проверка числа на простое
    static bool IsPrime(int n)
    {
        if (n < 2)
        {
            return false;
        }

        // Проверка делителей от 2 до квадратного корня из n
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    static void Main()
    {
        // Ввод количества простых чисел
        int k = ReadCount("Введите K: ");

        // Количество найденных простых чисел и текущее проверяемое число
        int found = 0;
        int number = 2;

        // Поиск простых чисел до получения K штук
        while (found < k)
        {
            if (IsPrime(number))
            {
                // Вывод числа, по 10 на строке
                Console.Write(number + "\t");
                found++;

                // Переход на новую строку после каждого десятого числа
                if (found % 10 == 0)
                {
                    Console.WriteLine();
                }
            }

            number++;
        }

        Console.WriteLine();
    }
}