using System;
using System.Collections.Generic;

// Делегат, переменная, в которой лежит метод фильтра, метод получает запись и значение, и говорит подходит запись или нет
delegate bool DataFilter(Record item, string value);


class Record // Класс с одной записью данных
{
    public string Title;     // Поле, текст записи
    public DateTime Date;    // Поле, дата записи

    public Record(string title, DateTime date) // Конструктор, записывает текст и дату в поля
    {
        Title = title;   // Кладём текст из параметра в поле Title
        Date = date;     // Кладём дату из параметра в поле Date
    }
}


class Program // Главный класс
{
    static bool ByDate(Record item, string value) // Метод, фильтр по дате, подходят записи начиная с этой даты
    {
        DateTime date;
        if (!DateTime.TryParse(value, out date)) // Превращение текста в дату
        {
            return false; // Если дата введена неверно
        }
        return item.Date >= date; // true если запись не старше этой даты
    }

    static bool ByKeyword(Record item, string value) // Метод, фильтр по ключевому слову
    {
        return item.Title.ToLower().Contains(value.ToLower()); // Приводим всё к маленьким буквам, чтобы регистр не мешал
    }
    
    static void Print(List<Record> records, DataFilter filter, string value) // Метод, показывает записи, которые прошли фильтр
    {
        int count = 0; // Сколько записей найдено
        foreach (Record item in records) // Берём записи из списка по одной, по кругу
            {
            if (filter(item, value)) // Вызов выбранного фильтра через делегат
            {
                Console.WriteLine(item.Date.ToShortDateString() + " " + item.Title); // Вывод короткой даты без времени и текст записи
                count++;
            }
        }
        if (count == 0)
        {
            Console.WriteLine("Ничего не найдено.");
        }
    }
    static void Main() // Метод Main, отсюда начинается программа
    {
        // Список с готовыми данными
        List<Record> records = new List<Record>();
        records.Add(new Record("Купить хлеб", new DateTime(2026, 9, 1)));
        records.Add(new Record("Сделать домашнее задание", new DateTime(2026, 9, 15)));
        records.Add(new Record("Купить билеты в кино", new DateTime(2026, 10, 1)));
        records.Add(new Record("Позвонить бабушке", new DateTime(2026, 10, 5)));
        records.Add(new Record("Сдать домашнее задание по C#", new DateTime(2026, 10, 10)));
 
        while (true) // Меню повторяется, пока не будет выбран выход
        {
            Console.WriteLine("\n1 - показать все записи");
            Console.WriteLine("2 - фильтр по дате");
            Console.WriteLine("3 - фильтр по ключевому слову");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? ""; // то, что ввёл пользователь

            switch (choice) // switch выбирает нужный case
            {
                case "1": // все записи, фильтр не нужен
                    foreach (Record item in records)
                    {
                        Console.WriteLine(item.Date.ToShortDateString() + " " + item.Title);
                    }
                    break;

                case "2": // фильтр по дате
                    Console.Write("Показать записи начиная с даты (например 01.10.2026): ");
                    string date = Console.ReadLine() ?? "";
                    Print(records, ByDate, date); // передаём метод ByDate
                    break;

                case "3": // фильтр по ключевому слову
                    Console.Write("Ключевое слово: ");
                    string word = Console.ReadLine() ?? "";
                    Print(records, ByKeyword, word); // передаём метод ByKeyword 
                    break;

                case "0": // выход
                    return; // конец Main, программа закрывается

                default: 
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }
}