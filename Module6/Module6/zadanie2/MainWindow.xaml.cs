using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TaskManagement
{
    public partial class MainWindow : Window
    {
        private readonly TaskRepository _repo = new TaskRepository(); // Объект для работы с базой данных
        public ObservableCollection<TaskModel> Tasks { get; set; } = new ObservableCollection<TaskModel>(); // Коллекция для таблицы
        private int? _id; // Идентификатор выбранной задачи, null при добавлении

        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна
            LoadTasks(); // Загрузка задач из БД
            dgTasks.ItemsSource = Tasks; // Привязка коллекции к таблице
        }

        private void LoadTasks() // Загрузить задачи
        {
            Tasks.Clear(); // Очистка коллекции
            foreach (var t in _repo.GetAll()) // Перебор задач из базы
            {
                Tasks.Add(t); // Добавление задачи в коллекцию
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) // Нажатие кнопки сохранения
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) // Проверка названия
            {
                MessageBox.Show("Введите название задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "К выполнению"; // Выбранный статус

            _repo.Save(new TaskModel // Передача данных в слой работы с БД
            {
                Id = _id ?? 0, // Если ID нет, значит добавление
                Title = txtTitle.Text.Trim(), // Название
                Description = txtDescription.Text.Trim(), // Описание
                Status = status // Статус
            });

            LoadTasks(); // Перезагрузка таблицы
            ClearForm(); // Очистка формы
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) // Нажатие кнопки удаления
        {
            if (_id == null) return; // Если задача не выбрана, ничего не происходит
            if (MessageBox.Show("Удалить задачу?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes) // Запрос на подтверждение и удаление при ответе Да
            {
                _repo.Delete(_id.Value); // Удаление задачи по ID
                LoadTasks(); // Перезагрузка таблицы
                ClearForm(); // Очистка формы
            }
        }

        private void DgTasks_SelectionChanged(object sender, SelectionChangedEventArgs e) // Выбор строки в таблице
        {
            if (dgTasks.SelectedItem is TaskModel t)
            {
                _id = t.Id; // Сохранение ID выбранной задачи
                txtTitle.Text = t.Title; // Заполнение названия
                txtDescription.Text = t.Description; // Заполнение описания
                foreach (ComboBoxItem item in cmbStatus.Items) // Поиск нужного статуса в списке
                {
                    if (item.Content.ToString() == t.Status) cmbStatus.SelectedItem = item; // Выбор статуса
                }
                btnSave.Content = "Сохранить"; // Смена текста кнопки
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e) => ClearForm(); // Кнопка очистки формы

        private void ClearForm() // Очистить форму
        {
            _id = null; // Сброс ID, режим добавления
            txtTitle.Clear(); // Очистка названия
            txtDescription.Clear(); // Очистка описания
            cmbStatus.SelectedIndex = 0; // Возврат первого статуса
            btnSave.Content = "Добавить / Сохранить"; // Возврат текста кнопки
            dgTasks.UnselectAll(); // Снятие выделения в таблице
        }
    }
}