using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TaskPlanner
{
    public class TaskItem 
    {
        public string Title { get; set; } = ""; // Название
        public string Priority { get; set; } = "Средний"; // Приоритет
        public DateTime Due { get; set; } // Срок
        public int Progress { get; set; } // Процент
        public bool Notified { get; set; } // Флаг уведомления

        [XmlIgnore] public string DueText => Due.ToString("dd.MM.yyyy"); // Дата для таблицы
        [XmlIgnore] public bool IsOverdue => Progress < 100 && Due.Date < DateTime.Today; // Просрочка
    }

    public class TaskLogic // Логика задач
    {
        const string FileName = "tasks.xml"; // Файл данных
        static readonly XmlSerializer serializer = new XmlSerializer(typeof(List<TaskItem>)); // Запись и чтение XML
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>(); // Список задач

        public void Load() // Загрузка задач из файла
        {
            if (!File.Exists(FileName)) return; // Файла нет, выход
            using (var f = File.OpenRead(FileName)) // Открытие файла для чтения
                foreach (var t in (List<TaskItem>)serializer.Deserialize(f)) Tasks.Add(t); // Задачи из файла в список
        }

        public void Save() // Сохранение
        {
            using (var f = File.Create(FileName)) // Запись
                serializer.Serialize(f, Tasks.ToList()); // Список в файл
        }

        public void Add(string title, string priority, DateTime due, int progress) // Добавление
        {
            Tasks.Add(new TaskItem { Title = title, Priority = priority, Due = due, Progress = progress }); // Добавление новой задачи
            Save(); // Сохранение
        }

        public void Update(TaskItem t, string title, string priority, DateTime due, int progress) // Изменение
        {
            if (t.Due != due) t.Notified = false; // Сброс уведомления
            t.Title = title; // Новое название
            t.Priority = priority; // Новый приоритет
            t.Due = due; // Новый срок
            t.Progress = progress; // Новый процент
            Save(); // Сохранение
        }

        public void Remove(TaskItem t) // Удаление
        {
            Tasks.Remove(t); // Удаление
            Save(); // Сохранение
        }

        public List<TaskItem> TakeSoon() // Задачи для уведомления
        {
            var soon = Tasks.Where(t => t.Progress < 100 && !t.Notified
                                     && t.Due.Date <= DateTime.Today.AddDays(1)).ToList(); // Близкие сроки
            foreach (var t in soon) t.Notified = true; 
            if (soon.Count > 0) Save(); // Сохранение отметок
            return soon; // Результат
        }
    }
}