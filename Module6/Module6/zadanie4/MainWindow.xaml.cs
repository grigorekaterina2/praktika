using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace FinanceManagement
{
    public partial class MainWindow : Window
    {
        private readonly FinanceRepository _repo = new FinanceRepository(); // Объект для работы с базой данных
        public ObservableCollection<Record> Records { get; set; } = new ObservableCollection<Record>(); // Коллекция для таблицы
        private int? _id; // Идентификатор выбранной записи, null при добавлении

        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна
            dgRecords.ItemsSource = Records; // Привязка коллекции к таблице
            dpDate.SelectedDate = DateTime.Today; // Сегодняшняя дата по умолчанию
            LoadRecords(); // Загрузка записей из БД
        }

        private void LoadRecords() // Загрузить записи и итоги
        {
            Records.Clear(); // Очистка коллекции
            foreach (var r in _repo.GetAll()) // Перебор записей из базы
            {
                Records.Add(r); // Добавление записи в коллекцию
            }

            decimal income = _repo.GetTotal("Доход"); // Сумма доходов
            decimal expense = _repo.GetTotal("Расход"); // Сумма расходов
            tbIncome.Text = "Доходы: " + income; // Показ доходов
            tbExpense.Text = "Расходы: " + expense; // Показ расходов
            tbBalance.Text = "Баланс: " + (income - expense); // Показ баланса
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) // Нажатие кнопки сохранения
        {
            // Проверка категории, суммы и даты
            if (string.IsNullOrWhiteSpace(txtCategory.Text) || !decimal.TryParse(txtAmount.Text, out var amount) || amount <= 0 || dpDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните категорию, сумму больше нуля и дату.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string type = (cmbType.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Доход"; // Выбранный тип

            _repo.Save(new Record // Передача данных в слой работы с БД
            {
                Id = _id ?? 0, // Если ID нет, значит добавление
                Type = type, // Тип
                Category = txtCategory.Text.Trim(), // Категория
                Amount = amount, // Сумма
                Date = dpDate.SelectedDate.Value, // Дата
                Description = txtDescription.Text.Trim() // Описание
            });

            LoadRecords(); // Перезагрузка таблицы
            ClearForm(); // Очистка формы
        }

        private void DgRecords_SelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор строки в таблице
        {
            if (dgRecords.SelectedItem is Record r)
            {
                _id = r.Id; // Сохранение ID выбранной записи
                foreach (ComboBoxItem item in cmbType.Items) // Поиск нужного типа в списке
                {
                    if (item.Content.ToString() == r.Type) cmbType.SelectedItem = item; // Выбор типа
                }
                txtCategory.Text = r.Category; // Заполнение категории
                txtAmount.Text = r.Amount.ToString(); // Заполнение суммы
                dpDate.SelectedDate = r.Date; // Заполнение даты
                txtDescription.Text = r.Description; // Заполнение описания
                btnSave.Content = "Сохранить"; // Смена текста кнопки
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearForm(); // Кнопка очистки формы

        private void ClearForm() // Очистить форму
        {
            _id = null; // Сброс ID, режим добавления
            cmbType.SelectedIndex = 0; // Возврат первого типа
            txtCategory.Clear(); // Очистка категории
            txtAmount.Clear(); // Очистка суммы
            dpDate.SelectedDate = DateTime.Today; // Возврат сегодняшней даты
            txtDescription.Clear(); // Очистка описания
            btnSave.Content = "Добавить / Сохранить"; // Возврат текста кнопки
            dgRecords.UnselectAll(); // Снятие выделения в таблице
        }
    }
}