using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PasswordManager
{
    public partial class MainWindow : Window // Главное окно приложения
    {
        PasswordStorage storage = new PasswordStorage(); // Экземпляр хранилища данных
        bool firstRun; // Флаг первого запуска программы
        DispatcherTimer clipboardTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) }; // Таймер очистки буфера

        public MainWindow()
        {
            InitializeComponent(); // Инициализация визуальных элементов
            grid.ItemsSource = storage.entries; // Привязка таблицы к коллекции записей
            firstRun = !storage.FileExists(); // Проверка наличия файла базы
            pnlConfirm.Visibility = firstRun ? Visibility.Visible : Visibility.Collapsed; // Показ подтверждения пароля при первом запуске
            clipboardTimer.Tick += (s, e) => { clipboardTimer.Stop(); try { Clipboard.Clear(); } catch { } }; // Событие таймера буфера
            pbMaster.Focus(); // Фокус на поле ввода мастер-пароля
        }

        void BtnLogin_Click(object sender, RoutedEventArgs e) // Обработка нажатия кнопки входа
        {
            string pass = pbMaster.Password; // Считывание мастер-пароля
            if (string.IsNullOrEmpty(pass)) { txtError.Text = "Введите мастер-пароль."; return; } // Проверка на пустоту
            if (firstRun)
            {
                if (pass != pbConfirm.Password) { txtError.Text = "Пароли не совпадают."; return; } // Проверка совпадения паролей
                storage.SetMaster(pass); // Сохранение мастер-пароля
            }
            else if (!storage.Open(pass))
            {
                txtError.Text = "Неверный мастер-пароль или файл повреждён."; // Ошибка открытия файла
                return;
            }
            pbMaster.Clear(); pbConfirm.Clear(); // Очистка полей паролей
            pnlLogin.Visibility = Visibility.Collapsed; // Скрытие панели входа
        }

        string CurrentPassword // Свойство для получения/установки текущего пароля
        {
            get => chkShow.IsChecked == true ? tbPasswordVisible.Text : pbPassword.Password; // В зависимости от видимости галочки
            set { pbPassword.Password = value; tbPasswordVisible.Text = value; } // Запись в оба поля
        }

        void ChkShow_Changed(object sender, RoutedEventArgs e) // Переключение видимости пароля
        {
            if (pbPassword == null || tbPasswordVisible == null) return; // Защита до инициализации
            bool show = chkShow.IsChecked == true; // Состояние чекбокса
            tbPasswordVisible.Visibility = show ? Visibility.Visible : Visibility.Collapsed; // Видимость текстового поля
            pbPassword.Visibility = show ? Visibility.Collapsed : Visibility.Visible; // Видимость PasswordBox
            if (show) tbPasswordVisible.Text = pbPassword.Password; // Копирование текста
            else pbPassword.Password = tbPasswordVisible.Text; // Возврат текста в скрытое поле
        }

        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор строки в таблице
        {
            if (grid.SelectedItem is Entry en) // Проверка, выбрана ли строка и приведение к типу Entry
            {
                tbSite.Text = en.Site; tbLogin.Text = en.Login; CurrentPassword = en.Password; // Заполнение полей данными строки
            }
        }

        bool ValidateInput() // Проверка заполненности полей ввода
        {
            if (string.IsNullOrWhiteSpace(tbSite.Text) || string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrEmpty(CurrentPassword))
            {
                MessageBox.Show("Заполните все поля."); // Предупреждение
                return false; // Поля пустые
            }
            return true; // Всё заполнено
        }

        void BtnAdd_Click(object sender, RoutedEventArgs e) // Кнопка добавления записи
        {
            if (!ValidateInput()) return; // Проверка полей
            storage.entries.Add(new Entry { Site = tbSite.Text.Trim(), Login = tbLogin.Text.Trim(), Password = CurrentPassword }); // Добавление в список
            storage.Save(); // Сохранение на диск

            // Очистка полей после успешного добавления
            tbSite.Clear();
            tbLogin.Clear();
            CurrentPassword = "";
            chkShow.IsChecked = false; // Возврат галочки "Показать" в исходное положение
        }

        void BtnEdit_Click(object sender, RoutedEventArgs e) // Кнопка изменения записи
        {
            if (!(grid.SelectedItem is Entry en)) { MessageBox.Show("Выберите запись в таблице."); return; } // Проверка выбора строки
            if (!ValidateInput()) return; // Проверка полей
            en.Site = tbSite.Text.Trim(); en.Login = tbLogin.Text.Trim(); en.Password = CurrentPassword; // Обновление данных
            grid.Items.Refresh(); storage.Save(); // Обновление таблицы и сохранение
        }

        void BtnDelete_Click(object sender, RoutedEventArgs e) // Кнопка удаления записи
        {
            if (grid.SelectedItem is Entry en && MessageBox.Show("Удалить выбранную запись?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                storage.entries.Remove(en); // Удаление из коллекции
                tbSite.Clear(); tbLogin.Clear(); CurrentPassword = ""; // Очистка полей ввода
                storage.Save(); // Сохранение изменений
            }
        }

        void BtnGenerate_Click(object sender, RoutedEventArgs e) // Кнопка генерации пароля
        {
            CurrentPassword = PasswordStorage.GeneratePassword(); // Генерация нового пароля
            chkShow.IsChecked = true; // Автоматическое включение показа пароля
        }

        void BtnCopy_Click(object sender, RoutedEventArgs e) // Кнопка копирования пароля в буфер
        {
            if (string.IsNullOrEmpty(CurrentPassword)) return; // Выход, если пароля нет
            Clipboard.SetText(CurrentPassword); // Копирование в буфер обмена
            clipboardTimer.Stop(); clipboardTimer.Start(); // Перезапуск таймера очистки
            MessageBox.Show("Пароль скопирован. Буфер обмена очистится через 15 секунд."); 
        }
    }
}