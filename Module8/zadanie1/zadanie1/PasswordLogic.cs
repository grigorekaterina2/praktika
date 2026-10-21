using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager
{
    public class Entry // Класс одной записи пароля
    {
        public string Site { get; set; } // Сайт или сервис
        public string Login { get; set; } // Логин
        public string Password { get; set; } // Пароль
        public string Masked => "********"; // Маска для таблицы
    }

    public static class CryptoHelper // Класс шифрования
    {
        public static byte[] Encrypt(string plain, string master) // Метод шифрования текста
        {
            byte[] salt = new byte[16]; // Массив для соли
            using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(salt); // Генерация случайной соли
            using (var aes = Aes.Create()) // Создание AES шифра
            {
                aes.Key = new Rfc2898DeriveBytes(master, salt, 100000).GetBytes(32); // Генерация ключа из пароля
                aes.GenerateIV(); // Генерация вектора инициализации
                byte[] data = Encoding.UTF8.GetBytes("PM1\n" + plain); // Добавление метки к тексту
                using (var enc = aes.CreateEncryptor()) // Создание шифратора
                    return salt.Concat(aes.IV).Concat(enc.TransformFinalBlock(data, 0, data.Length)).ToArray(); // Сборка байт
            }
        }

        public static string Decrypt(byte[] file, string master) // Метод расшифровки файла
        {
            try
            {
                byte[] salt = file.Take(16).ToArray(), iv = file.Skip(16).Take(16).ToArray(), cipher = file.Skip(32).ToArray(); // Извлечение соли, IV и шифра
                using (var aes = Aes.Create()) // Создание AES шифра
                {
                    aes.Key = new Rfc2898DeriveBytes(master, salt, 100000).GetBytes(32); // Воссоздание ключа
                    aes.IV = iv; // Установка IV
                    using (var dec = aes.CreateDecryptor()) // Создание дешифратора
                    {
                        string text = Encoding.UTF8.GetString(dec.TransformFinalBlock(cipher, 0, cipher.Length)); // Расшифровка в текст
                        return text.StartsWith("PM1\n") ? text.Substring(4) : null; // Проверка метки и возврат текста
                    }
                }
            }
            catch { return null; } // Возврат null при ошибке расшифровки
        }
    }

    public class PasswordStorage // Класс управления хранилищем
    {
        readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "passwords.dat"); // Путь к файлу данных
        string master; // Мастер-пароль в памяти
        public ObservableCollection<Entry> entries { get; } = new ObservableCollection<Entry>(); // Коллекция записей для интерфейса

        public bool FileExists() => File.Exists(path); // Проверка существования файла
        public void SetMaster(string pass) => master = pass; // Установка мастер-пароля

        public bool Open(string pass) // Открытие и расшифровка файла
        {
            string plain = CryptoHelper.Decrypt(File.ReadAllBytes(path), pass); // Расшифровка файла байт
            if (plain == null) return false; // Ошибка, если пароль неверный
            entries.Clear(); // Очистка текущего списка
            foreach (var line in plain.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)) // Обход строк
            {
                var p = line.Split('|'); // Разделение по разделителю
                if (p.Length == 3) entries.Add(new Entry { Site = UnB64(p[0]), Login = UnB64(p[1]), Password = UnB64(p[2]) }); // Добавление записи
            }
            master = pass; // Запоминаем пароль
            return true;
        }

        public void Save() => File.WriteAllBytes(path, CryptoHelper.Encrypt(string.Join("\n", entries.Select(x => B64(x.Site) + "|" + B64(x.Login) + "|" + B64(x.Password))), master)); // Сериализация и сохранение

        string B64(string s) => Convert.ToBase64String(Encoding.UTF8.GetBytes(s ?? "")); // Кодирование в Base64
        string UnB64(string s) => Encoding.UTF8.GetString(Convert.FromBase64String(s)); // Декодирование из Base64

        public static string GeneratePassword() // Генератор случайного надежного пароля
        {
            const string chars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@#$%&*?"; // Набор символов
            var sb = new StringBuilder(); // Буфер для пароля
            using (var rng = new RNGCryptoServiceProvider()) // Криптографический генератор случайных чисел
            {
                byte[] b = new byte[1]; // Буфер для байта
                while (sb.Length < 16) // Генерация 16 символов
                {
                    rng.GetBytes(b); // Получение случайного байта
                    if (b[0] < 256 - 256 % chars.Length) sb.Append(chars[b[0] % chars.Length]); // Добавление символа
                }
            }
            return sb.ToString(); // Возврат готового пароля
        }
    }
}