using System;

class Program
{
    // Чтение целого положительного числа с проверкой ввода
    static int ReadSize(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            // Попытка преобразования строки в целое число больше 0
            if (int.TryParse(input, out int n) && n > 0)
            {
                return n;
            }

            // Сообщение об ошибке ввода
            Console.WriteLine("Ошибка: введите целое число больше 0.");
        }
    }

    // Вывод матрицы с суммами строк
    static void PrintMatrix(int[,] matrix, int[] sums)
    {
        int n = matrix.GetLength(0); // Размер матрицы (число строк)

        for (int i = 0; i < n; i++) // Перебор строк
        {
            for (int j = 0; j < n; j++) // Перебор элементов строки
            {  
                Console.Write($"{matrix[i, j],5}"); // Вывод элемента в поле шириной 5 символов
            }

            // Вывод суммы элементов строки
            Console.WriteLine($"   | {sums[i]}");
        }
    }

    static void Main()
    {
        // Ввод размера матрицы
        int n = ReadSize("Введите размер матрицы N: ");
        int[,] matrix = new int[n, n];
        Random random = new Random();

        // Заполнение матрицы случайными числами от -50 до 50
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = random.Next(-50, 51);
            }
        }

        // Подсчёт сумм элементов каждой строки
        int[] sums = new int[n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sums[i] += matrix[i, j];
            }
        }

        Console.WriteLine("\nИсходная матрица (справа суммы строк):");
        PrintMatrix(matrix, sums);

        // Сортировка строк пузырьком по возрастанию сумм
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (sums[j] > sums[j + 1])
                {
                    // Обмен местами сумм соседних строк
                    int tempSum = sums[j];
                    sums[j] = sums[j + 1];
                    sums[j + 1] = tempSum;

                    // Обмен местами самих строк матрицы
                    for (int k = 0; k < n; k++)
                    {
                        int temp = matrix[j, k];
                        matrix[j, k] = matrix[j + 1, k];
                        matrix[j + 1, k] = temp;
                    }
                }
            }
        }

        Console.WriteLine("\nМатрица после сортировки строк:");
        PrintMatrix(matrix, sums);
    }
}