using System;

class Program
{
    // Чтение целого положительного числа с проверкой
    static int ReadSize(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int n) && n > 0)
            {
                return n;
            }

            Console.WriteLine("Ошибка: введите целое число больше 0.");
        }
    }

    static void Main()
    {
        // Ввод размера массива
        int n = ReadSize("Введите размер массива N: ");
        double[] array = new double[n];
        Random random = new Random();

        // Генерация элементов от -100 до 100 и поиск максимального по модулю
        double maxAbs = 0;
        for (int i = 0; i < n; i++)
        {
            array[i] = random.Next(-100, 101);

            if (Math.Abs(array[i]) > maxAbs)
            {
                maxAbs = Math.Abs(array[i]);
            }
        }

        // Вывод исходного массива
        Console.WriteLine("Исходный массив: " + string.Join(", ", array));

        // Проверка на нулевой максимум
        if (maxAbs == 0)
        {
            Console.WriteLine("Все элементы равны 0, нормировка невозможна.");
            return;
        }

        // Нормировка элементов
        for (int i = 0; i < n; i++)
        {
            array[i] /= maxAbs;
        }

        // Вывод результата
        Console.WriteLine("Нормированный массив:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"[{i}] = {array[i]:F3}");
        }
    }
}