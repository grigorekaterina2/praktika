using System; 
using System.Collections.Generic; 
using System.IO;
using System.Linq; 

namespace StudentApp 
{
    public class Student // класс студента, хранилище данных об одном человеке
    {
        public string Name { get; set; } = string.Empty; // свойство ФИО, начальное значение пустая строка
        public string Group { get; set; } = string.Empty; // свойство группы, начальное значение пустая строка
        public double AverageGrade { get; set; } // свойство среднего балла, дробное число

        public override string ToString() => $"{Name} | Группа: {Group} | Средний балл: {AverageGrade:F2}"; 
    }

    public class StudentManager // класс менеджера, хранение списка и работа с файлом
    {
        private const string FilePath = "students.txt"; // константа с именем файла для хранения данных
        public List<Student> Students { get; private set; } = new List<Student>(); // список студентов, чтение снаружи и запись только внутри класса

        public StudentManager() => LoadFromFile(); // конструктор с загрузкой данных из файла при создании менеджера

        public void AddOrUpdate(Student student, int index = -1) // добавление или замена записи
        {
            if (index >= 0 && index < Students.Count) // проверка индекса на попадание в границы списка
                Students[index] = student; // замена записи по индексу новым студентом
            else 
                Students.Add(student); // добавление студента в конец списка
            SaveToFile(); // сохранение списка в файл после изменения
        }

        public bool RemoveAt(int index) // удаление записи по индексу 
        {
            if (index < 0 || index >= Students.Count) return false; // проверка индекса и возврат неудачи при выходе за границы
            Students.RemoveAt(index); // удаление записи из списка
            SaveToFile(); // сохранение списка в файл после изменения
            return true; // возврат успеха
        }


        public List<Student> Search(string query) => // поиск студентов 
            Students.Where(s => s.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || // по ФИО без учёта регистра букв
                               s.Group.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList(); // по группе без учёта регистра и превращение результата в список

        public void Sort(int criterion) // сортировка списка по номеру критерия
        {
            Students = criterion switch // выбор способа сортировки по номеру критерия
            {
                1 => Students.OrderBy(s => s.Name).ToList(), // порядок по ФИО от начала алфавита
                2 => Students.OrderByDescending(s => s.AverageGrade).ToList(), // порядок по баллу от большего к меньшему
                _ => Students // любой другой критерий, список без изменений
            };
            SaveToFile(); // сохранение нового порядка в файл
        }

        public void SaveToFile() // сохранение списка в файл
        {
            var lines = Students.Select(s => $"{s.Name};{s.Group};{s.AverageGrade}"); // превращение каждого студента в строку с разделителем точка с запятой
            File.WriteAllLines(FilePath, lines); // запись всех строк в файл с заменой старого содержимого
        }

        public void LoadFromFile() // загрузка списка из файла
        {
            if (!File.Exists(FilePath)) return; // проверка наличия файла и выход при его отсутствии
            Students.Clear(); // очистка текущего списка перед загрузкой
            foreach (var line in File.ReadAllLines(FilePath)) // перебор всех строк файла по одной
            {
                var parts = line.Split(';'); // разбиение строки на части по точке с запятой
                if (parts.Length == 3 && double.TryParse(parts[2], out double grade)) // проверка числа частей и перевод третьей части в число
                    Students.Add(new Student { Name = parts[0], Group = parts[1], AverageGrade = grade }); // создание студента из частей строки и добавление в список
            }
        }
    }
}