using System.Collections.Generic;
using System.Data.SQLite; 
using System.IO;

namespace TaskManagement
{
    public class TaskModel // Класс задачи
    {
        public int Id { get; set; }           // Уникальный идентификатор
        public string Title { get; set; }     // Название задачи
        public string Description { get; set; } // Описание задачи
        public string Status { get; set; }    // Статус задачи
    }

    public class TaskRepository // Класс логики, вся работа с базой данных только здесь
    {
        private const string ConnStr = "Data Source=tasks.db;Version=3;"; // Строка подключения для System.Data.SQLite

        public TaskRepository() // Конструктор репозитория
        {
            InitializeDatabase(); // Вызов метода безопасного создания файла и таблицы
        }

        private void InitializeDatabase() // Создать базу и таблицу
        {
            // Если файла базы еще нет, создаем его программно
            if (!File.Exists("tasks.db"))
            {
                SQLiteConnection.CreateFile("tasks.db"); // Создание файла базы
            }

            ExecuteQuery("CREATE TABLE IF NOT EXISTS Tasks (Id INTEGER PRIMARY KEY, Title TEXT NOT NULL, Description TEXT, Status TEXT NOT NULL);"); // Создание таблицы
        }

        public List<TaskModel> GetAll() // Получить все задачи
        {
            var list = new List<TaskModel>(); // Список для результата
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("SELECT * FROM Tasks", conn)) // Запрос на выборку всех задач
                {
                    using (var reader = cmd.ExecuteReader()) // Выполнение запроса
                    {
                        while (reader.Read()) // Чтение строк по одной
                        {
                            list.Add(new TaskModel // Добавление задачи в список
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Status = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return list; // Возврат списка задач
        }

        public void Save(TaskModel task) // Добавить или обновить задачу
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand(conn)) // Создание SQL-команды
                {
                    // Если ID равен нулю, то это новая задача
                    cmd.CommandText = task.Id == 0
                        ? "INSERT INTO Tasks (Title, Description, Status) VALUES (@t, @d, @s)"
                        : "UPDATE Tasks SET Title = @t, Description = @d, Status = @s WHERE Id = @id";

                    if (task.Id != 0) cmd.Parameters.AddWithValue("@id", task.Id); // Передача ID при обновлении
                    cmd.Parameters.AddWithValue("@t", task.Title); // Передача названия
                    cmd.Parameters.AddWithValue("@d", task.Description); // Передача описания
                    cmd.Parameters.AddWithValue("@s", task.Status); // Передача статуса
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

        public void Delete(int id) // Удалить задачу по ID
        {
            using (var conn = new SQLiteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SQLiteCommand("DELETE FROM Tasks WHERE Id = @id", conn)) // Запрос удаления с параметром
                {
                    cmd.Parameters.AddWithValue("@id", id); // Передача ID
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