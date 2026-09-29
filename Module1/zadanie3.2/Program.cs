using System;

class Program
{
    // Чтение целого положительного числа с проверкой ввода
    static int ReadPositive(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number) && number > 0)
            {
                return number;
            }

            Console.WriteLine("Ошибка: введите целое число больше 0.");
        }
    }

    static void Main()
    {
        // Ввод заданной суммы
        int target = ReadPositive("Введите число: ");

        // Минимальное количество элементов 
        int count = (target + 8) / 9;
        int[] array = new int[count];
        Random random = new Random();

        // Остаток суммы, который ещё нужно набрать
        int remaining = target;

        // Заполнение массива случайными значениями с точной итоговой суммой
        for (int i = 0; i < count; i++)
        {
            int left = count - i - 1;
            int min = Math.Max(1, remaining - 9 * left);
            int max = Math.Min(9, remaining - left);

            array[i] = random.Next(min, max + 1);
            remaining -= array[i];
        }

        // Вывод результата
        Console.WriteLine("Массив: " + string.Join(", ", array));
        Console.WriteLine($"Количество элементов: {array.Length}");
        Console.WriteLine($"Сумма: {target}");
    }
}