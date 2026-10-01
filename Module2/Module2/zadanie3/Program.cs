using System;

class Author // Класс Author
{
    // Поля класса для хранения данных
    private string name = string.Empty;
    private int birthYear;

    public string Name // Свойство для работы с именем
    {
        get { return name; }
        set { name = value; }
    }

    public int BirthYear // Свойство для работы с годом рождения
    {
        get { return birthYear; }
        set { birthYear = value; }
    }

    public Author(string name, int birthYear) // Конструктор принимает имя и год рождения
    {
        Name = name; // Присваивает имя через свойство Name
        BirthYear = birthYear;
    }

    public void DisplayInfo() // Вывод информации об авторе в консоль
    {
        Console.WriteLine($"Автор: {Name}, год рождения: {BirthYear}");
    }
}

class Book // Класс Book
{
    private string title = string.Empty; // Закрытое поле для названия книги
    private int year;
    private Author author; // Композиция: объект Author является частью объекта Book

    public string Title // Свойство для работы с названием
    {
        get { return title; }
        set { title = value; }
    }

    public int Year // Свойство для работы с годом выпуска
    {
        get { return year; }
        set { year = value; }
    }

    public Author Author // Свойство для доступа к автору книги
    {
        get { return author; }
        set { author = value; }
    }

    public Book(string title, int year, Author author) // Конструктор принимает название, год и автора
    {
        Title = title;
        Year = year;
        this.author = author;
    }

    public void DisplayInfo() // Вывод информации о книге вместе с автором
    {
        Console.WriteLine($"Книга: \"{Title}\", год выпуска: {Year}");
        Console.Write("  ");
        author.DisplayInfo(); // Книга обращается к своему автору
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание нескольких объектов Author
        Author author1 = new Author("Александр Пушкин", 1799);
        Author author2 = new Author("Лев Толстой", 1828);
        Author author3 = new Author("Фёдор Достоевский", 1821);

        // Создание нескольких объектов Book, каждая связана со своим автором
        Book book1 = new Book("Евгений Онегин", 1833, author1);
        Book book2 = new Book("Война и мир", 1869, author2);
        Book book3 = new Book("Преступление и наказание", 1866, author3);
        Book book4 = new Book("Анна Каренина", 1877, author2); // Тот же автор, что и у книги 2

        // Вывод информации
        book1.DisplayInfo();
        book2.DisplayInfo();
        book3.DisplayInfo();
        book4.DisplayInfo();
    }
}