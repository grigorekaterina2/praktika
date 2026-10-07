using System;
using System.Collections.Generic; // Пространство имен для списков

namespace LibrarySystem
{
    // Создание интерфейса «Книга» 
    interface IBook
    {
        bool IsAvailable();      // Проверка доступности книги
        void IssueBook();        // Выдача книги читателю
        string GetTitle();       // Получение названия книги
    }

    // Класс «Печатная книга» 
    class PrintedBook : IBook
    {
        private string title;      // Название книги
        private bool available;    // Статус доступности (true — есть в наличии, false — выдана)

        // Конструктор печатной книги для инициализации полей
        public PrintedBook(string title, bool available)
        {
            this.title = title;          // Сохранение названия в поле
            this.available = available;  // Сохранение статуса доступности
        }

        public bool IsAvailable() // Проверка доступности книги
        {
            return available;
        }

        public void IssueBook() // Выдача печатной книги (смена статуса на false)
        {
            // Проверка доступности книги
            if (available)
            {
                available = false; // Изменение статуса доступности
                Console.WriteLine($"Печатная книга '{title}' успешно выдана на руки."); // Вывод сообщения об успешной выдаче
            }
            else // Альтернатива при отсутствии книги
            {
                Console.WriteLine($"Книга '{title}' уже выдана!"); // Вывод сообщения о недоступности
            }
        }

        public string GetTitle() => title; // Возврат названия книги
    }

    // Класс «Электронная книга» 
    class EBook : IBook
    {
        private string title;      // Название книги (поле)

        public EBook(string title) // Конструктор электронной книги для инициализации названия
        {
            this.title = title;
        }

        public bool IsAvailable() // Доступность электронной книги всегда
        {
            return true;
        }

        public void IssueBook() // Выдача электронной книги
        {
            Console.WriteLine($"Электронная книга '{title}' отправлена на вашу почту (ссылка для скачивания).");
        }

        public string GetTitle() => title; // Возврат названия книги
    }

    class Program // Главный класс программы
    {
        static void Main()
        {
            // Создание списка книг с общим типом интерфейса IBook
            List<IBook> library = new List<IBook>();

            Console.WriteLine(" Библиотека книг ");

            // Блок try-catch для безопасного ввода данных печатной книги
            try
            {
                Console.WriteLine("\n Добавление печатной книги ");
                Console.Write("Введите название книги: ");
                string title1 = Console.ReadLine() ?? "Без названия";

                // Добавление печатной книги 
                library.Add(new PrintedBook(title1, true));
            }
            catch (Exception ex) // Обработка возможных исключений при вводе
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Блок try-catch для безопасного ввода данных электронной книги
            try
            {
                Console.WriteLine("\n Добавление электронной книги ");
                Console.Write("Введите название электронной книги: ");
                string title2 = Console.ReadLine() ?? "Без названия";

                // Добавление электронной книги
                library.Add(new EBook(title2));
            }
            catch (Exception ex) // Обработка возможных исключений при вводе
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Заголовок для проверки доступности и выдачи книг из списка
            Console.WriteLine("\n Выдача книг читателям ");

            // Перебор каждой книги в цикле
            foreach (var book in library)
            {
                Console.WriteLine($"\nКнига: {book.GetTitle()}");

                // Проверка доступности через метод интерфейса
                if (book.IsAvailable())
                {
                    Console.WriteLine("Статус: Доступна");
                    book.IssueBook(); // Выдача книги
                }
                else
                {
                    Console.WriteLine("Статус: Нет в наличии");
                }

                Console.WriteLine(new string('-', 30)); 
            }
        }
    }
}