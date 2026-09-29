using System;

class Program
{
    // Чтение целого числа с проверкой: не меньше заданного минимума
    static int ReadInt(string prompt, int min)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            // Попытка преобразования строки в целое число не меньше min
            if (int.TryParse(input, out int number) && number >= min)
            {
                return number;
            }

            // Сообщение об ошибке ввода
            Console.WriteLine($"Ошибка: введите целое число не меньше {min}.");
        }
    }

    // Наибольший общий делитель по алгоритму Евклида
    static int Gcm(int a, int b)
    {
        while (b != 0)
        {
            int remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }

    static void Main()
    {
        // Ввод числителя и знаменателя 
        int numerator = ReadInt("Введите числитель: ", 0);
        int denominator = ReadInt("Введите знаменатель: ", 1);

        // Наибольший общий делитель числителя и знаменателя
        int gcm = Gcm(numerator, denominator);

        // Сокращение дроби на общий делитель
        int newNumerator = numerator / gcm;
        int newDenominator = denominator / gcm;

        // Вывод результата
        Console.WriteLine($"Сокращённая дробь: {newNumerator}/{newDenominator}");
    }
}