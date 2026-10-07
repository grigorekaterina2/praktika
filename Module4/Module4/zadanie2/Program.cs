using System;
using System.Collections.Generic; // Пространство имен для коллекций (списков, массивов)

namespace StoreInventory
{
    // Создание интерфейса «Товар». Это контракт, который обязывает классы реализовать указанные методы
    interface IProduct
    {
        double CalculateTotalCost(); // Метод для определения общей стоимости товара на складе
        int GetStockBalance();       // Метод для определения остатка товара на складе
        string GetName();            // Метод получения имени 
    }

    // Класс «Штучный товар», реализующий интерфейс IProduct
    class PieceProduct : IProduct
    {
        private string name;     // Название товара (поле)
        private double price;    // Цена за единицу
        private int quantity;    // Количество на складе (остаток)

        // Конструктор штучного товара для инициализации полей
        public PieceProduct(string name, double price, int quantity)
        {
            // 'this' для обращения к полю объекта, отличая его от параметра, запись параметра в поле
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }

        public int GetStockBalance() // Возвращает текущий остаток товара на складе
        {
            return quantity; // Возвращаем значение поля quantity
        }

        public double CalculateTotalCost() // Вычисляет общую стоимость товара (цена * количество)
        {
            return price * quantity;
        }

        public string GetName() => name; // Возвращает название штучного товара
    }

    // Класс «Весовой товар», реализующий интерфейс IProduct
    class WeightProduct : IProduct
    {
        private string name;       // Название товара (поле)
        private double pricePerKg; // Цена за 1 кг
        private double weightKg;   // Вес на складе в килограммах 

        // Конструктор весового товара для инициализации полей
        public WeightProduct(string name, double pricePerKg, double weightKg)
        {
            // 'this' для сохранения переданных аргументов в поля текущего объекта
            this.name = name;
            this.pricePerKg = pricePerKg;
            this.weightKg = weightKg;
        }

        public int GetStockBalance() // Для весового товара остаток в штуках возвращает как целую часть килограммов
        {
            return (int)weightKg; // Превращение дробного веса в целое число
        }

        public double CalculateTotalCost() // Вычисляет общую стоимость весового товара 
        {
            return pricePerKg * weightKg;
        }

        public string GetName() => name; // Возвращает название весового товара
    }

    class Program // Главный класс программы
    {
        static void Main()
        {
            // Создаем пустой список для динамического добавления товаров, введенных пользователем
            List<IProduct> products = new List<IProduct>();

            Console.WriteLine(" Учет товаров в магазине ");

            // Блок try-catch для безопасного ввода данных штучного товара и отлова возможных ошибок
            try
            {
                Console.WriteLine("\n Ввод штучного товара ");
                Console.Write("Введите название товара: ");

                // Используем ?? "Без названия", если пользователь ничего не ввел
                string pieceName = Console.ReadLine() ?? "Без названия";

                Console.Write("Введите цену за единицу: ");
                // Считывание строки с защитой от null и преобразование в дробное число
                double piecePrice = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Введите количество на складе (шт): ");
                // Считывание строки с защитой от null и преобразование в целое число
                int pieceQuantity = int.Parse(Console.ReadLine() ?? "0");

                // Добавление созданного штучного товара в общий список
                products.Add(new PieceProduct(pieceName, piecePrice, pieceQuantity));
            }
            catch (Exception ex) // Перехват исключений
            {
                Console.WriteLine($"Ошибка при вводе штучного товара: {ex.Message}");
            }

            // Блок try-catch для безопасного ввода данных весового товара
            try
            {
                Console.WriteLine("\n Ввод весового товара");
                Console.Write("Введите название товара: ");
                // Считываем название весового товара с подстраховкой
                string weightName = Console.ReadLine() ?? "Без названия";

                Console.Write("Введите цену за 1 кг: ");
                double pricePerKg = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Введите вес на складе (кг): ");
                double weightKg = double.Parse(Console.ReadLine() ?? "0");

                // Добавляем созданный весовой товар в общий список
                products.Add(new WeightProduct(weightName, pricePerKg, weightKg));
            }
            catch (Exception ex) // Обработка ошибок ввода для весового товара
            {
                Console.WriteLine($"Ошибка при вводе весового товара: {ex.Message}");
            }

            // Заголовок для вывода результатов по всем успешно введенным товарам
            Console.WriteLine("\n Результаты учета на складе ");

            // Перебор каждого товара в цикле foreach 
            foreach (var product in products)
            {
                // Вызываем метод GetName() напрямую из интерфейса для получения имени текущего товара
                Console.WriteLine($"Товар: {product.GetName()}");

                // Вызываем метод интерфейса для получения остатка на складе
                Console.WriteLine($"Остаток на складе: {product.GetStockBalance()}");

                // Вычисляем общую стоимость, округляем до 2 знаков и выводим
                Console.WriteLine($"Общая стоимость: {Math.Round(product.CalculateTotalCost(), 2)} руб.");

                Console.WriteLine(new string('-', 30));
            }
        }
    }
}