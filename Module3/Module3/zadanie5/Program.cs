using System;

// Делегат, переменная, в которой лежит метод сортировки. Метод получает массив чисел и сортирует его
delegate void SortMethod(int[] numbers);

class Program // Главный класс
{
    
    static int[] Generate(int count, Random random) // Метод, создаёт массив случайных чисел от 1 до 100
    {
        int[] result = new int[count]; // массив нужного размера
        for (int i = 0; i < count; i++)
        {
            result[i] = random.Next(1, 101); // случайное число от 1 до 100
        }
        return result;
    }
    
    static void BubbleSort(int[] numbers) // Метод, сортировка пузырьком
    {
        for (int i = 0; i < numbers.Length - 1; i++) // каждый проход ставит самое большое число в конец
        {
            for (int j = 0; j < numbers.Length - 1 - i; j++)
            {
                if (numbers[j] > numbers[j + 1]) // если соседи стоят неправильно
                {
                    int temp = numbers[j];       // меняем их местами
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
            }
        }
    }

    
    static void QuickSort(int[] numbers) // Метод, быстрая сортировка 
    {
        if (numbers.Length > 1) // с пустым массивом и одним числом делать нечего
        {
            Quick(numbers, 0, numbers.Length - 1);
        }
    }

    static void Quick(int[] numbers, int left, int right) // Метод, помощник быстрой сортировки, сортирует часть массива
    {
        int i = left;                              // идём слева направо
        int j = right;                             // идём справа налево
        int pivot = numbers[(left + right) / 2];   // опорное число из середины

        while (i <= j)
        {
            while (numbers[i] < pivot) i++;        // ищем слева число не меньше опорного
            while (numbers[j] > pivot) j--;        // ищем справа число не больше опорного
            if (i <= j)
            {
                int temp = numbers[i];             // меняем найденные числа местами
                numbers[i] = numbers[j];
                numbers[j] = temp;
                i++;
                j--;
            }
        }

        if (left < j) Quick(numbers, left, j);     // сортируем левую часть
        if (i < right) Quick(numbers, i, right);   // сортируем правую часть
    }

    static void Sort(int[] numbers, SortMethod method) // Метод, сортирует копию массива выбранным методом и показывает результат
    {
        int[] copy = (int[])numbers.Clone();       // копия, чтобы не потерять исходные числа
        method(copy);                              // вызываем выбранную сортировку через делегат
        Console.WriteLine("Результат: " + string.Join(" ", copy));
    }

    static void Main() // Метод Main, отсюда начинается программа
    {
        Random random = new Random(); // генератор случайных чисел

        int[] numbers = Generate(10, random); // Сразу создаём 10 чисел, чтобы было что сортировать
        Console.WriteLine("Создано 10 случайных чисел.");

        while (true) // Меню повторяется, пока не выберем выход
        {
            Console.WriteLine("\n1 - создать новые случайные числа");
            Console.WriteLine("2 - сортировка пузырьком");
            Console.WriteLine("3 - быстрая сортировка");
            Console.WriteLine("4 - показать числа");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? ""; // то, что ввёл пользователь

            switch (choice) // switch выбирает нужный case
            {
                case "1": // создание чисел
                    Console.Write("Сколько чисел создать: ");
                    int count;
                    if (int.TryParse(Console.ReadLine(), out count) && count > 0) // нужно целое число больше 0
                    {
                        numbers = Generate(count, random); // запоминание новых чисел
                        Console.WriteLine("Числа: " + string.Join(" ", numbers));
                    }
                    else
                    {
                        Console.WriteLine("Нужно целое число больше 0.");
                    }
                    break;

                case "2": // пузырёк
                    Sort(numbers, BubbleSort); // передаём метод BubbleSort
                    break;

                case "3": // быстрая сортировка
                    Sort(numbers, QuickSort); // передаём метод QuickSort
                    break;

                case "4": // показать числа
                    Console.WriteLine("Числа: " + string.Join(" ", numbers));
                    break;

                case "0": // выход
                    return; // конец Main, программа закрывается

                default: // ничего не подошло
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }
}