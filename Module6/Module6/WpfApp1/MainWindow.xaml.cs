using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagement
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeRepository _repo = new EmployeeRepository(); // Объект для работы с базой данных
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>(); // Коллекция для автоматического обновления таблицы 
        private int? _id; // Идентификатор выбранного сотрудника (null, если режим добавления)

        public MainWindow()
        {
            InitializeComponent(); // Инициализация графических компонентов окна
            LoadEmployees(); // Загрузка данных из БД в коллекцию
            dgEmployees.ItemsSource = Employees; // Привязка коллекции к таблице на форме
        }

        private void LoadEmployees() // Загрузить сотрудников
        {
            Employees.Clear(); // Очистка коллекции перед заполнением
            foreach (var emp in _repo.GetAll()) // Перебор сотрудников из базы
            {
                Employees.Add(emp); // Добавление сотрудника в коллекцию
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) // Событие сохранения
        {
            // Проверка заполнения полей и корректности ввода зарплаты
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || !decimal.TryParse(txtSalary.Text, out var sal))
            {
                MessageBox.Show("Заполните все поля корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.Save(new Employee // Передача данных в слой работы с БД
            {
                Id = _id ?? 0, // Если ID нет, значит добавление
                FullName = txtFullName.Text.Trim(), // ФИО
                Position = txtPosition.Text.Trim(), // Должность
                Department = txtDepartment.Text.Trim(), // Отдел
                Salary = sal // Зарплата
            });

            LoadEmployees(); // Перезагрузка таблицы
            ClearForm(); // Очистка полей ввода
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_id == null) return; // Если сотрудник не выбран, ничего не делаем
            // Запрос подтверждения удаления у пользователя
            if (MessageBox.Show("Удалить запись?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _repo.Delete(_id.Value); // Удаление записи по ID
                LoadEmployees(); // Перезагрузка таблицы
                ClearForm(); // Очистка формы
            }
        }

        private void DgEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор строки в таблице
        {
            // Срабатывает при выборе строки в таблице (перевод формы в режим редактирования)
            if (dgEmployees.SelectedItem is Employee emp)
            {
                _id = emp.Id; // Сохранение ID выбранного сотрудника
                txtFullName.Text = emp.FullName; // Заполнение поля ФИО
                txtPosition.Text = emp.Position; // Заполнение поля Должность
                txtDepartment.Text = emp.Department; // Заполнение поля Отдел
                txtSalary.Text = emp.Salary.ToString(); // Заполнение поля Зарплата
                btnSave.Content = "Сохранить"; // Смена текста кнопки сохранения
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearForm(); 

        private void ClearForm() // Очистить форму
        {
            _id = null; // Сброс выбранного ID (перевод в режим добавления)
            txtFullName.Clear(); // Очистка поля ФИО
            txtPosition.Clear(); // Очистка поля Должность
            txtDepartment.Clear(); // Очистка поля Отдел
            txtSalary.Clear(); // Очистка поля Зарплата
            btnSave.Content = "Добавить / Сохранить"; // Возврат исходного текста кнопки
            dgEmployees.UnselectAll(); // Снятие выделения со строки таблицы
        }
    }
}