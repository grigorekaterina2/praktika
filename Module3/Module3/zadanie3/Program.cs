using System;
using System.Collections.Generic;

// Делегат 
delegate void TaskAction(string title);


class TaskItem // Класс, который выполняет одну задачу
{
    public string Title;        // Поле, название задачи
    public TaskAction Action;   // Поле, метод для этой задачи

    public TaskItem(string title, TaskAction action) // Конструктор, записывает название и метод в поля
    {
        Title = title;     // Кладём название из параметра в поле Title
        Action = action;   // Кладём метод из параметра в поле Action
    }
}


class Program // Главный класс
{
    
    static void Notify(string title) // Метод - уведомление
    {
        Console.WriteLine("Уведомление отправлено: " + title);
    }

    
    static void Show(string title) // Метод - показать на экране
    {
        Console.WriteLine("Задача: " + title);
    }

    
    static void Main() // Метод Main, отсюда начинается программа
    {
        List<TaskItem> tasks = new List<TaskItem>(); // Список всех задач

        
        while (true) // Меню повторяется, пока не выберем выход
        {
            Console.WriteLine("\n1 - добавить задачу");
            Console.WriteLine("2 - показать задачи");
            Console.WriteLine("3 - выполнить все задачи");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? ""; // то, что ввёл пользователь

            switch (choice) // switch выбирает нужный case
            {
                case "1": // добавление задачи
                    Console.Write("Название задачи: ");
                    string title = Console.ReadLine() ?? "";

                    Console.Write("Действие (1 - уведомление, 2 - вывод на экран): ");
                    string action = Console.ReadLine() ?? "";

                    if (action == "1")
                    {
                        tasks.Add(new TaskItem(title, Notify)); // Добавление объекта в конец списка
                    }
                    else if (action == "2")
                    {
                        tasks.Add(new TaskItem(title, Show));
                    }
                    else
                    {
                        Console.WriteLine("Нужно 1 или 2. Задача не добавлена.");
                    }
                    break;

                case "2": // Показать задачи
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + tasks[i].Title); // номер равен i плюс 1
                    }
                    break;

                case "3": // Выполнить все задачи
                    foreach (TaskItem task in tasks)
                    {
                        task.Action(task.Title); // Вызов метода из делегата
                    }
                    break;

                case "0": // Выход
                    return; // Конец Main, программа закрывается

                default: // введено что-то другое
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }
}