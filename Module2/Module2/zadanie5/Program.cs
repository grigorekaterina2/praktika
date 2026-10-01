using System;

// Датчик (он сообщает о событии)
class TemperatureSensor
{
    // Событие: температура изменилась
    public event Action<double>? TemperatureChanged;

    private double _temp; // Поле для хранеия текущей температуры

    public void Measure(double newTemp) // Метод для измерения и обновления температуры, принимающий новое значение
    {
        if (newTemp != _temp) // Проверка: изменилась ли температура по сравнению со старой
        {
            _temp = newTemp; // Обновление значения текущей температуры в поле
            TemperatureChanged?.Invoke(newTemp); // // Безопасный вызов события (если есть подписчики, отправляем им новое число через Invoke)
        }
    }
}

class Thermostat // Объявление класса термостата, который будет реагировать на изменение температуры
{
    public void OnTemperatureChanged(double temp) // Метод, принимающий текущую температуру
    {
        if (temp < 20)
            Console.WriteLine($"{temp}°C — температура ниже нормы, отопление ВКЛЮЧЕНО");
        else if (temp > 25)
            Console.WriteLine($"{temp}°C — температуры выше нормы, отопление ВЫКЛЮЧЕНО");
        else
            Console.WriteLine($"{temp}°C — норма, отопление ВЫКЛЮЧЕНО ");
    }
}

class Program
{
    static void Main()
    {
        var sensor = new TemperatureSensor(); // Создание объекта датчика температуры
        var thermostat = new Thermostat(); // Создание объекта термостата

        // Подписка на событие
        sensor.TemperatureChanged += thermostat.OnTemperatureChanged;

        Console.WriteLine("Вводите температуру (или 'exit' для выхода):");

        // Ввод, пока не exit
        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine(); // Ввод с клавиатуры

            // Выход
            if (input == "exit") break;

            // если число
            if (double.TryParse(input, out double temp))
                sensor.Measure(temp); // Отдаём число датчику
            else
                Console.WriteLine("Введите число!"); // Ошибка ввода
        }

        sensor.TemperatureChanged -= thermostat.OnTemperatureChanged; // Отписка от события
    }
}