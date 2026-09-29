using System;

class Program
{
    static void Main()
    {
        // Массив из 10 вещественных чисел из диапазона [-10, 10)
        double[] array = new double[10];
        Random random = new Random();

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.NextDouble() * 20 - 10;
        }

        // Массив индексов
        int[] indexes = new int[array.Length];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }

        // Сортировка индексов пузырьком по значениям элементов исходного массива
        for (int i = 0; i < indexes.Length - 1; i++)
        {
            for (int j = 0; j < indexes.Length - 1 - i; j++)
            {
                if (array[indexes[j]] > array[indexes[j + 1]])
                {
                    // Обмен местами соседних индексов
                    int temp = indexes[j];
                    indexes[j] = indexes[j + 1];
                    indexes[j + 1] = temp;
                }
            }
        }

        // Вывод исходного массива
        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine($"[{i}] = {array[i]:F3}");
        }

        // Вывод массива индексов
        Console.WriteLine("\nИндексы по возрастанию значений: " + string.Join(", ", indexes));

        // Вывод элементов в порядке возрастания через массив индексов
        Console.WriteLine("Элементы в порядке возрастания:");
        foreach (int index in indexes)
        {
            Console.WriteLine($"[{index}] = {array[index]:F3}");
        }
    }
}