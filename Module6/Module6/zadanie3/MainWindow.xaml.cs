using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        private readonly BookRepository _repo = new BookRepository(); // Объект для работы с базой данных
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>(); // Коллекция для таблицы

        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна
            dgBooks.ItemsSource = Books; // Привязка коллекции к таблице
            LoadBooks(); // Загрузка книг из БД
        }

        private void LoadBooks() // Показать книги с учетом поиска
        {
            Books.Clear(); // Очистка коллекции
            foreach (var b in _repo.Search(txtSearch.Text)) // Перебор найденных книг
            {
                Books.Add(b); // Добавление книги в коллекцию
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) // Нажатие кнопки добавления
        {
            // Проверка названия, автора и года
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text) || !int.TryParse(txtYear.Text, out var year))
            {
                MessageBox.Show("Заполните название, автора и год корректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.AddBook(new Book // Передача данных в слой работы с БД
            {
                Title = txtTitle.Text.Trim(), // Название
                Author = txtAuthor.Text.Trim(), // Автор
                Year = year // Год
            });

            txtTitle.Clear(); // Очистка названия
            txtAuthor.Clear(); // Очистка автора
            txtYear.Clear(); // Очистка года
            LoadBooks(); // Перезагрузка таблицы
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e) => LoadBooks(); // Кнопка поиска

        private void BtnReset_Click(object sender, RoutedEventArgs e) // Кнопка сброса поиска
        {
            txtSearch.Clear(); // Очистка строки поиска
            LoadBooks(); // Показ всех книг
        }

        private void BtnRent_Click(object sender, RoutedEventArgs e) // Нажатие кнопки аренды
        {
            if (!(dgBooks.SelectedItem is Book book)) // Проверка выбранной книги
            {
                MessageBox.Show("Выберите книгу в таблице.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReader.Text)) // Проверка имени читателя
            {
                MessageBox.Show("Введите имя читателя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!_repo.Rent(book.Id, txtReader.Text.Trim())) // Попытка аренды
            {
                MessageBox.Show("Эта книга уже арендована.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            txtReader.Clear(); // Очистка имени читателя
            LoadBooks(); // Перезагрузка таблицы
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e) // Нажатие кнопки возврата
        {
            if (!(dgBooks.SelectedItem is Book book) || string.IsNullOrEmpty(book.RentedBy)) // Проверка, что выбрана арендованная книга
            {
                MessageBox.Show("Выберите арендованную книгу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _repo.ReturnBook(book.Id); // Возврат книги
            LoadBooks(); // Перезагрузка таблицы
        }
    }
}