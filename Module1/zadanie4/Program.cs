using System;

class Program
{
    static void Main()
    {
        int[] numbers = new int[10]; // Массив из 10 целых чисел

        Random random = new Random(); // Генератор случайных чисел
 
        int sum = 0; // Сумма всех элементов массива

        // Заполнение массива случайными числами от 1 до 100
        for (int i = 0; i < numbers.Length; i++)
        {
            // Присвоение случайного значения элементу массива
            numbers[i] = random.Next(1, 101);

            // Добавление элемента к общей сумме
            sum += numbers[i];
        }

        // Вывод элементов массива
        Console.WriteLine("Элементы массива: " + string.Join(", ", numbers));

        // Вывод суммы элементов
        Console.WriteLine($"Сумма элементов: {sum}");
    }
}