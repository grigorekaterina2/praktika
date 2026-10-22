using System.Windows;
using System.Windows.Controls;

namespace TaskManagerWpf
{
    public partial class MainWindow : Window
    {
        private readonly TaskManagementLogic _db = new TaskManagementLogic(); // Экземпляр логики БД
        private int _pid, _eid, _tid; // Идентификаторы выбранных записей

        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов UI
            Refresh(); // Обновление данных
        }

        private void Refresh()
        {
            GridP.ItemsSource = _db.GetProjects(); // Загрузка проектов в таблицу
            GridE.ItemsSource = _db.GetEmployees(); // Загрузка сотрудников в таблицу
            GridT.ItemsSource = _db.GetTasks(); // Загрузка задач в таблицу
            CProject.ItemsSource = _db.GetProjects(); // Загрузка проектов в список
            CEmp.ItemsSource = _db.GetEmployees(); // Загрузка сотрудников в список
        }

        #region Проекты
        private void P_Select(object sender, SelectionChangedEventArgs e)
        {
            if (GridP.SelectedItem is Project p)
            {
                _pid = p.Id; // Сохранение ID проекта
                PT.Text = p.Title; // Вывод названия
                PD.Text = p.Description; // Вывод описания
                PS.SelectedDate = p.StartDate; // Вывод даты начала
                PE.SelectedDate = p.EndDate; // Вывод даты окончания
            }
        }

        private void P_Save(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PT.Text)) return; // Проверка названия

            _db.SaveProject(new Project
            {
                Id = _pid, // Идентификатор
                Title = PT.Text, // Название
                Description = PD.Text, // Описание
                StartDate = PS.SelectedDate ?? System.DateTime.Now, // Дата начала
                EndDate = PE.SelectedDate ?? System.DateTime.Now.AddDays(30) // Дата окончания
            });

            ClearProjectFields(); // Очистка полей
            Refresh(); // Обновление интерфейса
        }

        private void P_Del(object sender, RoutedEventArgs e)
        {
            if (_pid > 0)
            {
                _db.DelProject(_pid); // Удаление проекта из БД
                ClearProjectFields(); // Очистка полей
                Refresh(); // Обновление интерфейса
            }
        }

        private void ClearProjectFields()
        {
            _pid = 0; // Сброс ID
            PT.Clear(); // Очистка названия
            PD.Clear(); // Очистка описания
            PS.SelectedDate = null; // Сброс даты начала
            PE.SelectedDate = null; // Сброс даты окончания
            GridP.UnselectAll(); // Сброс выбора в таблице
        }
        #endregion

        #region Сотрудники
        private void E_Select(object sender, SelectionChangedEventArgs e)
        {
            if (GridE.SelectedItem is Employee emp)
            {
                _eid = emp.Id; // Сохранение ID сотрудника
                EN.Text = emp.FullName; // Вывод ФИО
                EP.Text = emp.Position; // Вывод должности
                EM.Text = emp.Email; // Вывод почты
            }
        }

        private void E_Save(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EN.Text)) return; // Проверка ФИО

            _db.SaveEmployee(new Employee
            {
                Id = _eid, // Идентификатор
                FullName = EN.Text, // ФИО
                Position = EP.Text, // Должность
                Email = EM.Text // Почта
            });

            ClearEmployeeFields(); // Очистка полей
            Refresh(); // Обновление интерфейса
        }

        private void E_Del(object sender, RoutedEventArgs e)
        {
            if (_eid > 0)
            {
                _db.DelEmployee(_eid); // Удаление сотрудника из БД
                ClearEmployeeFields(); // Очистка полей
                Refresh(); // Обновление интерфейса
            }
        }

        private void ClearEmployeeFields()
        {
            _eid = 0; // Сброс ID
            EN.Clear(); // Очистка ФИО
            EP.Clear(); // Очистка должности
            EM.Clear(); // Очистка почты
            GridE.UnselectAll(); // Сброс выбора в таблице
        }
        #endregion

        #region Задачи
        private void T_Select(object sender, SelectionChangedEventArgs e)
        {
            if (GridT.SelectedItem is TaskItem t)
            {
                _tid = t.Id; // Сохранение ID задачи
                CProject.SelectedValue = t.ProjectId; // Выбор проекта
                TT.Text = t.Title; // Вывод названия
                CEmp.SelectedValue = t.EmpId; // Выбор исполнителя
                CStatus.Text = t.Status; // Вывод статуса
                TD.SelectedDate = t.DueDate; // Вывод дедлайна
            }
        }

        private void T_Save(object sender, RoutedEventArgs e)
        {
            if (CProject.SelectedValue == null || string.IsNullOrWhiteSpace(TT.Text)) return; // Проверка данных

            _db.SaveTask(new TaskItem
            {
                Id = _tid, // Идентификатор
                ProjectId = (int)(CProject.SelectedValue ?? 0), // ID проекта
                Title = TT.Text, // Название
                EmpId = (int?)CEmp.SelectedValue, // ID исполнителя
                Status = (CStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Новая", // Статус
                DueDate = TD.SelectedDate ?? System.DateTime.Now // Дедлайн
            });

            ClearTaskFields(); // Очистка полей
            Refresh(); // Обновление интерфейса
        }

        private void T_Del(object sender, RoutedEventArgs e)
        {
            if (_tid > 0)
            {
                _db.DelTask(_tid); // Удаление задачи из БД
                ClearTaskFields(); // Очистка полей
                Refresh(); // Обновление интерфейса
            }
        }

        private void ClearTaskFields()
        {
            _tid = 0; // Сброс ID
            CProject.SelectedIndex = -1; // Сброс проекта
            TT.Clear(); // Очистка названия
            CEmp.SelectedIndex = -1; // Сброс исполнителя
            CStatus.SelectedIndex = 0; // Сброс статуса
            TD.SelectedDate = null; // Сброс даты
            GridT.UnselectAll(); // Сброс выбора в таблице
        }
        #endregion

        #region Отчет
        private void GenReport(object sender, RoutedEventArgs e) => TxtR.Text = _db.GetReport(); // Генерация отчета
        #endregion
    }
}