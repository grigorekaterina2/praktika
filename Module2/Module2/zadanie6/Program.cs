using System;
class BankAccount // Класс, представляющий банковский счет
{
    // Поля для хранения данных счета
    private string accountNumber;
    private string owner;
    private decimal balance;

    // Свойство для получения номера счета
    public string AccountNumber
    {
        get { return accountNumber; }
    }

    // Свойства для владельца и баланса
    public string Owner
    {
        get { return owner; }
        set { owner = value ?? string.Empty; }
    }

    public decimal Balance
    {
        get { return balance; }
    }

    // Конструктор по умолчанию (задает базовые значения)
    public BankAccount()
    {
        accountNumber = "Не указан";
        owner = "Не указан";
        balance = 0;
    }

    // Конструктор с параметрами для инициализации счета
    public BankAccount(string accountNumber, string owner, decimal initialBalance)
    {
        this.accountNumber = accountNumber; // Сохраняет переданный номер счета в поле класса
        this.owner = owner;

        // Начальный баланс не может быть отрицательным
        this.balance = initialBalance >= 0 ? initialBalance : 0;
    }

    // Метод для пополнения счета
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Успешно! Счет пополнен на {amount} руб. Текущий баланс: {balance} руб.");
        }
        else
        {
            Console.WriteLine("Ошибка: сумма пополнения должна быть больше нуля!");
        }
    }

    // Метод для снятия средств с проверкой достаточности денег
    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Успешно! Снято {amount} руб. Текущий баланс: {balance} руб.");
        }
        else if (amount > balance)
        {
            Console.WriteLine($"Ошибка: недостаточно средств! На балансе только {balance} руб.");
        }
        else
        {
            Console.WriteLine("Ошибка: сумма снятия должна быть больше нуля!");
        }
    }

    // Метод для вывода информации о счете в консоль
    public void DisplayInfo()
    {
        Console.WriteLine($"[Счет №{accountNumber}] Владелец: {owner}, Баланс: {balance} руб.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" Создание банковского счета ");

        // Ввод данных нового счета с клавиатуры
        Console.Write("Введите номер счета: ");
        string? accNumber = Console.ReadLine() ?? "0000";

        Console.Write("Введите имя владельца: ");
        string? ownerName = Console.ReadLine() ?? "Гость";

        // Ввод начального баланса с защитой от некорректного ввода
        decimal initialBalance;
        while (true)
        {
            Console.Write("Введите начальный баланс: ");
            if (decimal.TryParse(Console.ReadLine(), out initialBalance) && initialBalance >= 0)
            {
                break;
            }
            Console.WriteLine("Некорректный ввод! Баланс должен быть числом и не может быть отрицательным.");
        }

        // Создание объекта счета на основе введенных данных
        BankAccount myAccount = new BankAccount(accNumber, ownerName, initialBalance);

        Console.WriteLine("\nВаш счет успешно создан!");
        myAccount.DisplayInfo();

        // Основной цикл программы 
        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Пополнить счет");
            Console.WriteLine("2 - Снять средства");
            Console.WriteLine("3 - Посмотреть информацию о счете");
            Console.WriteLine("4 - Выход");
            Console.Write("Ваш выбор (1-4): ");

            string? choice = Console.ReadLine();

            if (choice == "4")
            {
                Console.WriteLine("Выход из программы. До свидания!");
                break;
            }

            // Обработка выбора пользователя
            switch (choice)
            {
                case "1":
                    Console.Write("Введите сумму для пополнения: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount)) // Проверка, что ввели число
                    {
                        myAccount.Deposit(depositAmount);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода суммы!");
                    }
                    break;

                case "2":
                    Console.Write("Введите сумму для снятия: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                    {
                        myAccount.Withdraw(withdrawAmount);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода суммы!");
                    }
                    break;

                case "3":
                    myAccount.DisplayInfo();
                    break;

                default: // Если введено что-то другое
                    Console.WriteLine("Неверный выбор! Пожалуйста, выберите пункт от 1 до 4.");
                    break;
            }
        }
    }
}