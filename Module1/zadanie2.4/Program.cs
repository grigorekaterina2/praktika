using System;

class Program
{
    // Чтение целого числа с проверкой ввода
    static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            // Попытка преобразования строки в целое число
            if (int.TryParse(input, out int number))
            {
                return number;
            }

            // Сообщение об ошибке ввода
            Console.WriteLine("Ошибка: введите целое число.");
        }
    }

    static void Main()
    {
        // Ввод размера массива (больше 0)
        int k;
        do
        {
            k = ReadInt("Введите K (размер массива): ");
        } while (k <= 0);

        // Ввод границ диапазона [A, B) (B должно быть больше A)
        int a = ReadInt("Введите A: ");
        int b;
        do
        {
            b = ReadInt("Введите B (больше A): ");
        } while (b <= a);

        // Заполнение массива случайными числами из [A, B)
        int[] array = new int[k];
        Random random = new Random();

        for (int i = 0; i < k; i++)
        {
            array[i] = random.Next(a, b);
        }

        Console.WriteLine("Массив: " + string.Join(", ", array));

        // Поиск индексов минимального и максимального элементов
        int minIndex = 0;
        int maxIndex = 0;
        for (int i = 1; i < k; i++)
        {
            if (array[i] < array[minIndex])
            {
                minIndex = i;
            }

            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        Console.WriteLine($"Индекс минимального элемента: {minIndex}");
        Console.WriteLine($"Индекс максимального элемента: {maxIndex}");

        // Определение границ участка (индексы могут идти в любом порядке)
        int from = Math.Min(minIndex, maxIndex);
        int to = Math.Max(minIndex, maxIndex);

        // Вывод элементов между найденными, включая их
        Console.Write("Элементы между ними: ");
        for (int i = from; i <= to; i++)
        {
            Console.Write(array[i] + " ");
        }

        Console.WriteLine();
    }
}