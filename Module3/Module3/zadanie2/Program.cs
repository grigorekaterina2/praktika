using System;
class Notification // Класс с событиями для трёх типов уведомлений
{
    public event Action<string>? MessageReceived; // Событие: получение сообщения
    public event Action<string>? CallReceived; // Событие: входящий звонок
    public event Action<string>? EmailReceived; // Событие: получение письма

    // Отправка уведомлений: запуск событий (если есть подписчики)
    public void SendMessage(string text) { MessageReceived?.Invoke(text); }
    public void SendCall(string text) { CallReceived?.Invoke(text); }
    public void SendEmail(string text) { EmailReceived?.Invoke(text); }
}

class Program // Основной класс
{
    // Обработчики событий: реакция на каждый тип уведомления
    static void OnMessage(string text)
    {
        Console.WriteLine("Сообщение: " + text); // Вывод текста сообщения
    }

    static void OnCall(string text)
    {
        Console.WriteLine("Входящий звонок от: " + text); // Вывод имени звонящего
    }

    static void OnEmail(string text)
    {
        Console.WriteLine("Пришло письмо от " + text); // Вывод темы письма
    }

    // Второй обработчик для сообщений: имитация звукового сигнала
    static void OnMessageSound(string text)
    {
        Console.WriteLine("Звуковой сигнал: новое сообщение");
    }

    // Ввод текста уведомления с клавиатуры
    static string ReadText()
    {
        Console.Write("Текст (от кого): ");
        return Console.ReadLine() ?? ""; // Пустая строка, если ввода нет
    }

    static void Main()
    {
        Notification notification = new Notification(); // Объект с событиями

        // Подписка обработчиков на события
        notification.MessageReceived += OnMessage;
        notification.MessageReceived += OnMessageSound; // Два обработчика на одно событие
        notification.CallReceived += OnCall;
        notification.EmailReceived += OnEmail;

        bool running = true; // Признак работы программы

        // Цикл меню: повтор до выбора выхода
        while (running)
        {
            Console.WriteLine("\n1 - сообщение");
            Console.WriteLine("2 - звонок");
            Console.WriteLine("3 - электронное письмо");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? ""; // Чтение пункта меню

            // Выбор действия по пункту меню
            switch (choice)
            {
                case "1": // Отправка сообщения
                    notification.SendMessage(ReadText());
                    break;

                case "2": // Отправка звонка
                    notification.SendCall(ReadText());
                    break;

                case "3": // Отправка письма
                    notification.SendEmail(ReadText());
                    break;

                case "0": // Выход из программы
                    running = false;
                    break;

                default: // Любой другой ввод
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }
}