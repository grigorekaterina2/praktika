using System;
using System.Collections.Generic;

class StringArray // Класс, массив строк фиксированной длины
{
    private string[] data; // Поле для хранения массива строк внутри объекта

    public int Length // Свойство для получения длины массива
    {
        get { return data.Length; } // Возвращает размер внутреннего массива
    }

    // Конструктор создает массив заданной длины из пустых строк
    public StringArray(int length)
    {
        // Длина должна быть больше нуля
        if (length <= 0)
            throw new ArgumentException("Длина массива должна быть положительной");

        // Заполнение пустыми строками вместо null
        data = new string[length];
        for (int i = 0; i < length; i++)
            data[i] = string.Empty; // Заполнение каждой ячейки пустой строкой
    }

    // Конструктор создает массив из готовых строк
    public StringArray(string[] items)
    {
        if (items.Length == 0)
            throw new ArgumentException("Длина массива должна быть положительной");

        // Копирование строк, null заменяется на пустую строку
        data = new string[items.Length];
        for (int i = 0; i < items.Length; i++)
            data[i] = items[i] ?? string.Empty; // Копирование строки
    }

    // Обращение к строке по индексу
    public string this[int index]
    {
        get
        {
            CheckIndex(index); // Проверка границ при чтении
            return data[index];
        }
        set
        {
            CheckIndex(index); // Проверка границ при записи
            data[index] = value ?? string.Empty; // Возврат элемента массива по указанному индексу
        }
    }

    // Контроль выхода за пределы массива
    private void CheckIndex(int index)
    {
        // Допустимые индексы от 0 до Length - 1
        if (index < 0 || index >= data.Length)
            throw new IndexOutOfRangeException($"Индекс {index} вне диапазона 0..{data.Length - 1}");
    }

    // Поэлементное сцепление
    // Если массивы разной длины, недостающие строки считаются пустыми
    public static StringArray Concat(StringArray a, StringArray b)
    {
        // Результат получает большую из двух длин
        int length = Math.Max(a.Length, b.Length); // Находим максимальную длину среди двух массивов
        StringArray result = new StringArray(length); // Создание нового результирующего объекта с этой длиной

        // Склейка строк с одинаковыми индексами
        for (int i = 0; i < length; i++)
        {
            string first = i < a.Length ? a[i] : string.Empty; // Если индекс в пределах первого массива, берем его элемент, иначе пустую строку
            string second = i < b.Length ? b[i] : string.Empty; // также для второго массива
            result[i] = first + second;
        }

        return result; // Исходные массивы не меняются
    }

    // Слияние без повторов, сначала все строки a, затем новые строки из b
    public static StringArray Merge(StringArray a, StringArray b)
    {
        List<string> unique = new List<string>(); // Список для строк без повторов

        // Строка попадает в список, только если ее там еще нет
        foreach (string s in a.data) // Обход всех элементов первого массива
            if (!unique.Contains(s)) // Если такой строки еще нет в списке unique
                unique.Add(s); // Добавление строки в список

        foreach (string s in b.data) // Обход всех элементов второго массива
            if (!unique.Contains(s)) // Если строки еще нет в уникальном списке
                unique.Add(s); // Добавляем ее

        return new StringArray(unique.ToArray()); // Из списка получается новый массив
    }

    public void PrintElement(int index) // Вывод элемента по индексу
    {
        Console.WriteLine($"[{index}] = \"{this[index]}\""); // Индекс сам проверяет границы
    }

    public void PrintAll() // Вывод всего массива
    {
        // Каждая строка выводится вместе с индексом
        for (int i = 0; i < data.Length; i++)
            Console.WriteLine($"[{i}] = \"{data[i]}\"");
    }
}

class Program
{
    // Набор слов для случайного заполнения 
    static string[] words = { "яблоко", "груша", "слива", "вишня", "банан" };

    // Создание массива случайной длины из случайных слов
    static StringArray CreateRandomArray(Random rnd)
    {
        int length = rnd.Next(3, 7); // Длина от 3 до 6
        StringArray array = new StringArray(length);

        // Каждая ячейка получает случайное слово
        for (int i = 0; i < length; i++)
            array[i] = words[rnd.Next(words.Length)]; // Случайное слово из набора

        return array;
    }

    static void Main(string[] args)
    {
        Random rnd = new Random(); // Генератор

        // Два случайных массива
        StringArray first = CreateRandomArray(rnd);
        StringArray second = CreateRandomArray(rnd);

        // Вывод исходных массивов
        Console.WriteLine("Первый массив:");
        first.PrintAll();
        Console.WriteLine("Второй массив:");
        second.PrintAll();

        // Сцепление по индексам
        Console.WriteLine();
        Console.WriteLine("Поэлементное сцепление:");
        StringArray.Concat(first, second).PrintAll();

        // Объединение без повторов
        Console.WriteLine();
        Console.WriteLine("Слияние без повторов:");
        StringArray.Merge(first, second).PrintAll();

        // Случайный индекс
        Console.WriteLine();
        int index = rnd.Next(-2, first.Length + 3); 
        Console.WriteLine($"Случайный индекс: {index}");

        // try/catch нужен, чтобы программа не падала при неверном индексе
        try
        {
            first.PrintElement(index); // Попытка вывести элемент по сгенерированному случайному индексу
        }
        catch (IndexOutOfRangeException ex) // Перехват ошибки выхода за пределы массива
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}