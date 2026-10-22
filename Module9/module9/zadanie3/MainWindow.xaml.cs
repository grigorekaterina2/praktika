using System;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace TaskPlanner
{
    public partial class MainWindow : Window
    {
        readonly TaskLogic logic = new TaskLogic(); // Логика приложения
        readonly DispatcherTimer timer = new DispatcherTimer(); // Таймер проверки сроков

        public MainWindow() // Конструктор главного окна
        {
            InitializeComponent(); // Инициализация визуальных элементов 
            logic.Load(); // Загрузка сохраненных задач
            TaskGrid.ItemsSource = logic.Tasks; // Привязка списка задач к таблице
            PriorityBox.ItemsSource = new[] { "Низкий", "Средний", "Высокий" }; 
            PriorityBox.SelectedIndex = 1; // Установка среднего приоритета по умолчанию
            DueBox.SelectedDate = DateTime.Today.AddDays(1); // Дедлайн по умолчанию — завтра

            timer.Interval = TimeSpan.FromSeconds(30); // Интервал срабатывания таймера (30 сек)
            timer.Tick += (s, e) => CheckDeadlines(); // Подписка на событие таймера
            timer.Start(); // Запуск таймера
            CheckDeadlines();                                     
        }

        void CheckDeadlines() // Проверка горящих задач
        {
            var soon = logic.TakeSoon(); // Получение списка задач с истекающим сроком
            if (soon.Count == 0) return; // Если срочных задач нет — выход
            BannerText.Text = "Скоро срок: " + string.Join(", ", soon.Select(t => t.Title + " (" + t.DueText + ")"));
            Banner.Visibility = Visibility.Visible; // Отображение баннера на экране
            SystemSounds.Exclamation.Play(); // Воспроизведение звукового сигнала
        }

        bool ReadForm(out string title, out DateTime due) // Чтение и проверка полей формы
        {
            title = TitleBox.Text.Trim(); // Считывание названия задачи
            due = DueBox.SelectedDate ?? DateTime.Today; // Считывание выбранной даты
            if (title != "") return true; // Если название заполнено — всё хорошо
            MessageBox.Show("Введите название задачи");             
            return false; // Возврат ошибки
        }

        void Add_Click(object sender, RoutedEventArgs e)          
        {
            if (!ReadForm(out var title, out var due)) return; // Проверка корректности формы
            logic.Add(title, (string)PriorityBox.SelectedItem, due, (int)ProgressSlider.Value); // Создание новой задачи
            TitleBox.Clear(); // Очистка поля ввода названия
            ProgressSlider.Value = 0; // Сброс ползунка прогресса
            CheckDeadlines(); // Обновление проверки дедлайнов
        }

        void Edit_Click(object sender, RoutedEventArgs e) // Обработчик клика по кнопке изменения
        {
            if (!(TaskGrid.SelectedItem is TaskItem t)) { MessageBox.Show("Выберите задачу"); return; } // Проверка выбора строки
            if (!ReadForm(out var title, out var due)) return; // Проверка формы
            logic.Update(t, title, (string)PriorityBox.SelectedItem, due, (int)ProgressSlider.Value); // Обновление задачи в логике
            TaskGrid.Items.Refresh(); // Обновление таблицы
            CheckDeadlines(); // Проверка дедлайнов
        }

        void Delete_Click(object sender, RoutedEventArgs e) // Обработчик клика по кнопке удаления
        {
            if (TaskGrid.SelectedItem is TaskItem t) logic.Remove(t); // Удаление выбранной задачи, если она есть
        }

        void TaskGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) // Выбор строки в таблице
        {
            if (!(TaskGrid.SelectedItem is TaskItem t)) return; // Если строка не выбрана — выход
            TitleBox.Text = t.Title; // Передача названия в поле ввода
            PriorityBox.SelectedItem = t.Priority; // Установка приоритета в список
            DueBox.SelectedDate = t.Due; // Установка даты в календарь
            ProgressSlider.Value = t.Progress; // Установка прогресса в ползунок
        }

        void CloseBanner_Click(object sender, RoutedEventArgs e) => Banner.Visibility = Visibility.Collapsed; // Скрытие баннера уведомления
    }
}