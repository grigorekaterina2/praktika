using System;
using System.Collections.Generic;

class Book // Объявление класса Book, представляющего одну книгу
{
    public string Title { get; set; } = string.Empty; // Публичное свойство названия книги
    public string Author { get; set; } = string.Empty; // Автор
    public int Year { get; set; } // Год издания

    public Book(string title, string author, int year) // Конструктор класса Book для создания объекта с заданными параметрами
    {
        Title = title; // Запись переданного названия в свойство Title
        Author = author;
        Year = year;
    }

    public void DisplayInfo() // Вывод информации о книге
    {
        Console.WriteLine($"{Title} | Автор: {Author} | Год: {Year}");
    }
}

class HomeLibrary // Домашняя библиотека
{
    private List<Book> books = new List<Book>(); // Закрытый список для хранения всех книг 

    public int Count // Свойство для получения текущего количества книг 
    {
        get { return books.Count; }
    }

    public void AddBook(Book book) // Метод для добавления новой книги в библиотеку
    {
        books.Add(book); // Добавление переданного объекта книги в общий список
    }

    public int RemoveByTitle(string title) // Удаление по названию, возвращает число удалённых
    {
        return books.RemoveAll(b => string.Equals(b.Title, title, StringComparison.CurrentCultureIgnoreCase));
    }

    public List<Book> FindByAuthor(string author) // Поиск по автору 
    {
        return books.FindAll(b => b.Author.Contains(author, StringComparison.CurrentCultureIgnoreCase)); // Ищет книги, где имя автора содержит подстроку
    }

    public List<Book> FindByYear(int year) // Поиск по году издания
    {
        return books.FindAll(b => b.Year == year);
    }

    public void SortByTitle() // Сортировка по названию
    {
        books.Sort((a, b) => string.Compare(a.Title, b.Title, StringComparison.CurrentCultureIgnoreCase)); 
    }

    public void SortByAuthor() // Сортировка по автору
    {
        books.Sort((a, b) => string.Compare(a.Author, b.Author, StringComparison.CurrentCultureIgnoreCase)); // Ищет книги, где имя автора содержит подстроку
    }

    public void SortByYear() // Сортировка по году
    {
        books.Sort((a, b) => a.Year.CompareTo(b.Year)); // Возвращает список книг, у которых год совпадает с искомым
    }

    public void DisplayAll() // Вывод всех книг
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пуста");
            return;
        }

        foreach (Book book in books)
            book.DisplayInfo(); // Вызов метода вывода информации для каждой книги
    }
}

class Program
{
    // Цикл повторяется, пока пользователь не введет непустой текст
    static string ReadText(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            // Проверка на не пустую строку и не состоящую только из пробелов
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Строка не должна быть пустой!");
        }
    }

    // Проверка, что это число, оно больше 0 и не из будущего
    static int ReadYear(string message)
    {
        int year;
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out year) && year > 0 && year <= DateTime.Now.Year) // Проверка: это число, больше 0 и не из будущего
                return year;

            Console.WriteLine("Введите корректный год!");
        }
    }

    // Вывод списка найденных книг 
    static void ShowResults(List<Book> found)
    {
        if (found.Count == 0)
        {
            Console.WriteLine("Ничего не найдено");
            return;
        }

        // Вывод информации по каждому найденному объекту
        foreach (Book book in found)
            book.DisplayInfo();
    }

    static void Main(string[] args)
    {
        HomeLibrary library = new HomeLibrary(); // Создание объекта домашней библиотеки

        // Несколько книг для проверки
        library.AddBook(new Book("Евгений Онегин", "Александр Пушкин", 1833));
        library.AddBook(new Book("Война и мир", "Лев Толстой", 1869));
        library.AddBook(new Book("Преступление и наказание", "Фёдор Достоевский", 1866));

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Домашняя библиотека");
            Console.WriteLine("1 - Добавить книгу");
            Console.WriteLine("2 - Удалить книгу");
            Console.WriteLine("3 - Найти по автору");
            Console.WriteLine("4 - Найти по году");
            Console.WriteLine("5 - Сортировать");
            Console.WriteLine("6 - Показать все книги");
            Console.WriteLine("0 - Выход");
            Console.Write("Выбор: ");

            string? choice = Console.ReadLine();

            // Выход (также при закрытии ввода)
            if (choice == null || choice == "0")
                break;

            switch (choice)
            {
                case "1": // Добавление
                    {
                        string title = ReadText("Название: ");
                        string author = ReadText("Автор: ");
                        int year = ReadYear("Год издания: ");
                        library.AddBook(new Book(title, author, year));
                        Console.WriteLine("Книга добавлена");
                        break;
                    }
                case "2": // Удаление
                    {
                        string title = ReadText("Название книги для удаления: ");
                        int removed = library.RemoveByTitle(title);

                        if (removed > 0)
                            Console.WriteLine($"Удалено книг: {removed}");
                        else
                            Console.WriteLine("Книга не найдена");
                        break;
                    }
                case "3": // Поиск по автору
                    {
                        string author = ReadText("Автор (можно часть имени): ");
                        ShowResults(library.FindByAuthor(author));
                        break;
                    }
                case "4": // Поиск по году
                    {
                        int year = ReadYear("Год издания: ");
                        ShowResults(library.FindByYear(year));
                        break;
                    }
                case "5": // Сортировка
                    {
                        Console.WriteLine("1 - по названию, 2 - по автору, 3 - по году");
                        Console.Write("Выбор: ");
                        string? sortChoice = Console.ReadLine();

                        if (sortChoice == "1") library.SortByTitle();
                        else if (sortChoice == "2") library.SortByAuthor();
                        else if (sortChoice == "3") library.SortByYear();
                        else
                        {
                            Console.WriteLine("Нет такого пункта!");
                            break;
                        }

                        Console.WriteLine("Отсортировано:");
                        library.DisplayAll();
                        break;
                    }
                case "6": // Вывод всех книг
                    {
                        library.DisplayAll();
                        break;
                    }
                default:
                    Console.WriteLine("Нет такого пункта меню!");
                    break;
            }
        }
    }
}