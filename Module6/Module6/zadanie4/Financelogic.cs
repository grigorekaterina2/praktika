using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization; // Нужно для правильной записи даты и чисел
using System.IO; // Нужно для проверки, есть ли файл базы

namespace FinanceManagement
{
    public class Record // Класс записи о доходе или расходе
    {
        public int Id { get; set; }             // Уникальный идентификатор
        public string Type { get; set; }        // Тип записи, Доход или Расход
        public string Category { get; set; }    // Категория
        public decimal Amount { get; set; }     // Сумма
        public DateTime Date { get; set; }      // Дата операции
        public string Description { get; set; } // Описание
    }

    public class FinanceRepository // Класс логики, вся работа с базой данных только здесь
    {
        private const string ConnStr = "Data Source=finance.db;Version=3;"; // Строка подключения к базе

        public FinanceRepository() // Конструктор репозитория
        {
            InitializeDatabase(); // Создание файла и таблицы
        }

        private void InitializeDatabase() // Создать базу и таблицу
        {
            // Если файла базы еще нет, создаем его программно
            if (!File.Exists("finance.db"))
            {
                SQLiteConnection.CreateFile("finance.db"); // Создание файла базы
            }

            ExecuteQuery("CREATE TABLE IF NOT EXISTS Records (Id INTEGER PRIMARY KEY, Type TEXT NOT NULL, Category TEXT NOT NULL, Amount REAL NOT NULL, Date TEXT NOT NULL, Description TEXT);"); // Создание таблицы
        }

        public List<Record> GetAll() // Получить все записи
        {
            var list = new List<Record>(); // Список для результата
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("SELECT * FROM Records ORDER BY Date DESC, Id DESC", conn)) // Запрос на выборку всех записей
                {
                    using (var reader = cmd.ExecuteReader()) // Выполнение запроса
                    {
                        while (reader.Read()) // Чтение строк по одной
                        {
                            list.Add(new Record // Добавление записи в список
                            {
                                Id = reader.GetInt32(0),
                                Type = reader.GetString(1),
                                Category = reader.GetString(2),
                                Amount = Convert.ToDecimal(reader.GetDouble(3)),
                                Date = DateTime.ParseExact(reader.GetString(4), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                Description = reader.IsDBNull(5) ? "" : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return list; // Возврат списка записей
        }

        public void Save(Record rec) // Добавить или обновить запись
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand(conn)) // Создание SQL-команды
                {
                    // Если ID равен нулю, то это новая запись
                    cmd.CommandText = rec.Id == 0
                        ? "INSERT INTO Records (Type, Category, Amount, Date, Description) VALUES (@t, @c, @a, @d, @desc)"
                        : "UPDATE Records SET Type = @t, Category = @c, Amount = @a, Date = @d, Description = @desc WHERE Id = @id";

                    if (rec.Id != 0) cmd.Parameters.AddWithValue("@id", rec.Id); // Передача ID при обновлении
                    cmd.Parameters.AddWithValue("@t", rec.Type); // Передача типа
                    cmd.Parameters.AddWithValue("@c", rec.Category); // Передача категории
                    cmd.Parameters.AddWithValue("@a", Convert.ToDouble(rec.Amount)); // Передача суммы
                    cmd.Parameters.AddWithValue("@d", rec.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)); // Передача даты
                    cmd.Parameters.AddWithValue("@desc", rec.Description); // Передача описания
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

        public decimal GetTotal(string type) // Получить сумму всех записей одного типа
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("SELECT IFNULL(SUM(Amount), 0) FROM Records WHERE Type = @t", conn)) // Запрос суммы
                {
                    cmd.Parameters.AddWithValue("@t", type); // Передача типа
                    return Convert.ToDecimal(cmd.ExecuteScalar()); // Возврат суммы
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