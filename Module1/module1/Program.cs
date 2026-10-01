using System;
class Program // Класс 
{
    static void Main()
    {
        Random random = new Random(); // Объект для генератора
        int targetNumber = random.Next(1, 101); // Переменная для хранения загаданного числа
        int userGuess = 0;  // Переменная для хранения ответа игрока
        int count = 0;   // Переменная для счетчика попыток

        Console.WriteLine(" Угадай число от 1 до 100 ");

        while (userGuess != targetNumber)
        {
            Console.Write("Введите число: ");
            string? input = Console.ReadLine(); // Переменная: ввод текста (с допуском null)

            // Проверка на null и на корректность ввода
            if (input == null || !int.TryParse(input, out userGuess))
            {
                Console.WriteLine("Ошибка: нужно ввести целое число!");
                continue;
            }

            count++; // Увеличение счетчика попыток 

            if (userGuess < targetNumber)
                Console.WriteLine("Загаданное число больше.");
            else if (userGuess > targetNumber)
                Console.WriteLine("Загаданное число меньше.");
            else
                Console.WriteLine($"Победа! Число: {targetNumber}. Попыток: {count}.");
        }
    }
}