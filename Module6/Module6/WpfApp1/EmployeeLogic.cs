using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace EmployeeManagement
{
    public class Employee
    {
        public int Id { get; set; }           // Уникальный идентификатор
        public string FullName { get; set; }  // ФИО сотрудника
        public string Position { get; set; }  // Должность
        public string Department { get; set; }// Отдел
        public decimal Salary { get; set; }   // Зарплата
    }

    public class EmployeeRepository // Класс логики, вся работа с базой данных только здесь
    {
        private const string ConnStr = "Data Source=employees.db;"; // Строка подключения к локальной базе данных SQLite

        public EmployeeRepository() // Конструктор репозитория
        {
            ExecuteQuery("CREATE TABLE IF NOT EXISTS Employees (Id INTEGER PRIMARY KEY, FullName TEXT NOT NULL, Position TEXT NOT NULL, Department TEXT NOT NULL, Salary REAL NOT NULL);"); // Создание таблицы при первом запуске
        }

        public List<Employee> GetAll() // Получить всех сотрудников
        {
            var list = new List<Employee>(); // Список для результата
            using (var conn = new SqliteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SqliteCommand("SELECT * FROM Employees", conn)) // Создание SQL-запроса на выборку всех записей
                {
                    using (var reader = cmd.ExecuteReader()) // Выполнение запроса и получение ридера данных
                    {
                        while (reader.Read()) // Построчное чтение результатов запроса
                        {
                            list.Add(new Employee // Добавление объекта сотрудника в список
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Position = reader.GetString(2),
                                Department = reader.GetString(3),
                                Salary = reader.GetDecimal(4)
                            });
                        }
                    }
                }
            }
            return list; // Возврат списка сотрудников
        }

        public void Save(Employee emp) // Добавить или обновить сотрудника
        {
            using (var conn = new SqliteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = conn.CreateCommand()) // Создание SQL-команды
                {
                    // Выбор запроса: INSERT (если новый) или UPDATE (если редактируемый)
                    cmd.CommandText = emp.Id == 0
                        ? "INSERT INTO Employees (FullName, Position, Department, Salary) VALUES (@n, @p, @d, @s)"
                        : "UPDATE Employees SET FullName = @n, Position = @p, Department = @d, Salary = @s WHERE Id = @id";

                    if (emp.Id != 0) cmd.Parameters.AddWithValue("@id", emp.Id); // Передача ID при редактировании
                    cmd.Parameters.AddWithValue("@n", emp.FullName); // Передача ФИО
                    cmd.Parameters.AddWithValue("@p", emp.Position); // Передача должности
                    cmd.Parameters.AddWithValue("@d", emp.Department); // Передача отдела
                    cmd.Parameters.AddWithValue("@s", emp.Salary); // Передача зарплаты
                    cmd.ExecuteNonQuery(); // Выполнение SQL-запроса изменения данных
                }
            }
        }

        public void Delete(int id) // Удалить сотрудника по ID
        {
            using (var conn = new SqliteConnection(ConnStr)) // Создание подключения к БД
            {
                conn.Open(); // Открытие соединения
                using (var cmd = new SqliteCommand("DELETE FROM Employees WHERE Id = @id", conn)) // Запрос удаления с параметром
                {
                    cmd.Parameters.AddWithValue("@id", id); // Передача ID
                    cmd.ExecuteNonQuery(); // Выполнение запроса удаления
                }
            }
        }

        private void ExecuteQuery(string query)
        {
            using (var conn = new SqliteConnection(ConnStr)) // Вспомогательный метод для выполнения разовых запросов без возврата данных
            {
                conn.Open();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery(); // Выполнение SQL-запроса (INSERT, UPDATE, DELETE или CREATE) без возврата данных
                }
            }
        }
    }
}   