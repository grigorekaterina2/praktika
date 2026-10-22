using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace TaskManagerWpf
{
    public class Employee
    {
        public int Id { get; set; } // Уникальный идентификатор сотрудника
        public string FullName { get; set; } = ""; // Полное имя (ФИО) сотрудника
        public string Position { get; set; } = ""; // Должность сотрудника в компании
        public string Email { get; set; } = ""; // Электронная почта сотрудника

        public override string ToString() => FullName; // Переопределение метода для отображения ФИО в списке
    }

    public class Project
    {
        public int Id { get; set; } // Уникальный идентификатор проекта
        public string Title { get; set; } = ""; // Название проекта
        public string Description { get; set; } = ""; // Подробное описание целей проекта
        public DateTime StartDate { get; set; } = DateTime.Now; // Дата начала реализации проекта
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30); // Плановая дата завершения проекта
        public double Progress { get; set; }  // Вычисление процента выполнения проекта (от 0 до 100)
    }

    public class TaskItem
    {
        public int Id { get; set; } // Уникальный идентификатор задачи
        public int ProjectId { get; set; } // Внешний ключ: принадлежность задачи к проекту
        public int? EmpId { get; set; } // Внешний ключ: ID исполнителя задачи
        public string Title { get; set; } = ""; // Краткое название задачи
        public string Desc { get; set; } = ""; // Описание объема работ
        public string Status { get; set; } = "Новая"; // Текущий статус задачи ("Новая", "В работе", "Завершена", "Отменена")
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7); // Крайний срок выполнения (дедлайн)
        public string ProjTitle { get; set; } = ""; // Поле для вывода названия проекта в таблице
        public string EmpName { get; set; } = "Не назначен"; // Поле для вывода ФИО исполнителя в таблице
    }

    public class TaskManagementLogic
    {
        private readonly string _cs = "Data Source=tasks.db"; // Строка подключения к файлу базы данных SQLite

        public TaskManagementLogic() // Конструктор класса: проверка и создание таблиц в БД
        {
            using (var c = new SqliteConnection(_cs)) // Создание подключения через блок using
            {
                c.Open(); // Открытие соединения с файлом БД
                using (var cmd = c.CreateCommand()) // Создание объекта команды
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Projects(Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Description TEXT, StartDate TEXT, EndDate TEXT);" +
                                      "CREATE TABLE IF NOT EXISTS Employees(Id INTEGER PRIMARY KEY AUTOINCREMENT, FullName TEXT, Position TEXT, Email TEXT);" +
                                      "CREATE TABLE IF NOT EXISTS Tasks(Id INTEGER PRIMARY KEY AUTOINCREMENT, ProjectId INT, EmpId INT, Title TEXT, Desc TEXT, Status TEXT, DueDate TEXT);"; // SQL-скрипт создания трех таблиц с автоинкрементом
                    cmd.ExecuteNonQuery(); // Выполнение SQL-запроса структуры таблиц
                }
            }
        }

        public List<Project> GetProjects() // Метод извлечения списка всех проектов из базы данных
        {
            var list = new List<Project>(); // Создание пустого списка проектов
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = "SELECT * FROM Projects;"; // SQL-запрос выборки проектов
                    using (var r = cmd.ExecuteReader()) // Запуск построчного чтения
                    {
                        while (r.Read()) // Цикл обработки строк из БД
                        {
                            var p = new Project
                            {
                                Id = r.GetInt32(0), // Чтение ID
                                Title = r.GetString(1), // Чтение названия
                                Description = r.IsDBNull(2) ? "" : r.GetString(2), // Проверка NULL и чтение описания
                                StartDate = DateTime.Parse(r.GetString(3)), // Конвертация даты начала
                                EndDate = DateTime.Parse(r.GetString(4)) // Конвертация даты окончания
                            };
                            p.Progress = GetProgress(p.Id); // Вычисление процента выполнения проекта
                            list.Add(p); // Добавление проекта в список
                        }
                    }
                }
            }
            return list; // Возврат списка проектов
        }

        public void SaveProject(Project p) // Метод сохранения проекта (INSERT или UPDATE)
        {
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = p.Id == 0
                        ? "INSERT INTO Projects (Title, Description, StartDate, EndDate) VALUES (@t, @d, @s, @e);"
                        : "UPDATE Projects SET Title=@t, Description=@d, StartDate=@s, EndDate=@e WHERE Id=@id;"; // Выбор SQL-команды

                    if (p.Id != 0) cmd.Parameters.AddWithValue("@id", p.Id); // Передача ID при обновлении
                    cmd.Parameters.AddWithValue("@t", p.Title);      // Передача названия
                    cmd.Parameters.AddWithValue("@d", p.Description); // Передача описания
                    cmd.Parameters.AddWithValue("@s", p.StartDate.ToString("yyyy-MM-dd")); // Передача даты начала
                    cmd.Parameters.AddWithValue("@e", p.EndDate.ToString("yyyy-MM-dd"));   // Передача даты окончания
                    cmd.ExecuteNonQuery();                           // Выполнение запроса записи
                }
            }
        }

        public void DelProject(int id) { Exec("DELETE FROM Projects WHERE Id=" + id + ";"); } // Метод удаления проекта по ID

        public List<Employee> GetEmployees()  // Метод получения списка сотрудников
        {
            var list = new List<Employee>(); // Создание списка сотрудников
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = "SELECT * FROM Employees;"; // SQL-запрос выборки сотрудников
                    using (var r = cmd.ExecuteReader()) // Запуск чтения данных
                    {
                        while (r.Read()) // Итерация по строкам
                            list.Add(new Employee { Id = r.GetInt32(0), FullName = r.GetString(1), Position = r.GetString(2), Email = r.IsDBNull(3) ? "" : r.GetString(3) }); // Заполнение объекта
                    }
                }
            }
            return list; // Возврат списка сотрудников
        }

        public void SaveEmployee(Employee e) // Метод сохранения сотрудника
        {
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = e.Id == 0
                        ? "INSERT INTO Employees (FullName, Position, Email) VALUES (@n, @p, @m);"
                        : "UPDATE Employees SET FullName=@n, Position=@p, Email=@m WHERE Id=@id;"; // Выбор операции

                    if (e.Id != 0) cmd.Parameters.AddWithValue("@id", e.Id);
                    cmd.Parameters.AddWithValue("@n", e.FullName); // Привязка ФИО
                    cmd.Parameters.AddWithValue("@p", e.Position); // Привязка должности
                    cmd.Parameters.AddWithValue("@m", e.Email);  // Привязка почты
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

        public void DelEmployee(int id) { Exec("DELETE FROM Employees WHERE Id=" + id + ";"); } // Метод удаления сотрудника

        public List<TaskItem> GetTasks() // Метод получения задач с JOIN связями
        {
            var list = new List<TaskItem>(); // Создание списка задач
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = "SELECT t.Id, t.ProjectId, t.EmpId, t.Title, t.Desc, t.Status, t.DueDate, p.Title, e.FullName FROM Tasks t " +
                                      "JOIN Projects p ON t.ProjectId=p.Id LEFT JOIN Employees e ON t.EmpId=e.Id;"; // SQL JOIN запрос
                    using (var r = cmd.ExecuteReader()) // Чтение данных
                    {
                        while (r.Read()) // Обработка строк
                            list.Add(new TaskItem
                            {
                                Id = r.GetInt32(0),
                                ProjectId = r.GetInt32(1),
                                EmpId = r.IsDBNull(2) ? (int?)null : r.GetInt32(2), // Проверка сотрудника на NULL
                                Title = r.GetString(3),
                                Desc = r.IsDBNull(4) ? "" : r.GetString(4),
                                Status = r.GetString(5),
                                DueDate = DateTime.Parse(r.GetString(6)),
                                ProjTitle = r.GetString(7), // Название проекта
                                EmpName = r.IsDBNull(8) ? "Не назначен" : r.GetString(8) // Имя сотрудника или заглушка
                            });
                    }
                }
            }
            return list; // Возврат списка задач
        }

        public void SaveTask(TaskItem t) // Метод сохранения задачи
        {
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = t.Id == 0
                        ? "INSERT INTO Tasks VALUES (NULL, @pid, @eid, @t, @d, @st, @dt);"
                        : "UPDATE Tasks SET ProjectId=@pid, EmpId=@eid, Title=@t, Desc=@d, Status=@st, DueDate=@dt WHERE Id=@id;"; // Определение типа операции

                    if (t.Id != 0) cmd.Parameters.AddWithValue("@id", t.Id);
                    cmd.Parameters.AddWithValue("@pid", t.ProjectId); // ID проекта
                    cmd.Parameters.AddWithValue("@eid", (object)t.EmpId ?? DBNull.Value); // ID сотрудника или NULL
                    cmd.Parameters.AddWithValue("@t", t.Title); // Название задачи
                    cmd.Parameters.AddWithValue("@d", t.Desc); // Описание
                    cmd.Parameters.AddWithValue("@st", t.Status); // Статус
                    cmd.Parameters.AddWithValue("@dt", t.DueDate.ToString("yyyy-MM-dd")); // Дедлайн
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }

        public void DelTask(int id) { Exec("DELETE FROM Tasks WHERE Id=" + id + ";"); } // Метод удаления задачи

        private double GetProgress(int pid) // Метод расчета прогресса проекта
        {
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = "SELECT COUNT(*), SUM(CASE WHEN Status='Завершена' THEN 1 ELSE 0 END) FROM Tasks WHERE ProjectId=" + pid + ";"; // SQL агрегация
                    using (var r = cmd.ExecuteReader()) // Чтение результата
                    {
                        if (r.Read() && !r.IsDBNull(0) && r.GetInt32(0) > 0)
                        {
                            int totalTasks = r.GetInt32(0); // Общее число задач
                            int completedTasks = r.IsDBNull(1) ? 0 : r.GetInt32(1); // Число завершенных задач
                            return Math.Round((double)completedTasks / totalTasks * 100, 1); // Расчет процента с округлением
                        }
                    }
                }
            }
            return 0; // Возврат 0% при отсутствии задач
        }

        public string GetReport() // Метод генерации отчета
        {
            var sb = new StringBuilder("=== СВОДНЫЙ ОТЧЕТ ПО ПРОЕКТАМ И ЗАДАЧАМ ===\n\n");
            foreach (var p in GetProjects()) // Итерация по проектам
            {
                sb.AppendLine("📁 Проект: " + p.Title + " | Прогресс: " + p.Progress + "%");
                foreach (var t in GetTasks().FindAll(x => x.ProjectId == p.Id)) // Поиск задач проекта
                    sb.AppendLine("   - [" + t.Status + "] " + t.Title + " (Исполнитель: " + t.EmpName + ")");
                sb.AppendLine(); // Пустая строка разделения
            }
            return sb.ToString(); // Возврат текста отчета
        }

        private void Exec(string query) // Метод выполнения запросов без возврата данных
        {
            using (var c = new SqliteConnection(_cs)) // Создание подключения
            {
                c.Open(); // Открытие соединения
                using (var cmd = c.CreateCommand()) // Создание команды
                {
                    cmd.CommandText = query; // Передача текста запроса
                    cmd.ExecuteNonQuery(); // Выполнение запроса
                }
            }
        }
    }
}