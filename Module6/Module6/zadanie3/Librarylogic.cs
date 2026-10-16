using System;
using System.Collections.Generic;
using System.Data.SQLite; // Классическая библиотека для SQLite
using System.IO;

namespace LibraryManagement
{
    public class Book // Класс книги
    {
        public int Id { get; set; }            // Уникальный идентификатор
        public string Title { get; set; }      // Название книги
        public string Author { get; set; }     // Автор книги
        public int Year { get; set; }          // Год издания
        public string RentedBy { get; set; }   // Имя читателя, пусто если книга свободна
        public string RentDate { get; set; }   // Дата аренды
        public string Status => string.IsNullOrEmpty(RentedBy) ? "Свободна" : "Арендована"; // Статус книги для таблицы
    }

    public class BookRepository // Класс логики, вся работа с базой данных только здесь
    {
        private const string ConnStr = "Data Source=library.db;Version=3;"; // Строка подключения к базе

        public BookRepository() // Конструктор репозитория
        {
            InitializeDatabase(); // Создание файла, таблицы и первых книг
        }

        private void InitializeDatabase() // Создать базу и таблицу
        {
            // Если файла базы еще нет, создаем его программно
            if (!File.Exists("library.db"))
            {
                SQLiteConnection.CreateFile("library.db"); // Создание файла базы
            }

            ExecuteQuery("CREATE TABLE IF NOT EXISTS Books (Id INTEGER PRIMARY KEY, Title TEXT NOT NULL, Author TEXT NOT NULL, Year INTEGER NOT NULL, RentedBy TEXT, RentDate TEXT);"); // Создание таблицы

            if (GetAll().Count == 0) // Если книг еще нет, добавляем несколько для примера
            {
                AddBook(new Book { Title = "Война и мир", Author = "Лев Толстой", Year = 1869 });
                AddBook(new Book { Title = "Преступление и наказание", Author = "Фёдор Достоевский", Year = 1866 });
                AddBook(new Book { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = 1967 });
                AddBook(new Book { Title = "Евгений Онегин", Author = "Александр Пушкин", Year = 1833 });
                AddBook(new Book { Title = "Отцы и дети", Author = "Иван Тургенев", Year = 1862 });
            }
        }

        public List<Book> GetAll() // Получить все книги
        {
            var list = new List<Book>(); // Список для результата
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("SELECT * FROM Books", conn)) // Запрос на выборку всех книг
                {
                    using (var reader = cmd.ExecuteReader()) // Выполнение запроса
                    {
                        while (reader.Read()) // Чтение строк по одной
                        {
                            list.Add(new Book // Добавление книги в список
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Author = reader.GetString(2),
                                Year = reader.GetInt32(3),
                                RentedBy = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                RentDate = reader.IsDBNull(5) ? "" : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return list; // Возврат списка книг
        }

        public List<Book> Search(string text) // Поиск книг по названию или автору
        {
            var all = GetAll(); // Получение всех книг
            if (string.IsNullOrWhiteSpace(text)) return all; // Если поиск пустой, возвращаем все книги

            var result = new List<Book>(); // Список найденных книг
            foreach (var b in all) // Перебор всех книг
            {
                bool inTitle = b.Title.IndexOf(text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0; // Совпадение в названии
                bool inAuthor = b.Author.IndexOf(text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0; // Совпадение в авторе
                if (inTitle || inAuthor) result.Add(b); // Добавление подходящей книги
            }
            return result; // Возврат найденных книг
        }

        public void AddBook(Book book) // Добавить книгу
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("INSERT INTO Books (Title, Author, Year) VALUES (@t, @a, @y)", conn)) // Запрос добавления
                {
                    cmd.Parameters.AddWithValue("@t", book.Title); // Передача названия
                    cmd.Parameters.AddWithValue("@a", book.Author); // Передача автора
                    cmd.Parameters.AddWithValue("@y", book.Year); // Передача года
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

        public bool Rent(int id, string reader) // Арендовать книгу, возвращает false если она уже занята
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("UPDATE Books SET RentedBy = @r, RentDate = @d WHERE Id = @id AND RentedBy IS NULL", conn)) // Аренда только свободной книги
                {
                    cmd.Parameters.AddWithValue("@r", reader); // Передача имени читателя
                    cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("dd.MM.yyyy")); // Передача даты аренды
                    cmd.Parameters.AddWithValue("@id", id); // Передача ID книги
                    return cmd.ExecuteNonQuery() > 0; // Если строка изменилась, аренда прошла
                }
            }
        }

        public void ReturnBook(int id) // Вернуть книгу
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("UPDATE Books SET RentedBy = NULL, RentDate = NULL WHERE Id = @id", conn)) // Очистка данных аренды
                {
                    cmd.Parameters.AddWithValue("@id", id); // Передача ID книги
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

        private void ExecuteQuery(string query) // Выполнить запрос без результата
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand(query, conn)) // Создание команды
                {
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }
    }
}