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
        // Массив из 10 случайных чисел от 1 до 100
        int[] array = new int[10];
        Random random = new Random();

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(1, 101);
        }

        // Вывод исходного массива
        Console.WriteLine("Исходный массив: " + string.Join(", ", array));

        // Ввод числа для замены
        int value = ReadInt("Введите целое число: ");

        // Поиск индекса максимального элемента
        int maxIndex = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        // Замена максимального элемента введённым числом
        array[maxIndex] = value;

        // Вывод изменённого массива
        Console.WriteLine("Измененный массив: " + string.Join(", ", array));
    }
}