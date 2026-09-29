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

    static void Main()
    {
        // Русский алфавит (строчные буквы) и набор гласных
        string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string vowels = "аеёиоуыэюя";

        // Ввод размера массива
        int k = ReadCount("Введите K: ");

        // Заполнение символьного массива случайными буквами
        char[] letters = new char[k];
        Random random = new Random();

        for (int i = 0; i < k; i++)
        {
            letters[i] = alphabet[random.Next(alphabet.Length)];
        }

        // Подсчёт согласных для определения размера нового массива
        int count = 0;
        foreach (char c in letters)
        {
            // Согласная: не гласная и не буква ъ, ь
            if (!vowels.Contains(c) && c != 'ъ' && c != 'ь')
            {
                count++;
            }
        }

        // Перенос согласных букв в новый массив
        char[] consonants = new char[count];
        int index = 0;
        foreach (char c in letters)
        {
            if (!vowels.Contains(c) && c != 'ъ' && c != 'ь')
            {
                consonants[index] = c;
                index++;
            }
        }

        // Вывод обоих массивов
        Console.WriteLine("Исходный массив: " + string.Join(" ", letters));
        Console.WriteLine("Массив согласных: " + string.Join(" ", consonants));
    }
}