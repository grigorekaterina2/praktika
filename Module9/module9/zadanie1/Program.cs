using System; 
using System.Collections.Generic; 

namespace StudentApp 
{
    internal class Program // главный класс, доступный только внутри проекта
    {
        private static readonly StudentManager Manager = new StudentManager(); // менеджер студентов 

        static void Main() // точка входа, начало работы программы
        {
            while (true) // бесконечный цикл показа меню до выбора выхода
            {
                Console.WriteLine("\n--- УПРАВЛЕНИЕ СТУДЕНТАМИ ---"); 
                Console.WriteLine("1. Список | 2. Добавить | 3. Редактировать | 4. Удалить | 5. Поиск | 6. Сортировка | 0. Выход");
                Console.Write("Выбор: "); 

                switch (Console.ReadLine()) // выбор действия по введённой строке
                {
                    case "1": ShowList(Manager.Students); break; // вывод всего списка студентов
                    case "2": Manager.AddOrUpdate(ReadStudent()); break; // ввод данных и добавление новой записи
                    case "3": EditStudent(); break; // редактирование записи по номеру
                    case "4": DeleteStudent(); break; // удаление записи по номеру
                    case "5": SearchStudents(); break; // поиск по ФИО или группе
                    case "6": SortStudents(); break; // сортировка списка
                    case "0": return; 
                    default: Console.WriteLine("Неверный пункт меню."); break; 
                }
            }
        }

        private static void ShowList(List<Student> list) // вывод переданного списка студентов на экран
        {
            if (list.Count == 0) { Console.WriteLine("Список пуст."); return; } // проверка на пустой список
            for (int i = 0; i < list.Count; i++) // перебор всех записей по порядку номеров
                Console.WriteLine($"[{i + 1}] {list[i]}"); // вывод строки студента с номером, начиная с единицы
        }

        private static Student ReadStudent() // ввод данных студента с клавиатуры
        {
            string name = ReadNonEmpty("Введите ФИО: "); 
            string group = ReadNonEmpty("Введите группу: ");
            double grade; // переменная для среднего балла

            while (true) // повтор ввода до верного значения
            {
                Console.Write("Введите средний балл (0 - 10): "); // приглашение к вводу балла с указанием границ
                if (double.TryParse(Console.ReadLine(), out grade) && grade >= 0 && grade <= 10) // проверка числа и диапазона от 0 до 10
                    break; 
                Console.WriteLine("Ошибка! Балл должен быть числом от 0 до 10."); 
            }

            return new Student { Name = name, Group = group, AverageGrade = grade }; // создание студента из введённых данных и возврат результата
        }

        private static string ReadNonEmpty(string prompt) // ввод строки, которая не бывает пустой
        {
            string? input; // переменная для введённой строки, допускающая пустое значение
            do // цикл с проверкой после ввода
            {
                Console.Write(prompt); // вывод приглашения, переданного в метод
                input = Console.ReadLine()?.Trim(); // чтение строки и удаление пробелов по краям
            } while (string.IsNullOrEmpty(input)); // повтор при пустой строке или отсутствии значения
            return input!; // возврат строки, восклицательный знак как подтверждение отсутствия пустого значения
        }

        private static void EditStudent() // редактирование записи студента
        {
            ShowList(Manager.Students); // вывод списка для выбора номера
            if (Manager.Students.Count == 0) return; // выход при пустом списке
            Console.Write("Номер записи для редактирования: "); 
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= Manager.Students.Count) // проверка числа и попадания номера в список
                Manager.AddOrUpdate(ReadStudent(), index - 1); // замена записи новыми данными, номер на единицу меньше индекса
        }

        private static void DeleteStudent() // удаление записи студента
        {
            ShowList(Manager.Students); // вывод списка для выбора номера
            if (Manager.Students.Count == 0) return; // выход при пустом списке
            Console.Write("Номер записи для удаления: "); 
            if (int.TryParse(Console.ReadLine(), out int index)) // проверка, что введено целое число
                Manager.RemoveAt(index - 1); // удаление записи по индексу, номер на единицу меньше индекса
        }

        private static void SearchStudents() // поиск студентов по строке
        {
            string q = ReadNonEmpty("Введите строку для поиска (ФИО или группа): "); 
            ShowList(Manager.Search(q)); // вывод найденных студентов
        }

        private static void SortStudents() // сортировка списка студентов
        {
            Console.Write("Сортировка (1 - по имени, 2 - по баллу): "); 
            if (int.TryParse(Console.ReadLine(), out int opt)) // проверка, что введено целое число
                Manager.Sort(opt); // сортировка по выбранному критерию
        }
    }
}