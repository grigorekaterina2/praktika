using System;

class Person // Объявление класса Person
{
    // Поля класса для хранения данных 
    private string name = string.Empty;   
    private int age;
    private string address = string.Empty; 

    // Свойство для работы с полем Name
    public string Name
    {
        get { return name; } // Возвращает значение имени
        set { name = value; } // Устанавливает новое значение имени
    }

    public int Age // Свойство для работы с полем Age
    {
        get { return age; } // Возврат значение возраста
        set
        {
            // Проверка на валидность возраста 
            if (value >= 0)
                age = value;
            else
                Console.WriteLine("Ошибка: Возраст не может быть отрицательным!");
        }
    }

    public string Address // Свойство для работы с полем Address
    {
        get { return address; }
        set { address = value; }
    }

    // Конструктор по умолчанию 
    public Person() { }

    // Конструктор с параметрами для быстрой инициализации объекта при создании
    public Person(string name, int age, string address)
    {
        Name = name;     // Использование свойства для присвоения значений 
        Age = age;
        Address = address;
    }

    public void DisplayInfo() // Метод для вывода информации о человеке в консоль
    {
        Console.WriteLine($"Имя: {Name}, Возраст: {Age}, Адрес: {Address}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание пустого объекта класса Person
        Person userPerson = new Person();

        Console.WriteLine("Ввод данных пользователя");

        // Ввод имени
        Console.Write("Введите имя: ");
        userPerson.Name = Console.ReadLine() ?? ""; 

        // Ввод возраста с проверкой на корректность ввода 
        int parsedAge;
        while (true)
        {
            Console.Write("Введите возраст: ");
            string? ageInput = Console.ReadLine(); 

            // Попытка преобразования строку в число
            if (int.TryParse(ageInput, out parsedAge) && parsedAge >= 0)
            {
                userPerson.Age = parsedAge;
                break;
            }
            else
            {
                Console.WriteLine("Некорректный ввод! Пожалуйста, введите целое неотрицательное число.");
            }
        }

        // Ввод адреса
        Console.Write("Введите адрес: ");
        userPerson.Address = Console.ReadLine() ?? ""; 

        // Вывод введенной информации
        Console.WriteLine("Введенная информация о человеке:");
        userPerson.DisplayInfo();
    }
}