using System;

class Program
{
    static void Main()
    {
        // Перебор чисел от 1 до 100
        for (int i = 1; i <= 100; i++)
        {
            // Деление и на 3, и на 5 
            if (i % 3 == 0 && i % 5 == 0)
            {
                Console.WriteLine("FizzBuzz");
            }
            else if (i % 3 == 0) // Деление только на 3
            {
                Console.WriteLine("Fizz");
            } 
            else if (i % 5 == 0) // Деление только на 5
            {
                Console.WriteLine("Buzz");
            }
            // Отсутствие делимости на 3 и на 5
            else
            {  
                Console.WriteLine(i); // Вывод самого числа
            }
        }
    }
}